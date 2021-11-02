$(function () {
    vlTotalVenda = 0;
    date = null;
    dateEntrega = null;
    var venda = new Venda();
    venda.init();

    $("#addProduto").click(function () {
        venda.addProdutoVenda();
        return false;
    });

    $(document).on("tblProdutoAfterDelete", function () {
        venda.calcTotalProdutoVenda();
        venda.clearProduto();
        return false;
    });

    $(document).on("tblProdutoOpenEdit", venda.openEditProduto);
    $(document).on("tblProdutoCancelEdit", venda.clearProduto);

    $("#CondicaoPagamento_btnGerarParcela").click(function () {
        venda.getparcelas();
        return false;
    });

    $(document).on('tblProdutoRowCallback', function (e, data) {
        let flTblProdutos = $("#flTblProdutos").val()
        if (flTblProdutos == "S") {
            let btn = $('td a[data-event=remove]', data.nRow);
            btn.attr('title', "Indisponível para alteração!");
            btn.attr('data-event', false);
            btn.removeClass().addClass("btn btn-secondary btn-sm");
            btn.find("i").removeClass().addClass("fa fa-info");
            btn.on('click', function (e) {
                e.preventDefault();
            })

            let btnEdit = $('td a[data-event=edit]', data.nRow);
            btnEdit.attr('title', "Indisponível para alteração!");
            btnEdit.attr('data-event', false);
            btnEdit.removeClass().addClass("btn btn-secondary btn-sm").css("width", "29px");
            btnEdit.find("i").removeClass().addClass("fa fa-info");
            btnEdit.click(function (e) {
                e.preventDefault();
            });
        }
        return false;
    });

    let dtAgendamento = $("#dtAgendamento")
    dtAgendamento.change(function () {
        let dtString = dtAgendamento.val();
        let dayArray = dtString.split("/");
        let day = dayArray[0];
        let month = (parseFloat(dayArray[1]) - 1);
        let year = dayArray[2];
        date = new Date(year, month, day).toJSON();
        $("#dtAgendamentoAux").val(dtAgendamento.val())
    });



    $("#btnSalvar").attr("disabled", true);
    $('input[name="CondicaoPagamento_Id"]').prop('disabled', true)
    $('input[name="Cliente_IdCliente"]').prop('disabled', true)
    $('input[name="Servico_IdServico"]').prop('disabled', true)
   // $("#CondicaoPagamento_btn-localizar").hide();
    $("#divAddProduto").show();

    let nrModelo = $("#nrModelo")
    nrModelo.change(function () {     
        $("#nrModeloAux").val(nrModelo.val())
    })

    let nrSerie = $("#nrSerie")
    nrSerie.change(function () {       
        $("#nrSerieAux").val(nrSerie.val())
    })


    $("#Servico_IdServico").change(function () {
        if (IsNullOrEmpty($(this).val())) {
            $("#divAddServico").show();
        }
    })



    $("#CondicaoPagamento_Id").change(function () {
        dtParcelas.clear();
        let idCondicao = $("#CondicaoPagamento_Id").val()
        if (IsNullOrEmpty(idCondicao)) {
            $("#divAddProduto").show();
            $('input[name="dtAgendamento"]').prop('disabled', false);        
            $("#flTblProdutos").val("")
        } else {
            $("#divAddProduto").hide();
        }
        dtProdutos.atualizarItens();
        dtProdutos.atualizarGrid();
    })

    $(document).on('AfterLoad_CondicaoPagamento', function (e, data) {
        $("#flTblProdutos").val("S")
        $("#divAddProduto").hide();
        dtParcelas.clear();
        dtProdutos.atualizarItens();
        dtProdutos.atualizarGrid();
    });

    $("#Produto_vlVenda").change(function () {
        venda.calcTotalItem();
    })


    //load
    let idCond = $("#CondicaoPagamento_Id").val()
    if (!IsNullOrEmpty(idCond)) {
        $("#flTblProdutos").val("S");
        $('input[name="dtAgendamento"]').prop('disabled', true)   
        $('input[name="CondicaoPagamento_Id"]').prop('disabled', false)
        //$("#CondicaoPagamento_btn-localizar").show();
        //$("#CondicaoPagamento_btnGerarParcela").attr('disabled', false)

        let dtString = $("#dtAgendamento").val();
        let dayArray = dtString.split("/");
        let day = dayArray[0];
        let month = (parseFloat(dayArray[1]) - 1);
        let year = dayArray[2];
        date = new Date(year, month, day).toJSON();
        dtProdutos.atualizarItens();
        dtProdutos.atualizarGrid();
    }

    let idC = $("#Cliente_IdCliente").val()
    if (!IsNullOrEmpty(idC)) {
        $("#Cliente_btn-localizar").hide();
    }

    let idS = $("#Servico_IdServico").val()
    if (!IsNullOrEmpty(idS)) {        
        $("#Servico_btn-localizar").hide();
    }
    //if (dtProdutos.length > 0) {
    //    $('input[name="CondicaoPagamento_Id"]').prop('disabled', false)
    //    $("#CondicaoPagamento_btn-localizar").show();
    //}

    //if (!IsNullOrEmpty(idCond) && dtParcelas.length > 0) {
    //    $("#btnSalvar").attr("disabled", false);
    //}
});

