$(function () {
    vlTotalCompra = 0;
    vlSeguro = 0;
    vlDespesa = 0;
    vlFrete = 0;
    date = null;
    dateEntrega = null;
    var compra = new Compra();
    compra.init();

    $("#addProduto").click(function () {
        compra.addProdutoCompra();
        return false;
    });

    $(document).on("tblProdutoAfterDelete", function () {
        compra.calcTotalProduto();
        compra.clearProduto();
        return false;
    });

    $(document).on("tblProdutoOpenEdit", compra.openEditProduto);
    $(document).on("tblProdutoCancelEdit", compra.clearProduto);

    $("#CondicaoPagamento_btnGerarParcela").click(function () {
        compra.getparcelas();
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

    let dtAux = $("#dtEmissao")
    dtAux.change(function () {
        let dtString = dtAux.val();
        let dayArray = dtString.split("/");
        let day = dayArray[0];
        let month = (parseFloat(dayArray[1]) - 1);
        let year = dayArray[2];
        date = new Date(year, month, day).toJSON();
        $("#dtEmissaoAux").val(dtAux.val())
    });

    let dtEnt = $("#dtEntrega")
    dtEnt.change(function () {
        let dtString = dtEnt.val();
        let dayArray = dtString.split("/");
        let day = dayArray[0];
        let month = (parseFloat(dayArray[1]) - 1);
        let year = dayArray[2];
        dateEntrega = new Date(year, month, day).toJSON();
        $("#dtEntregaAux").val(dtEnt.val())
    });

    $("#btnSalvar").attr("disabled", true);
    $('input[name="CondicaoPagamento_Id"]').prop('disabled', true)
    $("#CondicaoPagamento_btn-localizar").hide();
    $("#divAddProduto").hide();

    let nrModelo = $("#nrModelo")
    nrModelo.change(function () {
        let id = $("#Fornecedor_IdFornecedor").val();
        compra.verificaNF(id);
        $("#nrModeloAux").val(nrModelo.val())
    })

    let nrSerie = $("#nrSerie")
    nrSerie.change(function () {
        let id = $("#Fornecedor_IdFornecedor").val();
        compra.verificaNF(id);
        $("#nrSerieAux").val(nrSerie.val())
    })

    let numero = $("#nrNota")
    numero.change(function () {
        let id = $("#Fornecedor_IdFornecedor").val();
        compra.verificaNF(id);
        $("#nrNotaAux").val(numero.val())
    })

    $(document).on('AfterLoad_Fornecedor', function (e, data) {
        compra.verificaNF(data.id);
    });

    $("#Fornecedor_IdFornecedor").change(function () {
        if (IsNullOrEmpty($(this).val())) {
            $("#divAddProduto").hide();
        }
    })

    $("#CondicaoPagamento_Id").change(function () {
        dtParcelas.clear();
        let idCondicao = $("#CondicaoPagamento_Id").val()
        if (IsNullOrEmpty(idCondicao)) {
            $("#divAddProduto").show();
            $('input[name="dtEmissao"]').prop('disabled', false);
            $('input[name="dtEntrega"]').prop('disabled', false)
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

    $("#Produto_nrQtd").change(function () {
        compra.calcTotalItem();
    })

    $("#Produto_vlCompra").change(function () {
        compra.calcTotalItem();
    })

    $("#vlSeguro").change(function () {
        compra.calcTotalProduto();
    })
    $("#vlFrete").change(function () {
        compra.calcTotalProduto();
    })
    $("#vlDespesa").change(function () {
        compra.calcTotalProduto();
    })
   


    $("#Produto_txDesconto").change(function () {
        compra.calcTotalItem();
    })

    //load
    let idCond = $("#CondicaoPagamento_Id").val()
    if (!IsNullOrEmpty(idCond)) {
        $("#flTblProdutos").val("S");
        $('input[name="dtEmissao"]').prop('disabled', true)
        $('input[name="dtEntrega"]').prop('disabled', true)
        $('input[name="CondicaoPagamento_Id"]').prop('disabled', false)
        $("#CondicaoPagamento_btn-localizar").show();
        $("#CondicaoPagamento_btnGerarParcela").attr('disabled', false)

        let dtString = $("#dtEmissao").val();
        let dayArray = dtString.split("/");
        let day = dayArray[0];
        let month = (parseFloat(dayArray[1]) - 1);
        let year = dayArray[2];
        date = new Date(year, month, day).toJSON();
        dtProdutos.atualizarItens();
        dtProdutos.atualizarGrid();
    }

    let idFor = $("#Fornecedor_IdFornecedor").val()
    if (!IsNullOrEmpty(idFor)) {
        $("#Fornecedor_btn-localizar").hide();
    }

    if (dtProdutos.length > 0) {
       $('input[name="CondicaoPagamento_Id"]').prop('disabled', false)
        $("#CondicaoPagamento_btn-localizar").show();
    }

    if (!IsNullOrEmpty(idCond) && dtParcelas.length > 0) {
        $("#btnSalvar").attr("disabled", false);
    }
});

Compra = function () {
    self = this;
    dtProdutos = null;
    dtParcelas = null;

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
                            let vlCompra = data.vlCompra.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
                            return vlCompra;
                        }
                    },
                    {
                        data: null,
                        mRender: function (data) {
                            let vlVenda = data.vlVenda.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
                            return vlVenda;
                        }
                    },
                    {
                        data: null,
                        mRender: function (data) {
                            return data.txDesconto.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        }
                    },
                    {
                        data: null,
                        mRender: function (data) {
                            let vlTotalCompra = (data.txDesconto * data.vlCompra) / 100;
                            vlTotalCompra = (data.vlCompra - vlTotalCompra) * data.nrQtd;
                            return vlTotalCompra.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        }
                    },
                ]
            },
        });
        self.calcTotalProduto();

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
                    { data: "IdFormaPagamento" },
                    { data: "dsFormaPagamento" },
                ]
            },
        });

        if (dtParcelas.length > 0) {
            $("#flFinalizar").prop("checked", true)
            let total = vlTotalCompra;
            let totalFormat = total.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
            $("#vlTotal").val(totalFormat);
        }
    }

    self.getModelProdutoCompra = function () {
        let vlVendaProduto = $("#Produto_vlVenda").val().replace(".", "").replace(",", ".");
        let vlVendaProdutoAux = parseFloat(vlVendaProduto);

        let vlCompraProduto = $("#Produto_vlCompra").val().replace(".", "").replace(",", ".");
        let vlCompraProdutoAux = parseFloat(vlCompraProduto);

        let qtProdutoAux = $("#Produto_nrQtd").val().replace(".", "").replace(",", ".");
        qtProdutoAux = parseFloat(qtProdutoAux);

        let txDesconto = $("#Produto_txDesconto").val().replace(".", "").replace(",", ".");
        let txDescontoAux = 0;

        let vlTotalAux = 0;

        if (!IsNullOrEmpty(txDesconto)) {
            txDescontoAux += parseFloat(txDesconto);
            let calcDesc = (txDescontoAux * vlCompraProdutoAux) / 100;
            vlTotalAux = vlCompraProdutoAux - calcDesc;
        } else {
            vlTotalAux = vlCompraProdutoAux;
        }
        var model = {
            IdProduto: $("#Produto_IdProduto").val(),
            dsProduto: $("#Produto_dsProduto").val(),
            vlVenda: vlVendaProdutoAux,
            vlCompra: vlCompraProdutoAux,
            nrQtd: qtProdutoAux,
            vlTotal: vlCompraProdutoAux * qtProdutoAux,
            txDesconto: txDescontoAux,
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

        else if (IsNullOrEmpty($("#Produto_vlCompra").val()) || $("#Produto_vlCompra").val() == 0) {
            $("#Produto_vlCompra").blink({ msg: "Informe o valor de compra" });
            $("#Produto_vlCompra").focus();
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
        $("#Produto_vlVenda").val("");
        $("#Produto_nrQtd").val("");
        $("#Produto_vlCompra").val("");
        $("#Produto_txDesconto").val("");
        $("#Produto_vlVenda").val("");
        $("#Produto_vlTotal").val("") 
        $('input[name="Produto_IdProduto"]').prop('disabled', false)
    }

    self.addProdutoCompra = function () {
        if (self.validProdutoCompra()) {
            let model = self.getModelProdutoCompra();
            let item = {
                IdProduto: model.IdProduto,
                dsProduto: model.dsProduto,
                nrQtd: model.nrQtd,
                vlVenda: model.vlVenda,
                vlCompra: model.vlCompra,
                txDesconto: model.txDesconto,
                vlTotal: model.vlTotal
            }
            self.saveProdutoCompra(item);
            self.clearProduto();
            self.calcTotalProduto();
        }
    }

    self.calcTotalProduto = function () {
        let total = 0;
        if (dtProdutos.length && dtProdutos.length > 0) {
            for (var i = 0; i < dtProdutos.length; i++) {
                let vlCompraDesconto = (dtProdutos.data[i].vlCompra * dtProdutos.data[i].txDesconto) / 100;
                vlCompraDesconto = dtProdutos.data[i].vlCompra - vlCompraDesconto;
                let totalProduto = vlCompraDesconto * dtProdutos.data[i].nrQtd;
                total += totalProduto;
            }
            $('input[name="CondicaoPagamento_Id"]').prop('disabled', false)
            $("#CondicaoPagamento_btn-localizar").show();            
            $('input[name="nrModelo"]').prop('disabled', true)
            $('input[name="nrSerie"]').prop('disabled', true)
            $('input[name="nrNota"]').prop('disabled', true)           
            $("#Fornecedor_btn").removeAttr('disabled', true)
            $("#Fornecedor_btn-localizar").hide();
        } else {
            $("#divAddProduto").show();            
            $('input[name="nrModelo"]').prop('disabled', false)
            $('input[name="nrSerie"]').prop('disabled', false)
            $('input[name="nrNota"]').prop('disabled', false)          
            $("#Fornecedor_btn").removeAttr('disabled', false)
            $('input[name="CondicaoPagamento_Id"]').prop('disabled', true)
            $("#CondicaoPagamento_btn-localizar").hide();
            $("#CondicaoPagamento_Id").val("")
            $("#CondicaoPagamento_text").val("")
            $("#CondicaoPagamento_btnGerarParcela").attr('disabled', true)
            $("#Fornecedor_btn-localizar").show();
        }

        total = Number.parseFloat(total);
        $("#ftp").text("Total: " + total);


        
        vlFrete = Number.parseFloat($("#vlFrete").val());
       
        vlDespesa = Number.parseFloat($("#vlDespesa").val());
     
        vlSeguro = Number.parseFloat($("#vlSeguro").val());

        let valorTotalOutros = Number.parseFloat(vlFrete) + vlDespesa + vlSeguro;
        let vlCompra_Outros = 0;
        if (!IsNullOrEmpty(vlSeguro))
        {
            vlCompra_Outros = valorTotalOutros + total;

        } else if (!IsNullOrEmpty(vlFrete))
        {
            vlCompra_Outros = valorTotalOutros + total;

        } else if (!IsNullOrEmpty(vlDespesa))
        {
            vlCompra_Outros = valorTotalOutros + total;

        } else {
            vlCompra_Outros += total;
        }
        
        vlTotalCompra = vlCompra_Outros.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 });
        $("#vlTotal").val(vlTotalCompra)
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

        if (IsNullOrEmpty(date)) {
            $.notify({ message: "Informe a data de emissão!", icon: 'fa fa-exclamation' }, { type: 'danger', z_index: 2000 });
            valid = false;
        } else if (IsNullOrEmpty(dateEntrega)) {
            $.notify({ message: "Informe a data de entrega!", icon: 'fa fa-exclamation' }, { type: 'danger', z_index: 2000 });
            valid = false;
        } else if (dateEntrega < date) {
            $.notify({ message: "A data de entrega não pode ser menor que a data de Emissão!", icon: 'fa fa-exclamation' }, { type: 'danger', z_index: 2000 });
            valid = false;
        }
        if (!dtParcelas.length && valid) {
            console.log($("#vlTotal").val());           
            $.ajax({
                dataType: 'json',
                type: 'GET',
                url: Action.getParcelas,
                data: { idCondicaoPagamento: $("#CondicaoPagamento_Id").val(), vlTotal: Number.parseFloat($("#vlTotal").val()), dtInicioParcela: date },
                success: function (data) {
                    $.notify({ message: data.message, icon: 'fa fa-exclamation' }, { type: 'success', z_index: 2000 });                 
                    self.setParcelas(data)
                    $("#btnSalvar").attr("disabled", false);
                    $('input[name="dtEmissao"]').prop('disabled', true)
                    $('input[name="dtEntrega"]').prop('disabled', true)
                },
                error: function (request) {
                    alert("Erro ao buscar registro");
                }
            });
        }
        else if (dtParcelas.length) {
            $.notify({ message: "Já foram geradas parcelas para esta Compra, verifique!", icon: 'fa fa-exclamation' }, { type: 'danger', z_index: 2000 });
        }
    }

    self.setParcelas = function (data) {
        let itens = data.parcelas;
        for (var i = 0; i < itens.length; i++) {
            let item = {
                nrParcela: itens[i].nrParcela,
                vlParcela: itens[i].vlParcela,
                dtVencimento: itens[i].dtVencimento,
                IdFormaPagamento: itens[i].IdFormaPagamento,
                dsFormaPagamento: itens[i].dsFormaPagamento,
            }
            dtParcelas.addItem(item);
        }
        $("#divParcelas").slideDown();
    }

    self.verificaNF = function (id) {
        let nrModelo = $("#nrModelo");
        let nrSerie = $("#nrSerie");
        let nrNota = $("#nrNota");
        if (!IsNullOrEmpty(nrModelo.val()) && !IsNullOrEmpty(nrSerie.val()) && !IsNullOrEmpty(nrNota.val()) && !IsNullOrEmpty(id)) {
            $.ajax({
                dataType: 'json',
                type: 'GET',
                url: Action.verificaNF,
                data: { nrModelo: nrModelo.val(), nrSerie: nrSerie.val(), nrNota: nrNota.val(), idFornecedor: id },
                success: function (data) {
                    $.notify({ message: data.message, icon: 'fa fa-exclamation' }, { type: data.type, z_index: 2000 });
                    if (data.type == "success") {                        
                        let nrModelo = $("#nrModelo").val()
                        $("#modeloAux").val(nrModelo)
                        let nrSerie = $("#nrSerie").val()
                        $("#serieAux").val(nrSerie)
                        let numero = $("#nrNota").val()
                        $("#nrNotaAux").val(numero)
                        let idFornecedor = $("#Fornecedor.IdFornecedor").val()
                        $("#IdFornecedor").val(idFornecedor)                        
                        $("#divAddProduto").slideDown();

                    } else {
                        $("#divAddProduto").hide();
                    }
                },
                error: function (request) {
                    alert("Erro ao buscar registro");
                }
            });
        } else {
            $("#divAddProduto").hide();
        }
    }

    self.calcTotalItem = function () {

        let vlCompraProduto = $("#Produto_vlCompra").val().replace(".", "").replace(",", ".");
        let vlCompraProdutoAux = parseFloat(vlCompraProduto);

        let qtProdutoAux = $("#Produto_nrQtd").val().replace(".", "").replace(",", ".");
        qtProdutoAux = parseFloat(qtProdutoAux);

        let txDesconto = $("#Produto_txDesconto").val().replace(".", "").replace(",", ".");
        let txDescontoAux = 0;

        if (!IsNullOrEmpty(vlCompraProduto) && !IsNullOrEmpty(qtProdutoAux)) {
            let total = 0;

            if (!IsNullOrEmpty(txDesconto)) {
                let desc = parseFloat(txDesconto);
                let txDesc = (desc * vlCompraProdutoAux) / 100
                vlCompraProdutoAux = vlCompraProdutoAux - txDesc;
            }
            total = vlCompraProdutoAux * qtProdutoAux;
            $("#Produto_vlTotal").val(total.toLocaleString('pt-br', { currency: 'BRL', minimumFractionDigits: 2, maximumFractionDigits: 2 }))
        } else {
            $("#Produto_vlTotal").val("")
        }
    }
}