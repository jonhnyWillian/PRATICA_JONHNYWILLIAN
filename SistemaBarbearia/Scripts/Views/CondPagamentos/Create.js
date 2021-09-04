
$(function () {

    var condPag = new CondPagamento();
    //condPag.init();


    $('#addItem').click(function () {
        condPag.adicionar();
        return false;
    });

});
function CondPagamento() {
    var self = this;
    this.init = function () {
        self.dataTable = new tDataTable({
            table: {
                jsItem: "Itens_js",
                name: "tblItens",
                remove: true,
                edit: true,
                paginate: false,
                columns: [
                    {
                        data: 'nrParcela',
                        sortable: false,
                        mRender: function (data) {
                            return '<div style="text-align: right">' + data + '</div>';
                        }
                    },
                    {
                        data: 'qtDias',
                        sortable: false,
                        mRender: function (data) {
                            return '<div style="text-align: right">' + data + '</div>';
                        }
                    },
                    {
                        data: 'txPercentual',
                        sortable: false,
                        mRender: function (data) {
                            return '<div style="text-align: right">' + parseFloat(data) + '</div>';
                        }
                    },
                    { data: "nmFormaPagto" },


                ]
            }
        });
    };
    this.getForm = function () {
        var item = {
            shared_function.CondPagamento.findById(data.item.idFormapg),
        };
        return item
    };

    this.adicionar = function () {
        var form = self.getForm();
            if (self.valid()) {
                let CondPag = {

                };
             
            }
        }
    };

    this.Edit = function (form, dt) {
     
        var item = {
           
        }
        self.dataTable.editItem(item);
        self.limpar(form);
    }

    this.valid = function () {
        let result = true;

       
        return result;
    };

    this.limpar = function () {
     
    }
}

