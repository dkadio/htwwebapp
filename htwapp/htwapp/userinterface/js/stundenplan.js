/// <reference path="../view/viewFach.html" />
/// <reference path="viewfach.js" />
$(document).ready(function () {
 
    $("div:jqmData(role='collapsible')").each(function () {
        getfach($(this));
    }); 
});





function getfach(element) {
    element.bind('tap', function (event, ui) {
        if (element.hasClass('ui-collapsible-collapsed')) {

        } else {

            var loadUrl = "http://localhost:52142/htwservice.ashx?function=" + "getfach" + "&tag=" + element.attr("id") + "&studiengang=" + localStorage.getItem("studiengang") + "&semester=" + localStorage.getItem("semester") + "&wahlfaecher=" + localStorage.getItem("wahlfaecher");
 
           
            switch (element.attr("id")) {
                case ("montag"):
                   
                    $("#montagContent").load(loadUrl, null, function (responseText) {
                        alert(localStorage.getItem("wahlfaecher"));
                        $("#montagContent").html(responseText);
                        $("ul").listview("refresh");
                    });

                    break;
                case ("dienstag"):
                    $("#dienstagContent").load(loadUrl, null, function (responseText) {
                        //  $(content).html(responseText);
                        $("ul").listview("refresh");
                    });

                    break;
                case ("mittwoch"):
                    $("#mittwochContent").load(loadUrl, null, function (responseText) {
                        //  $(content).html(responseText);
                        $("ul").listview("refresh");
                    });
                    break;

                case ("donnerstag"):
                    $("#donnerstagContent").load(loadUrl, null, function (responseText) {
                        //  $(content).html(responseText);
                        $("ul").listview("refresh");
                    });
                    break;

                case ("freitag"):
                    $("#freitagContent").load(loadUrl, null, function (responseText) {
                        //  $(content).html(responseText);
                        $("ul").listview("refresh");
                    });
                    break;
                case ("samstag"):
                    $("#samstagContent").load(loadUrl, null, function (responseText) {
                        //  $(content).html(responseText);
                        $("ul").listview("refresh");
                    });
                    break;

                default:
                    break;

            } 



        }

    });
}

function getfachview(id) {

    var loadUrl = "http://localhost:52142/htwservice.ashx?function=" + "getfachview" + "&fach=" + id;


     $("#viewfachcontent")
        .load(loadUrl, null, function (responseText) {
            $("#viewfachcontent").html(responseText);
        });
}
