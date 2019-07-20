$(document).ready(function () {
    $('#dateWiseMeetingButton').click(function () {
        Manager.GetMeeting();
    });
    //$('#dateFrom').datepicker({
       
    //});
    //$('#dateTo').datepicker({});
    //$('input[type=date]').datepicker({});
    Manager.LoadVenueDDL();
    Manager.LoadEmployeeDDL();
    Manager.DatePicker();

});
var Manager = {


    LoadVenueDDL: function () {
        debugger;
        $.ajax({
            url: '/Report/GetVenueInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#VenueId').append('<option value="0" selected ="Selected">Select Venue </option>');
                $.each(jsonData,
                    function (key, value) {
                        $('#VenueId').append(
                            '<option value="' + value.VenueId + '">' + value.VenueName + '</option>'
                        );
                    });
            },
            error: function () {

            }
        });
    },

    LoadEmployeeDDL: function () {
        $.ajax({
            url: '/Report/GetEmployeeInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#EmployeeId').append('<option value="0" selected ="Selected">Select Employee </option>');
                $.each(jsonData,
                    function (key, value) {
                        $('#EmployeeId').append(
                            '<option value="' + value.EmployeeId + '">' + value.EmployeeName + '</option>'
                        );
                    });
            },
            error: function () {

            }
        });
    },

    DatePicker: function () {

        $('#BeginningTime').datetimepicker({
            timepicker: false,
            format: 'm/d/Y',
            onShow: function (ct) {
                this.setOptions({
                    maxDate: AjaxManager.MDYToDashDMY($('#EndTime').val()) ? AjaxManager.MDYToDashDMY($('#EndTime').val()) : false
                });
            }
        });

        $('#EndTime').datetimepicker({
            timepicker: false,
            format: 'm/d/Y',
            onShow: function (ct) {
                this.setOptions({
                    minDate: AjaxManager.MDYToDashDMY($('#BeginningTime').val()) ? AjaxManager.MDYToDashDMY($('#BeginningTime').val()) : false
                });
            }
        });
    },

    GetMeeting: function () {
        var startDate = $('#BeginningTime').val();
        var endDate = $('#EndTime').val();
        var venueId = !$('#VenueId').val() ? 0 : $('#VenueId').val();
        var employeeId = !$('#EmployeeId').val() ? 0 : $('#EmployeeId').val();

        if (startDate == "") {
            Message.Warning('Date From is required');
        }
        else if (endDate == "") {
            Message.Warning('Date To is required');
        }
        else {
            window.open(location.protocol + '//' + location.host + '/Reports/Viewer/DateWiseMeeting.aspx?startDate=' + $('#BeginningTime').val() + '&endDate=' + $('#EndTime').val() + '&venueId=' + venueId + '&employeeId=' + employeeId, '_blank');

        }

    }
}