$(window).load(function () {
    ajaxRequest();
});

//laed die Dozentenview und haengt sie an den platzhalter
function ajaxRequest() {
    var loadUrl = "http://localhost:52142/htwservice.ashx?function=" + "dozentenlistview";

    $("#dozent")
        .load(loadUrl, null, function (responseText) {
            $("#dozent").html(responseText);
            //listview muss refresht werden wegen jquery styles
            $("ul").listview("refresh");
        });
}