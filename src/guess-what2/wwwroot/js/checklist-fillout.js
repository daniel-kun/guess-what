
$(function () {
    $("#submit-results").jqBootstrapValidation({
        preventSubmit: true,
        submitError: function ($form, event, errors) {
            // something to have when submit produces an error ?
            // Not decided if I need it yet
        },
        submitSuccess: function ($form, event) {
            event.preventDefault(); // prevent default submit behaviour
            // get values from FORM
            var request = {
                "UserId": "d.albuschat[FIXME]",
                "TemplateId": $("input#template-id").value,
                "Results": []
            };
            var resultItems = request["Results"];
            resultItems.push({
                "TemplateItemId": "asdf",
                "Result": "NotChecked",
            })
            $.ajax({
                type: "POST",
                url: "result",
                dataType: "json",
                data: request,
                success: function (data) {
                    window.location.href = getBaseUrl() + "/c/result/" + data.Id;
                },
            });
        },
        filter: function () {
            return $(this).is(":visible");
        },
    });
});
