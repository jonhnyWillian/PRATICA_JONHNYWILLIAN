$(function () {

    var table = $('#listaFuncionarios').DataTable({
        ajax: { url: $('#listaFuncionarios').attr('data-url') },
        lengthMenu: false,
        lengthChange: false,
        language: {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        },
        columns: [
            { data: "Id" },
            { data: "nmFornecedor" },
            { data: "nmFornecedor" },

        ],
    });

    $('#searchForm').submit(function (e) {
        e.preventDefault();
        table.draw();
    });


    

});
