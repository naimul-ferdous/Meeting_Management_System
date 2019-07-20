var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);


});

var Manager = {

    ResetForm: function () {
        $('#departmentForm')[0].reset();
    },

    SaveDepartment: function () {
       
                var jsonParam = $('#departmentForm').serialize();
                var serviceURL = "/Department/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
           
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
        
    },

    EditDepartment: function (id) {
        debugger;
        if (!$('#departmentNameTxt').val()) {
            Message.Warning("Department name is required");
        }
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#departmentForm').serialize() + '&DepartmentId=' + id;
                var serviceURL = "/Department/Edit/ ";
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

    DeleteDepartment: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { departmentId: id };
            var serviceURL = "/Department/Delete/";
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
        var serviceURL = "/Department/Index/";
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
                        title: 'Department'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "text-left", targets: [0, 1] }
                ],
                columns: [
                    {
                        data: 'DepartmentName',
                        name: 'DepartmentName',
                        title: 'Department Name',
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
    $('#departmentNameTxt').val(rowData.DepartmentName);
    _id = rowData.DepartmentId;

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
    Manager.EditDepartment(_id);
});

$(document).on('click', '#saveButton', function () {
    var departmentNameFlag = 0;
    if (!$('#departmentNameTxt').val()) {
        $('#departmentMsg').text('Department Name is required');
        departmentNameFlag = 1;
    }
    if (departmentNameFlag == 0) {
        Manager.SaveDepartment();
        Manager.ResetForm();
    }
});
function departmentNameMsgClear() {
    $('#departmentMsg').text(' ');
}
$(document).on('click', '#crossClose', function () {
    Manager.ResetForm();
    departmentNameMsgClear();
});

$(document).on('click', '#closeButton', function () {
    departmentNameMsgClear();
    Manager.ResetForm();
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().DepartmentId;
    Manager.DeleteDepartment(_id);
});


