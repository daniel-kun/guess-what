/*
Jquery Validation using jqBootstrapValidation
example is taken from jqBootstrapValidation docs 
*/
$(function () {

    $("#estimate").jqBootstrapValidation({
        preventSubmit: true,
        submitError: function ($form, event, errors) {
            // something to have when submit produces an error ?
            // Not decided if I need it yet
        },
        submitSuccess: function ($form, event) {
            event.preventDefault(); // prevent default submit behaviour
            // get values from FORM

            var estimate = $("input#estimate").val();

            //appInsights.trackEvent("estimate_given", { Estimate: estimate });
            //appInsights.trackMetric("Estimate", estimate);

            $("div.checklistfull").show();
            $('html,body').animate({
                scrollTop: $("#page-preconditions").offset().top
            }, 300, 'swing');
        },
        filter: function () {
            return $(this).is(":visible");
        },
    });

    $("a[data-toggle=\"tab\"]").click(function (e) {
        e.preventDefault();
        $(this).tab("show");
    });
});

function updateResults(trackerMetric) {
    // Iterate over all btn-groups and find each button for none/low/high/very high selections to create a factor.
    var points = 0;
    $(".btn-group").each(function (btn) {
        if ($(".btn-very-good", $(this)).hasClass("active")) {
            points = points - 1;
        } else if ($(".btn-not-good", $(this)).hasClass("active")) {
            points = points + 1;
        } else if ($(".btn-bad", $(this)).hasClass("active")) {
            points = points + 2;
        }
    });
    // The maximum factor, with all 17 "worst" selections made, is 500% above the original estimate.
    // That means that a selected point weights 1 / (2* 17) * 5 in factor.
    var result = {
        factor: precise_round(Math.max(1.0 + (points / 34.0) * 5.0, 1.0), 1),
        originalEstimate: $("#estimate").val()
    };
    $("#result-factor").text(result.factor);
    $("#result-original-estimate").text(result.originalEstimate);
    $("#result-final").text((result.originalEstimate * result.factor).toFixed(1));
    $("div.section-result").show();
    $(".group1").each(function (btn) {
        var selection = 0.0;
        if ($(".btn-very-good", $(this)).hasClass("active")) {
            selection = 1.0;
        } else if ($(".btn-good", $(this)).hasClass("active")) {
            selection = 2.0;
        } else if ($(".btn-not-good", $(this)).hasClass("active")) {
            selection = 3.0;
        } else if ($(".btn-bad", $(this)).hasClass("active")) {
            selection = 4.0;
        }
        result.factorId = $(this).attr("id");
        if (trackerMetric !== null)
            trackerMetric(result.factorId, selection);
    });
    $("#clipboardUrl").val(buildResultUrl());
    return result;
}

function buildResultUrl() {
    var url = getBaseUrl() + "/?e=";
    var foo = $("input#estimate").val();
    var estimationStr = parseInt(foo).toString(16);
    var padding = "0000";
    url += padding.substring(0, padding.length - estimationStr.length) + estimationStr;
    $(".btn-group").each(function (btn) {
        if ($(".btn-rating-na",         $(this)).hasClass("active")) {
            url = url + "0";
        } else if ($(".btn-very-good",  $(this)).hasClass("active")) {
            url = url + "A";
        } else if ($(".btn-good",       $(this)).hasClass("active")) {
            url = url + "B";
        } else if ($(".btn-not-good",   $(this)).hasClass("active")) {
            url = url + "C";
        } else if ($(".btn-bad",        $(this)).hasClass("active")) {
            url = url + "D";
        }
    });
    return url;
}

$(function () {
    /*
    // Uncomment this for debugging/designing:
    $("#page-estimate").show();
    $("#page-preconditions").show();
    $("#page-complexity").show();
    $("#result").show();
    */
    if ($("#estimate").val() != 0) {
        $("#page-estimate").show();
        $("#page-preconditions").show();
        $("#page-complexity").show();
        $("#result").show();
        updateResults(null);
        smoothScrollTo("#result");
    }

    $("#btn-checklist1").click(function (e) {
        e.preventDefault();
        $(".group1").each(function (btn) {
            selection = 0.0;
            if ($(".btn-very-good", $(this)).hasClass("active")) {
                selection = 1.0;
            } else if ($(".btn-good", $(this)).hasClass("active")) {
                selection = 2.0;
            } else if ($(".btn-not-good", $(this)).hasClass("active")) {
                selection = 3.0;
            } else if ($(".btn-bad", $(this)).hasClass("active")) {
                selection = 4.0;
            }
            factorId = $(this).attr("id");
            //appInsights.trackMetric(factorId, selection);
        });

        //appInsights.trackEvent("Next");
        smoothScrollTo("#page-complexity");
    });

    $("#btn-finish").click(function (e) {
        e.preventDefault();
        //var results = updateResults(appInsights.trackMetric);
        var results = updateResults(null);

        //appInsights.trackEvent("Finish", { Estimate: results.originalEstimate, Factor: results.factor });
        smoothScrollTo("#result");
    });

    $("#btn-clipboard").click(function (e) {
        e.preventDefault();
        var url = $("#clipboardUrl");
        copyUrlToClipboard(url);
    });
});

