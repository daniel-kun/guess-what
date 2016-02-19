/**
Extracts and returns the id of a ChecklistTemplate, ChecklistItem or similar from
an element's id, that usually has a prefix.
Example:
getPrefixedIdFromElementId("checked_", "checked_SOMERANDOMGUID") -> "SOMERANDOMGUID"
**/
function getPrefixedIdFromElementId(prefix, elementId) {
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
Returns and object { id: <template item id>, checkboxType: <0, 1 or 2> } where id is the
template item's id and checkboxType is 0 when element is an "OK" checkbox, "1" when it is a
"Not OK" checkbox and 2 when it is a "Not Checked" checkbox. 
**/
function getEntityIdAndCheckBoxTypeFromElement(element) {
    var elementId = element.prop("id");
    var checklistItemId = getPrefixedIdFromElementId("check_ok_", elementId);
    var checkboxType; // 0 = OK, 1 = Not OK, 2 = Not Checked
    if (checklistItemId != "") {
        // It was the "check_ok" checkbox that was clicked - remove the other checkmarks
        return { id: checklistItemId, checkboxType: 0 };
    } else {
        checklistItemId = getPrefixedIdFromElementId("check_not_ok_", elementId);
        if (checklistItemId != "") {
            // It was the "check_not_ok" checkbox that was clicked - remove the other checkmarks
            return { id: checklistItemId, checkboxType: 1 };
        } else {
            // It must have been the "not_checked" checkbox that was clicked - remove the other checkmarks
            checklistItemId = getPrefixedIdFromElementId("not_checked_", elementId);
            return { id: checklistItemId, checkboxType: 2 };
        }
    }
    throw { errorCode: 1, message: "Could not identify checkbox type of element " + element }
}

/**
Returns true when the row with the template item id checklistItemId should be expanded (i.e. "OK" is checked)
or false when it shall be collapsed.
**/
function shouldRowExpand(checklistItemId)
{
    return $("#check_ok_" + checklistItemId).hasClass("checkbox-checked");
}

/**
Expands or collapsed the children of checklistItemId.
@param checklistItemId The template item id of the row which children's collapsed state should be updated.
@param isParentCollapsed True when the parent is collapsed and hence all children shall be collapsed
disregarding the checklistItemId's and the childrens check state. False when the parent is not collapsed
and the checklistItemId's children and their childrin shall be expanded or collapsed according to
their parent's check state.
**/
function updateExpandState(checklistItemId, isParentCollapsed)
{
    if (isParentCollapsed || !shouldRowExpand(checklistItemId)) {
        $("tr.checklistrow-parent-" + checklistItemId).each(function () {
            $(this).addClass("checklistrow-collapsed");
            var childItemId = getPrefixedIdFromElementId("check_", $(this).prop("id"));
            updateExpandState(childItemId, true);
        });
    } else {
        $("tr.checklistrow-parent-" + checklistItemId).each(function () {
            $(this).removeClass("checklistrow-collapsed");
            var childItemId = getPrefixedIdFromElementId("check_", $(this).prop("id"));
            updateExpandState(childItemId, false);
        });
    }
}

/**
Toggle "checked" state for a's that have the checkbox class, after the a has been clicked.
**/
$(function () {
    $("a.checkbox").each(function () {
        $(this).click(function (e) {
            e.preventDefault();
            var entityIdAndCheckBoxType = getEntityIdAndCheckBoxTypeFromElement($(this));
            var checklistItemId = entityIdAndCheckBoxType.id;
            var checkboxType = entityIdAndCheckBoxType.checkboxType; // 0 = OK, 1 = Not OK, 2 = Not Checked
            switch (checkboxType) {
                case 0:
                    uncheckCheckmark($("#check_not_ok_" + checklistItemId), colorClassFromCheckboxType(1));
                    uncheckCheckmark($("#not_checked_" + checklistItemId), colorClassFromCheckboxType(2));
                    break;
                case 1:
                    uncheckCheckmark($("#check_ok_" + checklistItemId), colorClassFromCheckboxType(0));
                    uncheckCheckmark($("#not_checked_" + checklistItemId), colorClassFromCheckboxType(2));
                    break;
                default:
                    uncheckCheckmark($("#check_ok_" + checklistItemId), colorClassFromCheckboxType(0));
                    uncheckCheckmark($("#check_not_ok_" + checklistItemId), colorClassFromCheckboxType(1));
                    break;
            }
            if (checklistItemId != "") {
                $("#check_ok_"      + checklistItemId).removeClass("checkbox-unvisited");
                $("#check_not_ok_"  + checklistItemId).removeClass("checkbox-unvisited");
                $("#not_checked_"   + checklistItemId).removeClass("checkbox-unvisited");
            }
            if ($(this).hasClass("checkbox-checked")) {
                uncheckCheckmark($(this), colorClassFromCheckboxType(checkboxType));
            } else {
                checkCheckmark($(this), colorClassFromCheckboxType(checkboxType));
            }
            updateExpandState(checklistItemId, false);
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
            var checklistTemplateItemId = getPrefixedIdFromElementId("check_", tableRowId);
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
