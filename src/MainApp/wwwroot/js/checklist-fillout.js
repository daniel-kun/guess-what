/**
Fills in the requests array with ChecklistResultItem with filled Result and TemplateItemId properties
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
        var tableRowId = $(this)[0].id;
        var prefix = "check_";
        if (tableRowId.slice(0, prefix.length) == prefix) {
            var checklistTemplateItemId = tableRowId.slice(prefix.length);
            var result = "";
            if ($("input.marker-ok", $(this))[0].checked) {
                result = "CheckedAndOk";
            } else if ($("input.marker-not-ok", $(this))[0].checked) {
                result = "CheckedAndNotOk";
            } else if ($("input.marker-not-checked", $(this))[0].checked) {
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
