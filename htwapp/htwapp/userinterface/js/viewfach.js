/*$(document).ready(function () {
    alert("viewfach");
});

*/
function ajaxRequest() {
    var loadUrl = "http://localhost:52142/htwservice.ashx?function=" + "dozentenlistview";

    $("#dozent")
        .load(loadUrl, null, function (responseText) {
            $("#dozent").html(responseText);
            $("ul").listview("refresh");
        });
}

function showfachview(element) {
    alert("viewfach");

}


function getfach() {
    alert("fachview");
}