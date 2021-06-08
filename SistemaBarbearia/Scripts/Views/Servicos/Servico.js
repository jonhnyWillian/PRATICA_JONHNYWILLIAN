$(function () {


    $(document).ready(function () {
        $('#listaServico').DataTable({
            lengthMenu: false,
            lengthChange: false,
            language: {
                url: 'https://cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json',
            }
        });
    });

    $('#searchForm').submit(function (e) {
        e.preventDefault();
        table.draw();
    });


});