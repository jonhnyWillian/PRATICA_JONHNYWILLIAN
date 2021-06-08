$(function () {


    $("#flTipoPessoa").change(function () {       
       
        if ($("#flTipoPessoa").val() == "F") {
           
            $("#lblJuridica").hide(); 
            $("#lblFisica").show();

        } else if ($("#flTipoPessoa").val() == "J") {

            $("#lblFisica").hide();
        
            $("#lblJuridica").show();
        } 
    });


    



});