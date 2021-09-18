
$(function () {

    var cond = new CondPagamento;
    cond.init();
    $("#addItem").click(function () {        
        cond.addItem();
    });

    $("#removeAll").click(function (e) {
        cond.removerTudo();
        e.preventDefault();
    });

    $(document).on("tblItensOpenEdit", cond.openEdit);
    $(document).on("tblItensCancelEdit", cond.clear);
});
CondPagamento = function () {

    self = this;

    var dtCondicao = null;
    var nrParcelaAux = 0;

    this.init = function () {
        dtCondicao = new tDataTable({
            table: {
                jsItem: "jsItens",
                name: "tblItens",
                remove: false,
                edit: true,
                order: [[1, "asc"]],
                columns: [
                    { data: "nrParcela" },
                    { data: "qtdDias" },
                    {
                        data: null,
                        mRender: function (data) {
                            let result = "";
                            if (data) {
                                result = data.txPercentual.toFixed(2).replace(".", ",");
                            }
                            return result;
                        }
                    },

                    { data: "dsFormaPagamento" },
                ]
            },
        });

        self.AtualizaTaxa(dtCondicao);
    }

    self.valid = function () {
        let valid = true;

        if (IsNullOrEmpty($("#qtdDias").val())) {
            $("#qtdDias").blink({ msg: "Informe a quantidade de dias" });
            valid = false;
        }

        if ($("#qtdDias").val() == 0 || $("#qtdDias").val() == "") {
            $("#qtdDias").blink({ msg: "Informe uma quantidade de dias válido" });
            valid = false;
        }
        let maior = 0;
        if (dtCondicao.data != null && dtCondicao.data.length) {
            for (var i = 0; i < dtCondicao.data.length; i++) {
                if (dtCondicao.data[i].qtdDias > maior) {
                    maior = dtCondicao.data[i].qtdDias;
                }
            }            
            if ($("#qtdDias").val() <= maior) {
                $("#qtdDias").blink({ msg: "Não é permitido adicionar uma parcela menor ou igual, verifique!" });
                valid = false;
            }
        }

        if (IsNullOrEmpty($("#txPercentual").val())) {
            $("#txPercentual").blink({ msg: "Informe o percentual" });
            valid = false;
        }

        if (IsNullOrEmpty($("#formaPagamento_Id").val())) {
            $("#formaPagamento_Id").blink({ msg: "Informe a condição de pagamento" });
            valid = false;
        }
        let txTotal = $("#txPercentualTotal").val();
        let txPercentual = $("#txPercentual").val();

        if (!IsNullOrEmpty(txPercentual)) {
            if (!IsNullOrEmpty(txTotal)) {
                txTotal = parseFloat(txTotal);
            }
            txPercentual = txPercentual.replace(",", ".");
            txPercentual = parseFloat(txPercentual);
            txTotal = parseFloat(txTotal);

            let total = 0;
            if (dtCondicao.isEdit) {
                total = txTotal - dtCondicao.dataSelected.item.txPercentual + txPercentual;
                if (total > 100) {
                    $("#txPercentualTotal").blink({ msg: "O valor total deve ser equivalente a 100%, verifique!" });
                    valid = false;
                } else {
                    if (valid == "true") {
                        $("#txPercentualTotal").val(total);
                        $("#txPercentualTotalAux").val(total);
                    }
                }
            } else {
                total = txTotal + txPercentual;
                if (total > 100) {
                    $("#txPercentualTotal").blink({ msg: "O valor total deve ser equivalente a 100%, verifique!" });
                    valid = false;
                } else {
                    if (valid == "true") {
                        $("#txPercentualTotal").val(total);
                        $("#txPercentualTotalAux").val(total);
                    }
                }
            }
        }

        return valid;
    }

    self.getModel = function () {
        let taxa = $("#txPercentual").val().replace(",", ".");
        taxa = parseFloat(taxa)
        var model = {
            idFormaPagamento: $("#formaPagamento_Id").val(),
            dsFormaPagamento: $("#formaPagamento_Text").val(),
            qtdDias: $("#qtdDias").val(),
            txPercentual: taxa,
        }
        return model;

    }

    self.clear = function () {
        $("#formaPagamento_Id").val('');
        $("#formaPagamento_Text").val('');
        $("#qtdDias").val('');
        $("#txPercentual").val('');
        $("#txPercentualTotal").val('');
    }

    self.addItem = function () {
        if (self.valid()) {
            var model = self.getModel();
            let item = {
                nrParcela: dtCondicao.isEdit ? dtCondicao.dataSelected.item.nrParcela : dtCondicao.length + 1, 
                idFormaPagamento: model.idFormaPagamento,
                dsFormaPagamento: model.dsFormaPagamento,
                qtdDias: parseFloat(model.qtdDias),
                txPercentual: model.txPercentual,
            }
            self.save(item)
            self.clear();
            self.AtualizaTaxa(dtCondicao);
        }
    }

    self.AtualizaTaxa = function (data) {

        let taxaTotal = 0;
        let dt = data.data;
        let aux = "";
        for (var i = 0; i < dt.length; i++) {
            if (typeof (dt[i].txPercentual) == "string") {
                aux = dt[i].txPercentual.replace(",", ".");
                aux = parseFloat(aux);
            } else {
                aux = dt[i].txPercentual;
            }
            taxaTotal += aux;
        }
        $("#txPercentualTotal").val(taxaTotal.toFixed(2).replace(".", ","));
        $("#txPercentualTotalAux").val(taxaTotal);
    }

    self.removerTudo = function () {
        if (dtCondicao.data == null || !dtCondicao.data.length) {
            $.notify({ message: "Não existem parcelas para serem removidas", icon: 'fa fa-exclamation' }, { type: 'danger', z_index: 2000, });
        } else {
            dtCondicao.data = null;
            $("#txPercentualTotal").val(0);
            $("#txPercentualTotalAux").val(0);
        }
    }

    self.openEdit = function (e, data) {
        let item = dtCondicao.dataSelected.item;
        $("#formaPagamento_Id").val(item.idFormaPagamento);
        $("#formaPagamento_Text").val(item.dsFormaPagamento);
        $("#qtdDias").val(item.qtdDias);
        $("#txPercentual").val(item.txPercentual.toFixed(2).replace(".", ","));        
    };

    self.save = function (data) {
        if (dtCondicao.isEdit) {
            dtCondicao.editItem(data);
        } else {
            dtCondicao.addItem(data);
        }
    };
}



