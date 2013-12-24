/*  hier wird der localstorage eingelesen*/
$(window).load(function () {
    loadStorage();
});

/* belege studiengang und semster vor wenns nicht vorbelegt ist*/
function loadStorage() {
    var studiengang = localStorage.getItem("studiengang");
    var semester = localStorage.getItem("semester");
    //alert(studiengang + "  " + semester);
    if (studiengang == null) {
        localStorage.setItem("studiengang", "ki");
    }
    if (semester == null) {
        localStorage.setItem("semester", "5");
    }

}