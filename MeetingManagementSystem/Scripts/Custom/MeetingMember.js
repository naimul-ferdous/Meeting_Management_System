


var meetingId;
$(document).ready(function() {
    $("#internalsTable").DataTable();
    $("#externalsTable").DataTable();
});
$(document).on('change', "#checkAllInternals", function () {
    $("input[name='internalIds']").prop("checked", this.checked);
});

$(document).on('change', "input[name = 'internalIds']", function () {
    if ($("input[name='internalIds']").length == $("input[name='internalIds']:checked").length) {
        $("#checkAllInternals").prop('checked', true);
    }
    else {
        $("#checkAllInternals").prop('checked', false);
    }
});

$(document).on('change', "#checkAllExternals", function () {
    $("input[name='externalIds']").prop("checked", this.checked);
});

$(document).on('change', "input[name = 'externalIds']", function () {
    if ($("input[name='externalIds']").length == $("input[name='externalIds']:checked").length) {
        $("#checkAllExternals").prop('checked', true);
    }
    else {
        $("#checkAllExternals").prop('checked', false);
    }
});



var tableBody = $('#MembersTable tbody');
$(document).on('click', '.btnAddMember', function () {
    GetSelected();
    if (meetingMembers.length > 0) {
        $.ajax({
            url: '/Meeting/MembersPartial/',
            type: "Post",
            datatype: "Json",
            data: { meetingMembers: meetingMembers },
            success: function (jsonData) {
                meetingMembers = [];
                $('#memberModal').modal('hide');
                $('#meetingMembers').empty();
                $('#meetingMembers').html(jsonData);
                $('#internalsPartialTable').DataTable();
                $('#externalsPartialTable').DataTable();
                Message.Success("save");

            },
            error: function () {
                Message.Error("Saved");
                $('#memberModal').modal('hide');
                meetingMembers = [];
                //alert("No members are added to meeting.");
            }
        });

    }

});
var _meetingId;
$('#openMemberModalButton').click(function () {

    _meetingId = $('input[name=MeetingId]').val();
    $.ajax({
        //url: '/MeetingMembers/AvailableEmployeesPartial/',
        url: '/Meeting/AddMemberPartial/',
        type: "GET",
        datatype: "Json",
        data: { meetingId: _meetingId },
        success: function (jsonData) {
            $('#availableEmployeesModal').empty();
            $('#availableEmployeesModal').html(jsonData);
            $('#allEmployeesTable').DataTable({
                columnDefs: [
                    { visible: false, targets: [1, 2] }
                ]
            });
            allExternalsTable = $('#allExternalsTable').DataTable(
                {
                    columnDefs: [
                        { visible: false, targets: [1, 2] }
                    ]
                }
                //{
                //    columnDefs: [{
                //        orderable: false,
                //        className: 'select-checkbox',
                //        targets: 0
                //    }],
                //    select: {
                //        style: 'os',
                //        selector: 'td:first-child'
                //    },
                //    //order: [[1, 'asc']]
                //}
            );
            $('#memberModal').modal('show');
        },
        error: function () {

        }
    });

});

var meetingMembers = [];
//var SelectedMembersVM;
function SelectedMembersVM(meetingId, employeeId) {
    this.MeetingId = meetingId;
    this.EmployeeId = employeeId;
}

function GetSelected() {
    $('#allEmployeesTable > tbody input[type="checkbox"]').each(function () {
        if ($(this).is(":checked")) {
            //SelectedMembersVM = new Object();
            //SelectedMembersVM.MeetingId = _meetingId;
            //SelectedMembersVM.EmployeeId = $(this).attr("Id");
            var obj = new SelectedMembersVM(_meetingId, $(this).attr("Id"));
            meetingMembers.push(obj);
        }
    });
    $('#allExternalsTable > tbody input[type="checkbox"]').each(function () {
        if ($(this).is(":checked")) {
            //var data = $(this).closest('tr').data();
            //console.log(data);
            //SelectedMembersVM = new Object();
            //SelectedMembersVM.MeetingId = _meetingId;
            //SelectedMembersVM.EmployeeId = $(this).attr("Id");
            var obj = new SelectedMembersVM(_meetingId, $(this).attr("Id"));
            meetingMembers.push(obj);
        }
    });
    //console.log(selected);

    //$('.checkboxes:checked').each(function () {
    //    selected.push($(this).attr('name'));

    //});
}

//$(document).onload(function () {
//    $('#MembersTable').DataTable();
//});

$(document).ready(function () {
   
    $('#MembersTable').DataTable();

 
});
var allExternalsTable;
var example;
function EveryTest() {
    var allEmployeesTable = $('#allEmployeesTable').DataTable({
        columnDefs: [
            { visible: false, targets: [1, 2] }
        ]
    });
    //var example= $('#example').DataTable({
    //     columnDefs: [{
    //         orderable: false,
    //         className: 'select-checkbox',
    //         targets: 0
    //     }],
    //     select: {
    //         style: 'os',
    //         selector: 'td:first-child'
    //     },
    //     //order: [[1, 'asc']]
    // });


    // var allExternalsTable = $('#allExternalsTable').DataTable();
    //allEmployeesTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
    //    var data = this.data();
    //    console.log("-Employee Id-" + data[1].find('input').val());

    //});
    allExternalsTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
        debugger;
        var data = this.data();
        //console.log("-External Id-" + data[1].$(cell).find('input').val());
        var checkbox = data[0];
        var text = $(checkbox).find('input[type=checkbox]').is(':checked');

        alert(text);
        alert(data[1]);
        //$(this).find('input[type="checkbox"]').prop('checked');
        //var data = this.data();
        // var isChecked = data[0].$(cell).find('input [type="checkbox"]').is(':checked');
        console.log(data);

        //data[0] = '* ' + data[0];

        //this.data(data);

    });

    //var oTable = $('#calltable').dataTable();
    //var rowcollection = allExternalsTable.$(".call-checkbox", { "page": "all" });
    //rowcollection.each(function (index, elem) {
    //    debugger;
    //    var checkbox_value = $(elem).val();
    //    alert(checkbox_value);
    //   // Do something with 'checkbox_value'
    //});

    //.checkboxes.selected()

    //var selectedIds = allExternalsTable.columns()[0];
    //console.log(selectedIds);

    //selectedIds.forEach(function (selectedId) {
    //    alert(selectedId);
    //});
}
function everyTest() {

    var memberDataTable = $('#MembersTable').DataTable();
    memberDataTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
        var data = this.data();
        console.log(data[0] + "-" + data[1]);

    });
}