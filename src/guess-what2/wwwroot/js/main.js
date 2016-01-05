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

// Smoothly scroll to the element with id elementId
function smoothScrollTo(elementId) {
    $('html,body').animate({
        scrollTop: $(elementId).offset().top
    }, 300, 'swing');
}

function getBaseUrl() {
    if (!location.origin)
        return location.protocol + "//" + location.host;
    else
        return location.origin;
}