Venda = function () {
    self = this;
    dtProdutos = null;
    dtParcelas = null;
    dtServico = null;
    this.init = function () {

        dtProdutos = new tDataTable({
            table: {
                jsItem: "jsProdutos",
                name: "tblProduto",
                remove: true,
                edit: true,
                order: [[1, "asc"]],
                columns: [
                    { data: "IdProduto" },
                    { data: "dsProduto" },
                    {
                        data: null,
                        mRender: function (data) {
                            return data.nrQtd;
                        }
                    },
                    {
                        data: null,
                        mRender: function (data) {
                            return data.vlVenda.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        }
                    },
                    {
                        data: null,
                        mRender: function (data) {
                            let vlTotalVenda = (data.vlVenda * data.nrQtd);
                            return vlTotalVenda.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        }
                    },
                ]
            },
        });
        self.calcTotalProdutoVenda();

        dtParcelas = new tDataTable({
            table: {
                jsItem: "jsParcelas",
                name: "tblParcela",
                order: [[0, "asc"]],
                columns: [
                    { data: "nrParcela" },
                    {
                        data: null,
                        mRender: function (data) {
                            return data.vlParcela.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        }
                    },
                    {
                        data: null,
                        mRender: function (data) {
                            return JSONDate(data.dtVencimento);
                        }
                    },
                    { data: "dsFormaPagamento" },
                ]
            },
        });

        if (dtParcelas.length > 0) {
            $("#flFinalizar").prop("checked", true)
            let vlServico = $("#Servico_vlServico").val();
            let total = vlTotalCompra + vlServico;
            let totalFormat = total.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
            $("#vlTotal").val(totalFormat);
        }
    }

    self.getModelProdutoVenda = function () {
        let vlVendaProduto = $("#Produto_vlVenda").val();
        let vlVendaProdutoAux = parseFloat(vlVendaProduto);
        let qtProdutoAux = $("#Produto_nrQtd").val();
        qtProdutoAux = parseFloat(qtProdutoAux);

        var model = {
            IdProduto: $("#Produto_IdProduto").val(),
            dsProduto: $("#Produto_dsProduto").val(),
            vlVenda: vlVendaProdutoAux,
            
            nrQtd: qtProdutoAux,
            vlTotal: vlVendaProdutoAux * qtProdutoAux,
        };
        return model;
    }

    self.validProdutoCompra = function () {
        let valid = true;

        if (IsNullOrEmpty($("#Produto_IdProduto").val()) || $("#Produto_IdProduto").val() == "") {
            $("#Produto_IdProduto").blink({ msg: "Informe o produto" });
            $("#Produto_IdProduto").focus();
            valid = false;
        }

        else if (IsNullOrEmpty($("#Produto_nrQtd").val()) || $("#Produto_nrQtd").val() == "" || $("#Produto_nrQtd").val() == 0) {
            $("#Produto_nrQtd").blink({ msg: "Informe a quantidade" });
            $("#Produto_nrQtd").focus();
            valid = false;
        }

        if (!dtProdutos.isEdit) {
            if (dtProdutos.exists("IdProduto", $("#Produto_IdProduto").val())) {
                $("#Produto_IdProduto").blink({ msg: "Produto já informado, verifique!" });
                valid = false;
            }
        }

        return valid;
    }

    self.clearProduto = function () {
        $("#Produto_IdProduto").val("");
        $("#Produto_dsProduto").val("");        
        $("#Produto_nrQtd").val("");
        $("#Produto_vlVenda").val("");
        $("#Produto_vlTotal").val("") // Produto_vlTotal
        $('input[name="Produto_IdProduto"]').prop('disabled', false)
    }

    self.addProdutoVenda = function () {
        if (self.validProdutoCompra()) {
            let model = self.getModelProdutoVenda();
            let item = {
                IdProduto: model.IdProduto,
                dsProduto: model.dsProduto,
                nrQtd: model.nrQtd,
                vlVenda: model.vlVenda,                          
                vlTotal: model.vlTotal
            }
            self.saveProdutoCompra(item);
            self.clearProduto();
            self.calcTotalProdutoVenda();
        }
    }

    self.calcTotalProdutoVenda = function () {
        let total = 0;
        let vlServico = $("#Servico_vlServico").val().replace(".", "").replace(",", ".");
        vlTotalServico = parseFloat(vlServico);

        if (dtProdutos.length && dtProdutos.length > 0) {
            for (var i = 0; i < dtProdutos.length; i++) {
                let totalProdutoVenda = dtProdutos.data[i].vlVenda * dtProdutos.data[i].nrQtd;
                total += totalProdutoVenda;
            }
            $('input[name="CondicaoPagamento_Id"]').prop('disabled', false)
            //$("#CondicaoPagamento_btn-localizar").show();
            //desabilita campos nota fiscal
            $('input[name="nrModelo"]').prop('disabled', true)
            $('input[name="nrSerie"]').prop('disabled', true)            
            $('input[name="Cliente.IdCliente"]').prop('disabled', true)
            $("#Cliente_btn").removeAttr('disabled', true)
            $("#Cliente_btn-localizar").hide();
        } else {
            //reaabilita campos nota fiscal
            $('input[name="nrModelo"]').prop('disabled', false)
            $('input[name="nrSerie"]').prop('disabled', false)
            $('input[name="Cliente.IdCliente"]').prop('disabled', false)
            $("#Cliente_btn").removeAttr('disabled', false)

            $('input[name="CondicaoPagamento_Id"]').prop('disabled', true)
            //$("#CondicaoPagamento_btn-localizar").hide();

            $("#CondicaoPagamento_Id").val("")
            $("#CondicaoPagamento_text").val("")
            $("#CondicaoPagamento_btnGerarParcela").attr('disabled', true)

            $("#Cliente_btn-localizar").show();
        }
        
        totalProdutoVenda = total.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
        $("#ftp").text("Total: " + total);
        vlTotalVenda = total.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
        let TotalServicoProduto = parseFloat(vlTotalVenda);
        ServicoProdutovlTotal = vlTotalServico + TotalServicoProduto

        $("#vlTotal").val(ServicoProdutovlTotal.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 }))
    }

    self.openEditProduto = function (e, data) {
        let item = dtProdutos.dataSelected.item;
        $("#Produto_IdProduto").val(item.IdProduto);
        $("#Produto_dsProduto").val(item.dsProduto);
        $("#Produto_vlVenda").val(item.vlVenda.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#Produto_nrQtd").val(item.nrQtd.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#Produto_vlCompra").val(item.vlCompra.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#Produto_txDesconto").val(item.txDesconto.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $('input[name="Produto_IdProduto"]').prop('disabled', true)
        self.calcTotalItem();
    }

    self.saveProdutoCompra = function (data) {
        if (dtProdutos.isEdit) {
            dtProdutos.editItem(data);
        } else {
            dtProdutos.addItem(data)
        }
    }

    self.getparcelas = function () {
        let valid = true;

        if (!dtParcelas.length && valid) {
            console.log(Number.parseFloat($("#vlTotal").val()));
            $.ajax({
                dataType: 'json',
                type: 'GET',
                url: Action.getParcelas,                
                data: { idCondicaoPagamento: $("#CondicaoPagamento_Id").val(), vlTotal: Number.parseFloat($("#vlTotal").val()), dtInicialParcala: date},                
                success: function (data) {
                    $.notify({ message: data.message, icon: 'fa fa-exclamation' }, { type: 'success', z_index: 2000 });
                    self.setParcelas(data)
                    $("#btnSalvar").attr("disabled", false);
                    $('input[name="dtAgendamento"]').prop('disabled', true)                
                },
                error: function (request) {
                    alert("Erro ao buscar registro");
                }
            });
        }
        else if (dtParcelas.length) {
            $.notify({ message: "Já foram geradas parcelas para esta Venda, verifique!", icon: 'fa fa-exclamation' }, { type: 'danger', z_index: 2000 });
        }
    }

    self.setParcelas = function (data) {
        let itens = data.parcelas;
        for (var i = 0; i < itens.length; i++) {
            let item = {
                nrParcela: itens[i].nrParcela,
                vlParcela: itens[i].vlParcela,
                dtVencimento: itens[i].dtVencimento,
                idFormaPagamento: itens[i].idFormaPagamento,
                dsFormaPagamento: itens[i].dsFormaPagamento,
            }
            dtParcelas.addItem(item);
        }
        $("#divParcelas").slideDown();
    }

    self.calcTotalItem = function () {

        let vlCompraProduto = $("#Produto_vlCompra").val().replace(".", "").replace(",", ".");
        let vlCompraProdutoAux = parseFloat(vlCompraProduto);

        let qtProdutoAux = $("#Produto_nrQtd").val();
        qtProdutoAux = parseFloat(qtProdutoAux);

        if (!IsNullOrEmpty(vlCompraProduto) && !IsNullOrEmpty(qtProdutoAux)) {
            let total = 0;
            total = vlCompraProdutoAux * qtProdutoAux;
            $("#Produto_vlTotal").val(total.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 }))
        } else {
            $("#Produto_vlTotal").val("")
        }
    }
}