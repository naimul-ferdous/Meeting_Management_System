$(document).ready(function () {
    $('#meetingButton').click(function () {
        Manager.GetMeeting();
    });

    Manager.LoadVenueDDL();
    Manager.DatePicker();

});
var Manager = {
    LoadVenueDDL: function () {
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

    DatePicker: function () {
        $('#BeginningTime').datetimepicker({
            timepicker: false,
            format: dtpDTFomat,
            onShow: function (ct) {
                this.setOptions({
                    maxDate: AjaxManager.MDYToDashDMY($('#EndTime').val()) ? AjaxManager.MDYToDashDMY($('#EndTime').val()) : false
                });
            }
        });

        $('#EndTime').datetimepicker({
            timepicker: false,
            format: dtpDTFomat,
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
        window.open(location.protocol + '//' + location.host + '/Reports/Viewer/MeetingViewer.aspx? _blank');
        //window.open(location.protocol + '//' + location.host + '/Report/Viewer/MeetingViewer.aspx?startDate=' + startDate + '&endDate=' + endDate, '_blank');

    }
}