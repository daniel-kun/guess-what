/**
Extracts and returns the id of a ChecklistTemplate, ChecklistItem or similar from
an element's id.
Example:
getEntityIdFromElementId("checked_", "checked_SOMERANDOMGUID") -> "SOMERANDOMGUID"
**/
function getEntityIdFromElementId(prefix, elementId) {
    if (elementId.slice(0, prefix.length) == prefix) {
        return elementId.slice(prefix.length);
    } else {
        return "";
    }
}


/**
Checks a checkmark that was built with an <a class="checkmark"> containing at least one <span>.
@param  checkmark An jQuery object pointing to the 'a' element that should be checked.
        Might be an empty list - in this case, this function does nothing.
@param colorClass A CSS class that should be added to the 'a', used for coloring when checked.
**/
function checkCheckmark(checkmark, colorClass)
{
    if (checkmark.length > 0) {
        checkmark.children()[0].innerHTML = "&#9745;";
        checkmark.addClass(colorClass);
        checkmark.removeClass("checkbox-unchecked");
        checkmark.addClass("checkbox-checked");
    }
}

/**
Unchecks a checkmark that was built with an <a class="checkmark"> containing at least one <span>.
@param  checkmark An jQuery object pointing to the 'a' element that should be unchecked.
        Might be an empty list - in this case, this function does nothing.
@param colorClass A CSS class that should be removed to the 'a', used for coloring when checked.
**/
function uncheckCheckmark(checkmark, colorClass)
{
    if (checkmark.length > 0) {
        checkmark.children()[0].innerHTML = "&#9744;";
        checkmark.removeClass(colorClass);
        checkmark.removeClass("checkbox-checked");
        checkmark.addClass("checkbox-unchecked");
    }
}

/**
Returns a CSS class for the 'a' element of a checkbox that defines it's color.
checkboxType 0 = OK, 1 = Not OK, 2 = Not Checked
**/
function colorClassFromCheckboxType(checkboxType)
{
    switch (checkboxType) {
        case 0: return "checkbox-color-ok";
        case 1: return "checkbox-color-not-ok";
        default: return "checkbox-color-not-checked";
    }
}

/**
Toggle "checked" state for a's that have the checkbox class
**/
$(function () {
    $("a.checkbox").each(function (e) {
        $(this).click(function () {
            var checklistItemId = getEntityIdFromElementId("check_ok_", $(this).prop("id"));
            var checkboxType; // 0 = OK, 1 = Not OK, 2 = Not Checked
            if (checklistItemId != "") {
                // It was the "check_ok" checkbox that was clicked - remove the other checkmarks
                checkboxType = 0;
                uncheckCheckmark($("#check_not_ok_" + checklistItemId), colorClassFromCheckboxType(1));
                uncheckCheckmark($("#not_checked_" + checklistItemId), colorClassFromCheckboxType(2));
            } else {
                checklistItemId = getEntityIdFromElementId("check_not_ok_", $(this).prop("id"));
                if (checklistItemId != "") {
                    // It was the "check_not_ok" checkbox that was clicked - remove the other checkmarks
                    checkboxType = 1;
                    uncheckCheckmark($("#check_ok_" + checklistItemId), colorClassFromCheckboxType(0));
                    uncheckCheckmark($("#not_checked_" + checklistItemId), colorClassFromCheckboxType(2));
                } else {
                    // It must have been the "not_checked" checkbox that was clicked - remove the other checkmarks
                    checklistItemId = getEntityIdFromElementId("not_checked_", $(this).prop("id"));
                    checkboxType = 2;
                    uncheckCheckmark($("#check_ok_" + checklistItemId), colorClassFromCheckboxType(0));
                    uncheckCheckmark($("#check_not_ok_" + checklistItemId), colorClassFromCheckboxType(1));
                }
            }
            if (checklistItemId != "") {
                $("#check_ok_"      + checklistItemId).removeClass("checkbox-unvisited");
                $("#check_not_ok_"  + checklistItemId).removeClass("checkbox-unvisited");
                $("#not_checked_"   + checklistItemId).removeClass("checkbox-unvisited");
            }
            var wasChecked = $(this).hasClass("checkbox-checked");
            if (wasChecked) {
                uncheckCheckmark($(this), colorClassFromCheckboxType(checkboxType));
            } else {
                checkCheckmark($(this), colorClassFromCheckboxType(checkboxType));
            }
            // If this is a parent row, collapse / expand the child rows:
            if ($(this).hasClass("checklistrow-parent")) {
                if (checkboxType == 0 && ! wasChecked) {
                    $("tr.checklistrow-parent-" + checklistItemId).each(function () {
                        $(this).removeClass("checklistrow-collapsed");
                    });
                } else {
                    $("tr.checklistrow-parent-" + checklistItemId).each(function () {
                        $(this).addClass("checklistrow-collapsed");
                    });
                }
            }
        });
    });
});

/**
Fills in the requests array with ChecklistResultItem with proper Result and TemplateItemId properties
for all filled out checklist items.

@param requests Will be filled with new objects with properties Result (either one of "CheckedAndOk", 
"CheckedAndNotOk" or "NotChecked") and the TemplateItemId.

@result Returns false when at least one checklist item is not filled out or an internal error occured.
Returns true when all checklist items are filled out.
**/
function fillRequests(resultItems)
{
    var entryComplete = true;
    $("tr.marker-checkcell").each(function (tableRow) {
        if (!$(this).hasClass("checklistrow-collapsed")) {
            var tableRowId = $(this)[0].id;
            var prefix = "check_";
            var checklistTemplateItemId = getEntityIdFromElementId("check_", tableRowId);
            if (checklistTemplateItemId != "") {
                var result = "";
                if ($("#check_ok_" + checklistTemplateItemId).hasClass("checkbox-checked")) {
                    result = "CheckedAndOk";
                } else if ($("#check_not_ok_" + checklistTemplateItemId).hasClass("checkbox-checked")) {
                    result = "CheckedAndNotOk";
                } else if ($("#not_checked_" + checklistTemplateItemId).hasClass("checkbox-checked")) {
                    result = "NotChecked";
                } else {
                    entryComplete = false;
                    return false;
                }
                resultItems.push({
                    "TemplateItemId": checklistTemplateItemId,
                    "Result": result,
                })
            } else {
                return false;
            }
        } else {
            return true;
        }
    });
    return entryComplete;
}

/**
Sends the checklist results via JSON to the server which stores the results and, if all went well, 
navigates to the result.
**/
function saveResults()
{
    // get values from FORM
    var request = {
        "UserId": $("input#userid").prop("value"),
        "TemplateId": $("input#template-id").prop("value"),
        "Results": []
    };
    var resultItems = request["Results"];
    if (fillRequests(resultItems)) {
        $.ajax({
            type: "POST",
            url: "result",
            dataType: "json",
            data: request,
            success: function (data) {
                window.location.href = getBaseUrl() + "/c/result/" + data.Id;
            },
        });
    } else {
        alert("Please fill out all checklist items!");
    }
}

/**
Handles the press of the submit button.
@see saveResults
**/
$(function () {
    $("#submit-results").jqBootstrapValidation({
        preventSubmit: true,
        submitError: function ($form, event, errors) {
            // something to have when submit produces an error ?
            // Not decided if I need it yet
        },
        submitSuccess: function ($form, event) {
            event.preventDefault(); // prevent default submit behaviour
            saveResults();
        },
        filter: function () {
            return $(this).is(":visible");
        },
    });
});
