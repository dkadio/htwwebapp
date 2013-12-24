$(window).load(function () {
    ajaxRequest();
});


function ajaxRequest() {
    var loadUrl = "http://localhost:52142/htwservice.ashx?function=" + "dozentenlistview";

    $("#dozent")
        .load(loadUrl, null, function (responseText) {
            $("#dozent").html(responseText);
            $("ul").listview("refresh");
        });
}