// This js runs in Result.cshtml
$(function () {
    // The JIRA, GitHub and url "copy code" functionality:

    $("#jira-badgecode-copy").click(function (e) {
        e.preventDefault();
        $("#github-badgecode").addClass("hidden");
        $("#url-badgecode").addClass("hidden");
        $("#jira-badgecode").removeClass("hidden");
        copyUrlToClipboard($("#jira-badgecode"));
    });

    $("#github-badgecode-copy").click(function (e) {
        e.preventDefault();
        $("#github-badgecode").removeClass("hidden");
        $("#url-badgecode").addClass("hidden");
        $("#jira-badgecode").addClass("hidden");
        copyUrlToClipboard($("#github-badgecode"));
    });

    $("#url-badgecode-copy").click(function (e) {
        e.preventDefault();
        $("#github-badgecode").addClass("hidden");
        $("#jira-badgecode").addClass("hidden");
        $("#url-badgecode").removeClass("hidden");
        copyUrlToClipboard($("#url-badgecode"));
    });
})
