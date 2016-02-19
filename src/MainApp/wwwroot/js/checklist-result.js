// This js runs in Result.cshtml
$(function () {
    // The JIRA, GitHub and url "copy code" functionality:

    $("#jira-badgecode-copy").click(function (e) {
        e.preventDefault();
        $("#jira-badgecode"     ).removeClass("hidden");
        $("#github-badgecode"   ).addClass("hidden");
        $("#redmine-badgecode"  ).addClass("hidden");
        $("#url-badgecode"      ).addClass("hidden");
        copyUrlToClipboard($("#jira-badgecode"));
        $("#badgecode-copy-notice").removeClass("hidden");
    });

    $("#github-badgecode-copy").click(function (e) {
        e.preventDefault();
        $("#jira-badgecode"     ).addClass("hidden");
        $("#github-badgecode"   ).removeClass("hidden");
        $("#redmine-badgecode"  ).addClass("hidden");
        $("#url-badgecode"      ).addClass("hidden");
        copyUrlToClipboard($("#github-badgecode"));
        $("#badgecode-copy-notice").removeClass("hidden");
    });

    $("#redmine-badgecode-copy").click(function (e) {
        e.preventDefault();
        $("#jira-badgecode"     ).addClass("hidden");
        $("#github-badgecode"   ).addClass("hidden");
        $("#redmine-badgecode"  ).removeClass("hidden");
        $("#url-badgecode"      ).addClass("hidden");
        copyUrlToClipboard($("#redmine-badgecode"));
        $("#badgecode-copy-notice").removeClass("hidden");
    });

    $("#url-badgecode-copy").click(function (e) {
        e.preventDefault();
        $("#jira-badgecode"     ).addClass("hidden");
        $("#github-badgecode"   ).addClass("hidden");
        $("#redmine-badgecode"  ).addClass("hidden");
        $("#url-badgecode"      ).removeClass("hidden");
        copyUrlToClipboard($("#url-badgecode"));
        $("#badgecode-copy-notice").removeClass("hidden");
    });
})
