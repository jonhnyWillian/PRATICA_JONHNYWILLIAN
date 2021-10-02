document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var initialLocaleCode = 'pt-br';
    var localeSelectorEl = document.getElementById('locale-selector');
    var now = new Date();
    var date = new Date(now.getFullYear(), now.getMonth() + 1, 1);
    var d = date.getDate();
        m = date.getMonth();
        y = date.getFullYear();

    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            locale: 'pt-br',
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'


        },
       
       /* initialDate: '2020-09-25',*/
        locale: initialLocaleCode,
        buttonIcons: false, // mostrar o texto anterior / seguinte
        weekNumbers: true,
        navLinks: true, // pode clicar nos nomes do dia / semana para navegar pelas visualizações
        editable: true,
        dayMaxEvents: true, // permitir link "mais" quando muitos eventos
        navLinks: true, // pode clicar nos nomes do dia / semana para navegar pelas visualizações
        selectable: true,
        selectMirror: true,
        selectable: true,
        select: function (start, end) {
            selectedEvent = {
                idAgenda: 0,
                dtAgendamento: start,
                title: '',
                description: '',
                //start: start,
                //end: end,
                allDay: false,
                color: ''
            };       
            openAddEditForm();
            //$('#agendamento').fullCalendar('unselect');
        },
        eventClick: function (calEvent, jsEvent, view) {
            selectedEvent = calEvent;
            start: new Date(y, m, d),
            $('#myModal #eventTitle').text(calEvent.title);
            var $description = $('<div/>');
            $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
            if (calEvent.end != null) {
                $description.append($('<p/>').html('<b>End:</b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
            }
            $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));
            $('#myModal #pDetails').empty().html($description);

            $('#myModal').modal();
        },
        editable: true,
        editable: true,
        eventDrop: function (event) {
            var data = {
                idAgenda: event.idAgenda,
           
                Subject: event.title,
                Start: event.start.format('DD/MM/YYYY HH:mm A'),
                End: event.end != null ? event.end.format('DD/MM/YYYY HH:mm A') : null,
                Description: event.description,
                ThemeColor: event.color,
                IsFullDay: event.allDay
            };
            SaveEvent(data);
        }
    });

    calendar.render();

    $('#btnEdit').click(function () {
        //Open modal dialog for edit event
        openAddEditForm();
    })
    $('#btnDelete').click(function () {
        if (selectedEvent != null && confirm('Are you sure?')) {
            $.ajax({
                type: "POST",
                url: '/Agendamento/DeleteEvent',
                data: { 'idAgenda': selectedEvent.idAgenda },
                success: function (data) {
                    if (data.status) {
                        //Refresh the calender
                        FetchEventAndRenderCalendar();
                        $('#myModal').modal('hide');
                    }
                },
                error: function () {
                    alert('Failed');
                }
            })
        }
    })

    //$('#dtp1,#dtp2').datetimepicker({
    //    format: 'DD/MM/YYYY HH:mm A'
    //});

    $('#chkIsFullDay').change(function () {
        if ($(this).is(':checked')) {
            $('#divEndDate').hide();
        }
        else {
            $('#divEndDate').show();
        }
    });

    function openAddEditForm() {
        if (selectedEvent != null) {
            $('#idAgenda').val(selectedEvent.idAgenda);          
            //$('#txtStart').val(selectedEvent.start.format('DD/MM/YYYY HH:mm A'));     
            $('#txtEnd').val(selectedEvent.end != null ? selectedEvent.end.format('DD/MM/YYYY HH:mm A') : '');
            $('#txtDescription').val(selectedEvent.description);
            $('#ddThemeColor').val(selectedEvent.color);
        }
        $('#myModal').modal('hide');
        $('#myModalSave').modal();
    }

    $('#btnSave').click(function () {
        //Validation/
        if ($('#txtSubject').val().trim() == "") {
            alert('Subject required');
            return;
        }
        if ($('#txtStart').val().trim() == "") {
            alert('Start date required');
            return;
        }
        if ($('#chkIsFullDay').is(':checked') == false && $('#txtEnd').val().trim() == "") {
            alert('End date required');
            return;
        }
        else {
            var startDate = moment($('#txtStart').val(), "DD/MM/YYYY HH:mm A").toDate();
            var endDate = moment($('#txtEnd').val(), "DD/MM/YYYY HH:mm A").toDate();
            if (startDate > endDate) {
                alert('Invalid end date');
                return;
            }
        }

        var data = {
          
            idCliente: $("#Servico_idCliente").val(),
            nmCliente: $("#Servico_nmCliente").val(),

            idServico: $("#Servico_IdServico").val(),
            dsServico: $("#Servico_dsServico").val(),

            idFuncionario: $("#Servico_Idfuncionario").val(),
            dsFuncionario: $("#Servico_dsfuncionario").val(),
            Start: $('#txtStart').val().trim(),
            End: $('#chkIsFullDay').is(':checked') ? null : $('#txtEnd').val().trim(),
            Description: $('#txtDescription').val(),
            ThemeColor: $('#ddThemeColor').val(),
            IsFullDay: $('#chkIsFullDay').is(':checked')
        }
        SaveEvent(data);
        // função de chamada para enviar dados para o servidor
    })

    function SaveEvent(data) {
        $.ajax({
            type: "POST",
            url: '/Agendamento/SaveEvent',
            data: data,
            success: function (data) {
                if (data.status) {
                    //Refresh the calender
                    FetchEventAndRenderCalendar();
                    $('#myModalSave').modal('hide');
                }
            },
            error: function () {
                alert('Failed');
            }
        })
    }
})