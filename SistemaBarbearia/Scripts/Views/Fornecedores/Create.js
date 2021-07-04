$(function () {


    if ($("#flTipo").val() == "F") {
        $(".fisica").show();
        $(".juridica").hide();
    } else {
        $(".fisica").hide();
        $(".juridica").show();
    }

    $("#flTipo").change(function () {
        if ($("#flTipo").val() == "F") {
            $(".fisica").slideDown();
            $(".juridica").slideUp();
        } else {
            $(".fisica").slideUp();
            $(".juridica").slideDown();
        }

    });


});