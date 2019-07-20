var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);
    Manager.LoadVenue();

});

var Manager = {

    ResetForm: function () {
        $('#logisticForm')[0].reset();
    },

    SaveLogistic: function () {
       
            if (Message.Prompt()) {
                var jsonParam = $('#logisticForm').serialize();
                var serviceURL = "/Logistic/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("Saved");

                }
                else {
                    Manager.ResetForm();
                    $('#myModal').modal('hide');
                    Message.Success("save");
                    Manager.GetDataForTable(1);

                }
            }
            function onFailed(xhr, status, err) {
                Message.Exception(xhr);
            }
        S
    },

    EditLogistic: function (id) {
       
        if (!$('#logisticNameTxt').val()) {
            Message.Warning("Logistic name is required");
        }
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#logisticForm').serialize() + '&LogisticId=' + id;
                var serviceURL = "/Logistic/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }


            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("update");
                }
                else {
                    Manager.ResetForm();
                    $('#myModal').modal('hide');
                    Message.Success("update");
                    Manager.GetDataForTable(1);
                }
            }
            function onFailed(xhr, status, err) {
                Message.Exception(xhr);
            }
        }

    },

    DeleteLogistic: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { id: id };
            var serviceURL = "/Logistic/Delete/" + id;
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }

        function onSuccess(JsonData) {
            if (JsonData == 0) {
                Message.Error("delete");
            }
            else {
                Message.Success("delete");
                Manager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadVenue: function () {

        $.ajax({
            url: '/Logistic/GetVenue/',
            type: "GET",
            datatype: "Json",
            success: function (jsonData) {
                $('#VenueId').append('<option value="0" selected ="Selected">Select Department </option>');
                $.each(jsonData, function (key, value) {
                    $('#VenueId').append(
                        '<option value="' + value.VenueId + '">' + value.VenueName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },  


    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/Logistic/Index/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            console.log('OK');
            Manager.LoadDataTable(jsonData, refresh);
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function (data, refresh) {
        if (refresh == "0") {
            dTable = $('#tableElement').DataTable({
                dom: 'lB<"toolbar">frtip',
                buttons: [
                    {
                        extend: 'csvHtml5',
                        exportOptions: {
                            columns: [0, 1]
                        },
                        title: 'Logistic'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "text-left", targets: [0, 1, 2] }
                ],
                columns: [
                    {
                        data: 'LogisticName',
                        name: 'LogisticName',
                        title: 'Logistic Name',
                        width: 100,
                    },
                    {
                        data: 'Availbale',
                        name: 'Availbale',
                        title: 'Available',
                        width: 100,
                    },
                    {
                        name: 'Option',
                        title: 'Option',
                        width: 100,

                        render: function (data, type, row) {
                            var deleteBtn = '';
                            deleteBtn = '<span class="glyphicon glyphicon-trash spnDataTableDelete" id="deleteBtn" title="Click to delete"></span>';
                            return '<span class="glyphicon glyphicon-edit spnDataTableEdit id="editButton" title="Edit"></span>' + deleteBtn;
                        }

                    }

                ],
                data: data

            });
        } else {
            dTable.clear().rows.add(data).draw();
        }
    }
}

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    $('#logisticNameTxt').val(rowData.LogisticName);
    $('#availableTxt').val(rowData.Availbale);
    $('#VenueId').val(rowData.VenueId);
    _id = rowData.LogisticId;

    $('#saveButton').text('Update');
    $('#saveButton').removeClass('btn-success');
    $('#saveButton').addClass('btn-warning');
    $('#saveButton').prop('id', 'editButton');
    $('#myModal').modal('show');
});

$("#myModal").on('hidden.bs.modal', function () {
    $('#editButton').text('Save');
    $('#editButton').removeClass('btn-warning');
    $('#editButton').addClass('btn-success');
    $('#editButton').prop('id', 'saveButton');
});

$(document).on('click', '#editButton', function () {
    Manager.EditLogistic(_id);
    Manager.ResetForm();
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveLogistic();
    Manager.ResetForm();
    $('#myModal').modal('hide');
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().VenueId;
    Manager.DeleteLogistic(_id);
});
