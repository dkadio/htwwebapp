$(document).ready(function () {
    //studiengang und semster vorbelegen
    setValues();


    $('#studiengang').bind('change', function () {
        var $this = $(this),
            val = $this.val();
        localStorage.setItem("studiengang", val);
    });
    $('#semester').bind('change', function () {
        var $this = $(this),
            val = $this.val();
        localStorage.setItem("semester", val);
    });

    //wahlfaehcer
    setLS();
});

function setLS() {
    
    var loadUrl = "http://localhost:52142/htwservice.ashx?function=" + "wahlfaecher" + "";

    $("#wahlfach")
        .load(loadUrl, null, function (responseText) {
            $("#wahlfach").html(responseText);
            //TODO wahlfächer refreshen.
            $("#wahlfach").page();
        });
}


function setValues() {
    // selectfelder abholen
    var se = $('#semester');
    var sg = $('#studiengang')
    // localstorage werte in das selectfeld laden und alle anderen abwaehlen
    se.val(localStorage.getItem("semester")).attr('selected', true).siblings('option').removeAttr('selected');
    sg.val(localStorage.getItem("studiengang")).attr('selected', true).siblings('option').removeAttr('selected');
    // initialisieren der menues
    se.selectmenu();
    sg.selectmenu();
    // ui refresh
    se.selectmenu("refresh", true);
    sg.selectmenu("refresh", true);
}



