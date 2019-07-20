var _id = null;
var dTable = null;

$(document).ready(function () {
    Manager.GetDataForTable(0);
    Manager.LoadCountry();
    Manager.ResetForm();
});

var Manager = {

    ResetForm: function() {
        $("#addressForm")[0].reset();
    },

    SaveAddress: function () {
        if (!$("#houseNoTxt").val()) {
            Message.Warning("HouseNo is required ");
        }
        else if (!$("#roadNoTxt").val()) {
            Message.Warning("RoadNo is required ");
        }
        else if (!$("#blockTxt").val()) {
            Message.Warning("block is required ");
        }
        else if (!$("#areaTxt").val()) {
            Message.Warning("area is required ");
        }
        else if (!$("#postCodeTxt").val()) {
            Message.Warning("post Code is required ");
        }
        else if (!$("#districtTxt").val()) {
            Message.Warning("District is required ");
        }
        else if (!$("#countryTxt").val()) {
            Message.Warning("Country is required ");
        }

        else {
            if (Message.Prompt()) {
                var jsonParam = $('#addressForm').serialize();
                var serviceURL = "/Address/Create/";
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

    EditAddress: function (id) {
        if (!$("#houseNoTxt").val()) {
            Message.Warning("HouseNo is required ");
        }
        else if (!$("#roadNoTxt").val()) {
            Message.Warning("RoadNo is required ");
        }
        else if (!$("#blockTxt").val()) {
            Message.Warning("block is required ");
        }
        else if (!$("#areaTxt").val()) {
            Message.Warning("area is required ");
        }
        else if (!$("#postCodeTxt").val()) {
            Message.Warning("post Code is required ");
        }
        else if (!$("#districtTxt").val()) {
            Message.Warning("District is required ");
        }
        else if (!$("#countryTxt").val()) {
            Message.Warning("Country is required ");
        }
        else {
            if (Message.Prompt()) {
                var jsonParam = $('#addressForm').serialize() + '&AddressId=' + id;
                var serviceURL = "/Address/Edit/ ";
                AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
            }


            function onSuccess(JsonData) {
                if (JsonData == "0") {
                    Message.Error("update");
                }
                else {
                    $('#myModal').modal('hide');
                    Manager.ResetForm();
                    Message.Success("update");
                    Manager.GetDataForTable(1);
                }
            }
            function onFailed(xhr, status, err) {
                Message.Exception(xhr);
            }
        }

    },

    DeleteAddress: function (id) {
        if (Message.Prompt()) {
            var jsonParam = { addressId: id };
            var serviceURL = "/Address/Delete/";
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
    LoadCountry: function () {
        $.ajax({
            url: '/Address/CountryCity/',
            type: "GET",
            datatype: "Json",
            success: function (jsonData) {
                $('#Country').append('<option value="0" selected ="Selected">Select Country </option>');
                $.each(jsonData, function (key, value) {
                    $('#Country').append(
                        '<option value="' + value.Country + '">' + value.Country + '</option>'
                    );
                });

            },
            error: function () {

            }
        });
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/Address/Index/";
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
                        title: 'Address'
                    }, 'print', 'pdfHtml5'
                ],

                scrollY: "300px",
                scrollX: true,
                scrollCollapse: true,
                lengthMenu: [[5, 10, 15, 40], [5, 10, 15, 40, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "dt-center", targets: [0, 1, 2, 3, 4, 5, 6, 7] }
                ],
                columns: [
                    {
                        data: 'HouseNo',
                        name: 'HouseNo',
                        title: 'House No',
                        width: 100,
                    },

                    {
                        data: 'RoadNo',
                        name: 'RoadNo',
                        title: 'Road No',
                        width: 100,
                    },

                    {
                        data: 'Block',
                        name: 'Block',
                        title: 'Block',
                        width: 100,
                    },

                    {
                        data: 'Area',
                        name: 'Area',
                        title: 'Area',
                        width: 100,
                    },

                    {
                        data: 'PostCode',
                        name: 'PostCode',
                        title: 'Post Code',
                        width: 100,
                    },

                    {
                        data: 'District',
                        name: 'District',
                        title: 'District',
                        width: 100,
                    },

                    {
                        data: 'Country',
                        name: 'Country',
                        title: 'Country',
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

$(document).on('click', '#saveButton', function () {
    Manager.SaveAddress();
});

$(document).on('click', '.spnDataTableEdit', function () {
    var rowData = dTable.row($(this).parent()).data();
    $('#houseNoTxt').val(rowData.HouseNo);
    $('#roadNoTxt').val(rowData.RoadNo);
    $('#blockTxt').val(rowData.Block);
    $('#areaTxt').val(rowData.Area);
    $('#postCodeTxt').val(rowData.PostCode);
    $('#districtTxt').val(rowData.District);
    $('#countryTxt').val(rowData.Country);
    _id = rowData.AddressId;

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
    Manager.EditAddress(_id);
});

$(document).on('click', '.spnDataTableDelete', function () {
    _id = dTable.row($(this).parents('tr')).data().AddressId;
    Manager.DeleteAddress(_id);
});