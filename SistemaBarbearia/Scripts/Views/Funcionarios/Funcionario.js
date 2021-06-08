$(function () {

   

    var table = $('#listaFornecedores').DataTable({
        ajax: { url: $('#listaFornecedores').attr('data-url') },
        lengthMenu: false,
        lengthChange: false,
        language: {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        },
        columns: [

            { data: "Id" },
            { data: "nmFuncionario" },
            { data: "nrCelular" },

        ],
    });

    $('#searchForm').submit(function (e) {
        e.preventDefault();
        table.draw();
    });

});