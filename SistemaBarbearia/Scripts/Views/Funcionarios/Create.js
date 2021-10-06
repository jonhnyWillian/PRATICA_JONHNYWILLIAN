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
        $("#dtDemissao").inputmask("mask", { "mask": "99/99/9999" });
        $("#dtAdimissao").inputmask("mask", { "mask": "99/99/9999" });
        $("#preco").inputmask("mask", { "mask": "999,99" }, { reverse: true });
        $("#valor").inputmask("mask", { "mask": "#.##9,99" }, { reverse: true });
        $("#ip").inputmask("mask", { "mask": "999.999.999.999" });
    });

    $("#cpf").on('change', function () {
        if (!ValidCPF($("#cpf").val())) {
            $.notify({ message: "Informe um CPF válido!", icon: 'fa fa-exclamation' }, { type: 'danger', z_index: 2000, });
            $("#cpf").val("");
        } else {
            $.notify({ message: "CPF válido!", icon: 'fa fa-exclamation' }, { type: 'success', z_index: 2000, });
        }
    });


});