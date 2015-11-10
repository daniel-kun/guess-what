/*
Copies the content of a given text input to the clipboard.
*/
function copyUrlToClipboard(input) {
    input.select();

    try {
        document.execCommand('copy');
    } catch (err) {
        window.prompt("Copy to clipboard: Ctrl+C", text);
    }
}

// **************************************
// jQuery to collapse the navbar on scroll
// **************************************

$(window).scroll(function () {
    if ($(".navbar").offset().top > 50) {
        $(".navbar-fixed-top").addClass("top-nav-collapse");
    } else {
        $(".navbar-fixed-top").removeClass("top-nav-collapse");
    }
});

// ****************************************************************
// jQuery for page scrolling feature - requires jQuery Easing plugin
// ****************************************************************

$(function () {
    $('.page-scroll a').bind('click', function (event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 300, 'swing');
        event.preventDefault();
    });
});

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
                scrollTop: $("#checklist-preconditions").offset().top
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

// Poor man's substitution for Math.sign (only available from ECMA Script 2015)
function mathSign(num) {
    if (num > 0.0) {
        return 1.0;
    } else if (num < 0.0) {
        return -1.0;
    } else {
        return 0.0;
    }
}

// Taken from http://stackoverflow.com/questions/1726630/javascript-formatting-number-with-exactly-two-decimals
// Replaced Math.sign with mathSign, because it is not compatible with all browsers.
function precise_round(num, decimals) {
    var t = Math.pow(10, decimals);
    return (Math.round((num * t) + (decimals > 0 ? 1 : 0) * (mathSign(num) * (10 / Math.pow(100, decimals)))) / t).toFixed(decimals);
}

// Get parameters from URI query parameters
function getQueryParams(qs) {
    qs = qs.split('+').join(' ');

    var params = {},
        tokens,
        re = /[?&]?([^=]+)=([^&]*)/g;

    while (tokens = re.exec(qs)) {
        params[decodeURIComponent(tokens[1])] = decodeURIComponent(tokens[2]);
    }

    return params;
}

function applyEstimationCode(code) {
    var estimationInHex = code.substring(0, 4);
    var estimation = parseInt(estimationInHex, 16);
    $("#estimate").val(estimation);
    var index = 4;
    $(".btn-group").each(function (btn) {
        $(".btn-default", $(this)).removeClass("active");
        $(".btn-success", $(this)).removeClass("active");
        $(".btn-primary", $(this)).removeClass("active");
        $(".btn-warning", $(this)).removeClass("active");
        $(".btn-danger", $(this)).removeClass("active");

        var factorCode = code[index];
        if (factorCode == "0") {
            $(".btn-default", $(this)).addClass("active");
        } else if (factorCode == "A") {
            $(".btn-success", $(this)).addClass("active");
        } else if (factorCode == "B") {
            $(".btn-primary", $(this)).addClass("active");
        } else if (factorCode == "C") {
            $(".btn-warning", $(this)).addClass("active");
        } else if (factorCode == "D") {
            $(".btn-danger", $(this)).addClass("active");
        }
        ++index;
    });
    updateResults(null);
}

// Smoothly scroll to the element with id elementId
function smoothScrollTo(elementId) {
    $('html,body').animate({
        scrollTop: $(elementId).offset().top
    }, 300, 'swing');
}

function updateResults(trackerMetric) {
    // Iterate over all btn-groups and find each button for none/low/high/very high selections to create a factor.
    var points = 0;
    $(".btn-group").each(function (btn) {
        if ($(".btn-success", $(this)).hasClass("active")) {
            points = points - 1;
        } else if ($(".btn-warning", $(this)).hasClass("active")) {
            points = points + 1;
        } else if ($(".btn-danger", $(this)).hasClass("active")) {
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
    $(".group2").each(function (btn) {
        var selection = 0.0;
        if ($(".btn-success", $(this)).hasClass("active")) {
            selection = 1.0;
        } else if ($(".btn-primary", $(this)).hasClass("active")) {
            selection = 2.0;
        } else if ($(".btn-warning", $(this)).hasClass("active")) {
            selection = 3.0;
        } else if ($(".btn-danger", $(this)).hasClass("active")) {
            selection = 4.0;
        }
        result.factorId = $(this).attr("id");
        if (trackerMetric !== null)
            trackerMetric(result.factorId, selection);
    });
    $("#clipboardUrl").val(buildResultUrl());
    return result;
}

function getBaseUrl() {
    if (!location.origin)
        return location.protocol + "//" + location.host;
    else
        return location.origin;
}

function buildResultUrl() {
    var url = getBaseUrl() + "/?e=";
    var foo = $("input#estimate").val();
    var estimationStr = parseInt(foo).toString(16);
    var padding = "0000";
    url += padding.substring(0, padding.length - estimationStr.length) + estimationStr;
    $(".btn-group").each(function (btn) {
        if ($(".btn-default", $(this)).hasClass("active")) {
            url = url + "0";
        } else if ($(".btn-success", $(this)).hasClass("active")) {
            url = url + "A";
        } else if ($(".btn-primary", $(this)).hasClass("active")) {
            url = url + "B";
        } else if ($(".btn-warning", $(this)).hasClass("active")) {
            url = url + "C";
        } else if ($(".btn-danger", $(this)).hasClass("active")) {
            url = url + "D";
        }
    });
    return url;
}

$(function () {
    /*
        $("#slider").hide();
        $("#checklist-preconditions").hide();
        $("#checklist-complexity").hide();
        $("#result").show();
    */

    var params = getQueryParams(document.location.search);
    if ("e" in params) {
        applyEstimationCode(params.e);
        $("#checklist-preconditions").show();
        $("#checklist-complexity").show();
        $("#result").show();
        $("#params").text(buildResultUrl());
        smoothScrollTo("#result");
    }

    $("#btn-checklist1").click(function (e) {
        e.preventDefault();
        $(".group1").each(function (btn) {
            selection = 0.0;
            if ($(".btn-success", $(this)).hasClass("active")) {
                selection = 1.0;
            } else if ($(".btn-primary", $(this)).hasClass("active")) {
                selection = 2.0;
            } else if ($(".btn-warning", $(this)).hasClass("active")) {
                selection = 3.0;
            } else if ($(".btn-danger", $(this)).hasClass("active")) {
                selection = 4.0;
            }
            factorId = $(this).attr("id");
            //appInsights.trackMetric(factorId, selection);
        });

        //appInsights.trackEvent("Next");
        smoothScrollTo("#checklist-complexity");
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

