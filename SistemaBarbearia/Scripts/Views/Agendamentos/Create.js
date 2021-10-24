
$(function () {

    var agenda = new Agenda();
    agenda.init();



    let dtAgendamento = $("#nrModelo")
    dtAgendamento.change(function () {
        let id = $("#Funcionario_IdFuncionario").val();
        agenda.verificaHorario(id);
        $("#dtAgendamentoAux").val(dtAgendamento.val())
    })

    let hora = $("#flhoraAgendamento")
    hora.change(function () {
        let id = $("#Funcionario_IdFuncionario").val();
        agenda.verificaHorario(id);

    })

    $(document).on('AfterLoad_Funcionario', function (e, data) {
        agenda.verificaHorario(data.id);
    });

    $("#Funcionario_IdFuncionario").change(function () {
        if (IsNullOrEmpty($(this).val())) {
            $("#divAddAgenda").hide();
        }
    })


    $("#divAddServico").hide();
});

Agenda = function () {
    self = this;
    this.init = function () {
        self.verificaHorario = function (id) {
            let dtAgendamento = $("#dtAgendamento");
            let hora = $("#flhoraAgendamento");
            if (!IsNullOrEmpty(dtAgendamento.val()) && !IsNullOrEmpty(hora.val()) && !IsNullOrEmpty(id)) {
                $.ajax({
                    dataType: 'json',
                    type: 'GET',
                    url: Action.verificahora,
                    data: { dtAgendamento: dtAgendamento.val(), hora: hora.val(), IdFuncionario: id },
                    success: function (data) {
                        $.notify({ message: data.message, icon: 'fa fa-exclamation' }, { type: data.type, z_index: 2000 });
                        if (data.type == "success") {
                            dtAgendamento = $("#dtAgendamento").val()
                            hora = $("#hora").val()
                            idFornecedor = $("#Funcionario.IdFuncionario").val()                            
                            $("#divAddServico").slideDown();
                        } else {
                            $("#divAddServico").hide();
                        }
                    },
                    error: function (request) {
                        alert("Erro ao buscar registro");
                    }
                });
            } else {
                $("#divAddServico").hide();
            }
        }
    }
}