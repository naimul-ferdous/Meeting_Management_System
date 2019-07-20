var _id = null;
var dTable = null;

$(document).ready(function () {

    Manager.GetDataForTable(0);
    $('#clearButton').click(function () {
        Manager.ResetForm();
    });

});

var Manager = {

    ResetForm: function () {
        $('#externalMemberForm')[0].reset();
    },

    SaveExternalMember: function () {
        if (!$('#externalMemberNameTxt').val()) {
            Message.Warning("External Member name is required");
        }

        else if (!$('#emailTxt').val()) {
            Message.Warning("Email is required");
        }
        else if (!$('#phoneNumberTxt').val()) {
            Message.Warning("Phone number is required");
        }
        else if (!$('#professionTxt').val()) {
            Message.Warning("Profession name is required");
        }
        else if (!$('#addressTxt').val()) {
            Message.Warning("Address is required");
        }

        else {
            if (Message.Prompt()) {
                var jsonParam = $('#externalMemberForm').serialize();
                var serviceURL = "/ExternalMember/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }
        }

        function onSuccess(JsonData) {
            if (JsonData == "0") {
                Message.Error("Saved");

            }
            else {
                $('#myModal').modal('hide');
                Manager.ResetForm();
                Message.Success("save");
                Manager.GetDataForTable(1);
            }
        }
        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    EditExternalMember: function (id) {
        if (Message.Prompt()) {
            var jsonParam = $('#externalMemberForm').serialize() + '&ExternalMemberId=' + id;
            var serviceURL = "/ExternalMember/Edit/ ";
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

    },

    DeleteExternalMember: function (id) {
        debugger;
        if (Message.Prompt()) {
            var jsonParam = { externalMemberId: id };
            var serviceURL = "/ExternalMember/Delete/";
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

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/ExternalMember/Index/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
    
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
                        title: 'External Member'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "text-left", targets: [0, 1, 2, 3, 4, 5] }
                ],
                columns: [
                    {
                        data: 'ExternalMemberName',
                        name: 'ExternalMemberName',
                        title: 'External Member Name',
                        width: 100,
                    },
                    {
                        data: 'Email',
                        name: 'Email',
                        title: 'Email',
                        width: 100,
                    }, {
                        data: 'Phone',
                        name: 'Phone',
                        title: 'Contact No',
                        width: 100,
                    },
                    {
                        data: 'Profession',
                        name: 'Profession',
                        title: 'Profession',
                        width: 100,
                    },
                    {
                        data: 'Address',
                        name: 'Address',
                        title: 'Address',
                        width: 100,
                    },
                    {
                        name: 'Option',
                        title: 'Option',
                        width: 50,

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
    $('#externalMemberNameTxt').val(rowData.ExternalMemberName);
    $('#emailTxt').val(rowData.Email);
    $('#phoneNumberTxt').val(rowData.Phone);
    $('#professionTxt').val(rowData.Profession);
    $('#addressTxt').val(rowData.Address);
    _id = rowData.ExternalMemberId;



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

$(document).on('click', '#saveButton', function () {
    Manager.SaveExternalMember();
});

$(document).on('click', '#editButton', function () {
    Manager.EditExternalMember(_id);
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().ExternalMemberId;
    Manager.DeleteExternalMember(_id);
});