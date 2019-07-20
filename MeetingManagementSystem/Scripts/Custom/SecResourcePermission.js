var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);
    Manager.LoadRoleDDL();
   // Manager.LoadResourceDDL();


});

var Manager = {
    LoadRoleDDL: function () {
        $.ajax({
            url: '/SecRole/GetRoleInfo/',
            type: "GET",
            datatype: "Json",
            success: function (jsonData) {
                $('#SecRoleId').empty();
                $('#SecRoleId').append('<option value="0" selected ="Selected">Select Role </option>');
                $.each(jsonData, function (key, value) {
                    $('#SecRoleId').append(
                        '<option value="' + value.SecRoleId + '">' + value.RoleName + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    //ResetForm: function () {
    //    $('#secResourcePermissionForm')[0].reset();
    //},

    //LoadRoleDDL: function () {
        
    //    $.ajax({
    //        url: '/SecResourcePermission/GetRoleInfo/',
    //        type: "POST",
    //        datatype: "Json",
    //        success: function (jsonData) {
    //            $('#SecRoleId').append('<option value="0" selected ="Selected">Select Role </option>');
    //            $.each(jsonData, function (key, value) {
    //                $('#SecRoleId').append(
    //                    '<option value="' + value.SecRoleId + '">' + value.RoleName + '</option>'
    //                );
    //            });

    //        },
    //        error: function () {

    //        }
    //    });
    //},

    LoadResourceDDL: function () {
        debugger;
        $.ajax({
            url: '/SecResourcePermission/GetResourceInfo/',
            type: "POST",
            datatype: "Json",
            success: function (jsonData) {
                $('#SecResourceId').append('<option value="0" selected ="Selected">Select Resource </option>');
                $.each(jsonData, function (key, value) {
                    $('#SecResourceId').append(
                        '<option value="' + value.SecResourceId + '">' + value.Name + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    SaveResourcePermission: function () {
        
            if (Message.Prompt()) {
                var jsonParam = $('#secResourcePermissionForm').serialize();
                var serviceURL = "/SecResourcePermission/Create/";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }

            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("Saved");

                } else {


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

    EditResourcePermission: function (id) {
       

            if (Message.Prompt()) {
                var jsonParam = $('#secResourcePermissionForm').serialize() + '&SecResourcePermissionId=' + id;
                var serviceURL = "/SecResourcePermission/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }


            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("update");
                } else {
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

    DeleteResourcePermission: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { secResourcePermissionId: id };
            var serviceURL = "/SecResourcePermission/Delete/";
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
        var serviceURL = "/SecResourcePermission/GetAllSecResourcePermission/";
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
                        title: 'SecResourcePermission'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "dt-center", targets: [0, 1, 2, 3, 4, 5, 6, 7, 8,9 ,10, 11, 12, 13, 14] }
                ],
                columns: [
                    {
                        data: 'SecRoleId',
                        name: 'SecRoleId',
                        title: 'SecRole Name',
                        width: 80,
                    },

                    {
                        data: 'SecResourceId',
                        name: 'SecResourceId',
                        title: 'SecResourceId',
                        width: 30,
                    }, {
                        data: 'FileName',
                        name: 'FileName',
                        title: 'FileName',
                        width: 30,
                    },{
                        data: 'MenuName',
                        name: 'MenuName',
                        title: 'MenuName',
                        width: 30,
                    },{
                        data: 'DisplayName',
                        name: 'DisplayName',
                        title: 'DisplayName',
                        width: 30,
                    },{
                        data: 'ModuleId',
                        name: 'ModuleId',
                        title: 'ModuleId',
                        width: 30,
                    },{
                        data: 'Order',
                        name: 'Order',
                        title: 'Order',
                        width: 30,
                    },{
                        data: 'Level',
                        name: 'Level',
                        title: 'Level',
                        width: 30,
                    },{
                        data: 'ActionUrl',
                        name: 'ActionUrl',
                        title: 'ActionUrl',
                        width: 30,
                    },

                    {
                        data: 'Status',
                        name: 'Status',
                        title: 'Status',
                        width: 50,
                    },

                    {
                        data: 'CreatedBy',
                        name: 'CreatedBy',
                        title: 'Created By',
                        width: 80,
                    },

                    {
                        data: 'CreatedDate',
                        name: 'CreatedDate',
                        title: 'Created Date',
                        width: 90,
                    },

                    {
                        data: 'ModifiedBy',
                        name: 'ModifiedBy',
                        title: 'Modified By',
                        width: 80,
                    },

                    {
                        data: 'ModifiedDate',
                        name: 'ModifiedDate',
                        title: 'Modified Date',
                        width: 90,
                    },

                    {
                        name: 'Option',
                        title: 'Option',
                        width: 30,

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
    var createdDate = moment(rowData.CreatedDate);
    var formatCreatedDate = createdDate.format('YYYY-MM-DD');
    var modifiedDate = moment(rowData.ModifiedDate);
    var formatModifiedDate = modifiedDate.format('YYYY-MM-DD');
    $('#SecRoleId').val(rowData.RoleName);
    $('#SecResourceId').val(rowData.SecResourceId);
    $('#fileNameTxt').val(rowData.FileName);
    $('#menuNameTxt').val(rowData.MenuName);
    $('#displayNameTxt').val(rowData.DisplayName);
    $('#moduleIdTxt').val(rowData.ModuleId);
    $('#orderTxt').val(rowData.Order);
    $('#levelTxt').val(rowData.Level);
    $('#actionUrlTxt').val(rowData.ActionUrl);
    $('#CreatedBy').val(rowData.CreatedBy);
    $('#CreationDateTime').val(formatCreatedDate);
    $('#modifiedByTxt').val(rowData.ModifiedBy);
    $('#ModificationDateTime').val(formatModifiedDate);
    $('#StatusId').prop('checked', rowData.Status);
    _id = rowData.SecResourcePermissionId;

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

$(document).on('change', '#StatusId', function () {
    debugger;
    if ($(this).prop("checked") == true) {

        $('#Status').val('true');
    } else {
        $('#Status').val('false');
    }

});
$(document).on('click', '#editButton', function () {
    Manager.EditResourcePermission(_id);
});

$(document).on('click', '#saveButton', function () {
    Manager.SaveResourcePermission();
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().SecResourcePermissionId;
    Manager.DeleteResourcePermission(_id);
});

//Tree View

var tree;
tree = $('#tree').tree({
    primaryKey: 'id',
    uiLibrary: 'bootstrap4',

    checkboxes: true
});


var ResourceManager = {
    GetCheckedNodes: function () {
        $("#parentId").find("checkbox").each(function () {
            if ($(this).prop('checked') == true) {
                //do something
            }
        });

        var obj;
        var result = [];

       // var nodeList = tree.find('li [data-role="node"] ');
        var nodeList = tree.find('li');
        $.each(nodeList, function () {
            obj = new Object();
            obj.id = $(this).closest('li').data('id');
            obj.text = $(this).find('[data-role="display"]').text();
            obj.checked = $(this).find('input[type="checkbox"]').prop('checked');
            result.push(obj);
        });

        return result;
    },
    GetData: function (q) {
        $.ajax({
            url: '/ResourceTree/Get/',
            type: "GET",
            datatype: "Json",
            data: { roleId: q },
            success: function (jsonData) {
               
                tree.render(jsonData);
            },
            error: function () {

            }
        });
    },
    SaveCheckedNodes: function () {
        var checkedIds = ResourceManager.GetCheckedNodes();
        $.ajax({
            url: '/ResourceTree/SaveCheckedNodes_V2/',
            data: { resourceRoleList: checkedIds },
            method: 'POST',
            success: function (jsonData) {
                if (jsonData == true) {
                    Message.Success("update");
                }
            },
            error: function () {

            }
        }).fail(function () {
            Message.Error("update");
        });
    }
};

$('#btnSave').on('click', function () {

    ResourceManager.SaveCheckedNodes();

});

$('#SecRoleId').change(function () {
    
    var q = $("#SecRoleId").val();
    ResourceManager.GetData(q);

});
