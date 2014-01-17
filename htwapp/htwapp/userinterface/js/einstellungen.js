//man koennte die studiengaenge und den andern scheiss noch dynamisch machen

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
            //set old values


            $(".checkbox").change(function () {
                //speicher die wahlfächer im localstorage
              //  localStorage.removeItem("wahlfaecher");
                alert($(this).is(":checked"));

                if (localStorage.getItem("wahlfaecher") == null) {
                    //wahlfaecher exisiterit nicht -> leg es an
                    localStorage.setItem("wahlfaecher", "");
                    
                } else {
                    if ($(this).is(":checked")) {
                        //hinzufuegen in localstorage
                        localStorage.setItem("wahlfaecher", localStorage.getItem("wahlfaecher") + "." + $(this).attr("id"));
                        alert(localStorage.getItem("wahlfaecher"));
                        alert($(this).attr("id"));
                    } else {
                        //entfernen aus localstorage
                        // suche in wahlfaecher nach substring "." + id
                    }
                }
                

            });
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




