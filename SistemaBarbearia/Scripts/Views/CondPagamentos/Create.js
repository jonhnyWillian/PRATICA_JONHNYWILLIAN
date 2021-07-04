
$(function () {

    Item.datatable = new tDataTable({

        table: {

            jsItem: "Itens_js",
            name: "tblItens",
            remove: true,
            edit: false,
            paginate: false,
            order: [[1, "asc"]],
            sortable: false,
            data: null,
            columns: [
                { data: "nrParcela" },
                { data: "qtDias" },
                { data: "txPercentual" },
                { data: "nmFormaPagto" },                
            ]
        }
    });

    $('#addItem').click(function () {

        Item.adicionar(Item.datatable);
        return false;
    });
});
let nr = 1;

var Item = {
    datatable: null,

    validarForm: function (datatable) {

        if (($("#qtDias").val() == "")) {
            alert('Por favor, informe quantos dias para pagamento');
            $("#qtDias").focus();
            return false;
        }

        if (($("#txPercent").val() == "")) {
            alert('Por favor, informe o percetual de cobrança');
            $("#txPercent").focus();
            return false;
        }

        if (($("#FormaPagto_id").val() == "")) {
            alert('Por favor, informe a forma de pagamento');
            $("#FormaPagto_id").focus();
            return false;
        }

        return true;
    },

    limparForm: function () {
        $("#FormaPagto_id").val('');
        $("#FormaPagto_text").val('');
        $("#qtDias").val('');
        $("#txPercent").val('');
    },

    getForm: function () {

        return {
            idFormaPagto: $('#FormaPagto_id').val(),
            nmFormaPagto: $('#FormaPagto_text').val(),
            qtDias: $('#qtDias').val(),
            txPercentual: $('#txPercent').val(),
        };
    },

    adicionar: function (datatable) {


        var form = Item.getForm();

        if (Item.validarForm(datatable)) {

            var item = {

                nrParcela: nr,
                idFormaPagto: form.idFormaPagto,
                nmFormaPagto: form.nmFormaPagto,
                qtDias: form.qtDias,
                txPercentual: form.txPercentual,
            }
            nr++;
            console.log(item);
            datatable.addItem(item);
            Item.limparForm();

            form.idFormaPagto.focus();
        }
    },


}