$(function () {

    $(document).ready(function () {
        $("#telefone").inputmask("mask", { "mask": "(99) 9999-99999" });
        $("#celular").inputmask("mask", { "mask": "(99) 9999-99999" });
        $("#contato").inputmask("mask", { "mask": "(99) 9999-99999" });
        $("#cpf").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
        $('#rg').inputmask("mask", { "mask": "99.999.999-9" }, { reverse: true });
        $('#cnpj').inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
        $('#ei').inputmask("mask", { "mask": "999.999.999/9999" }, { reverse: true });
        $("#cep").inputmask("mask", { "mask": "99999-999" });
        $("#nascimento").inputmask("mask", { "mask": "99/99/9999" });
        $("#preco").inputmask("mask", { "mask": "999.999,99" }, { reverse: true });
        $("#valor").inputmask("mask", { "mask": "#.##9,99" }, { reverse: true });
        $("#ip").inputmask("mask", { "mask": "999.999.999.999" });
    });


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