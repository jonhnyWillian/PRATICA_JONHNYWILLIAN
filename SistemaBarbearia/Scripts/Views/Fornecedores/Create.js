$(function () {


    $("#flTipoPessoa").change(function () {       
       
        if ($("#flTipoPessoa").val() == "F") {
           
            $("#lblJuridica").hide(); 
            $("#lblFisica").show();

            $("#lblJuridicaNome").hide();
            $("#lblFisicaNome").show();

            $("#lblJuridicaCNPJ").hide();
            $("#lblFisicaCPF").show();

            $("#lblJuridicaIE").hide();
            $("#lblFisicaRG").show();
         

        } else if ($("#flTipoPessoa").val() == "J") {

            $("#lblFisica").hide();
            $("#lblJuridica").show();        

            $("#lblFisicaNome").hide();
            $("#lblJuridicaNome").show();

            $("#lblFisicaCPF").hide();
            $("#lblJuridicaCNPJ").show();

            $("#lblJuridicaIE").show();
            $("#lblFisicaRG").hide();
         
        } 
    });


    



});