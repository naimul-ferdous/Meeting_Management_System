// <reference path="JSLib/jquery-1.4.1.js" />

var Message = {

    Prompt: function() {
        var yesNo;
        if (confirm("Do you want to proceed?.")) {
            yesNo = true;
        }
        else {
            yesNo = false;
        }
        return yesNo;
    },
     
    Success: function(event) {
        var _save = "Successfully saved.";
        var _update = "Successfully updated.";
        var _delete = "Successfully deleted.";
        var _cancel = "Successfully cancelled";
        var _reject = "Successfully rejected";
        var _add = "Successfully added";
        if (event == "save") {

            notif({
                msg: _save,
                type: "success",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "add"){
            notif({
                msg: _add,
                type: "success",
                position: 'center',
                autohide: false
            });

        }
        else if (event == "update") {

            notif({
                msg: _update,
                type: "success",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "delete") {
            notif({
                msg: _delete,
                type: "success",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "cancel") {
            notif({
                msg: _cancel,
                type: "success",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "reject") {
            notif({
                msg: _reject,
                type: "success",
                position: 'center',
                autohide: false
            });
        }

    },

    Error: function(event) {
        var _save = "Failed to save.";
        var _update = "Failed to update.";
        var _delete = "Failed to delete.";
        var _print = "Failed to print";
        var _cancel = "Failed to cancel";
        var _reject = "Failed to reject";
        var _select = "Please select a record.";
        var _Office = "Please Select an Office!";
        var _unknown = "Internal server error.";
        var _Id = "Id Must Be of 5 Digits";
        var _ErrorLogin = "Id or password is empty or incorrect!!!";
        var _add = "Failed to add.";

        //$('#lblMessage').attr("class", "");
        //$('#lblMessage').attr("class", "err");

        if (event == "save") {
            notif({
                msg: _save,
                type: "error",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "add") {
            notif({
                msg: _add,
                type: "success",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "update") {
            notif({
                msg: _update,
                type: "error",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "delete") {
            notif({
                msg: _delete,
                type: "error",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "print") {
            notif({
                msg: _print,
                type: "error",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "cancel") {
            notif({
                msg: _cancel,
                type: "error",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "reject") {
            notif({
                msg: _reject,
                type: "error",
                position: 'center',
                autohide: false
            });
        }
        else if (event == "select") {
            notif({
                msg: _select,
                type: "error",
                position: 'center',
                autohide: false
            });
        }

        else if (event == "unknown") {
            notif({
                msg: _unknown,
                type: "error",
                position: 'center',
                autohide: false
            });
        } else if (event == "Id") {

            notif({
                msg: _Id,
                type: "error",
                position: 'center',
                autohide: false
            });
        } else if (event == "LoginNotFound") {

            notif({
                msg: _ErrorLogin,
                type: "error",
                position: 'center',
                autohide: false
            });
        }

        else if (event == "Office") {

            notif({
                msg: _Office,
                type: "error",
                position: 'center',
                autohide: false
            });
        }

    },

    Warning: function(message) {
        notif({
            msg: message,
            type: "warning",
            position: 'center',
            autohide: false
        });
    },

    CustomSuccessMessage: function (msg) {
        $('#lblMessage').attr("class", "");
        $('#lblMessage').attr("class", "success");

        notif({
            msg: msg,
            type: "success",
            position: 'center',
            autohide: false
        });
    },

    CustomErrorMessage: function (msg) {
        $('#lblMessage').attr("class", "");
        $('#lblMessage').attr("class", "err");

        notif({
            msg: msg,
            type: "error",
            position: 'center',
            autohide: false
        });
    },

}
