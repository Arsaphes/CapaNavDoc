﻿// ReSharper disable CoercedEqualsUsing

function ShowDialog(dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow, divHtmlContent) {
    $("<div id='ShowDialog'></div>").dialog({
        autoOpen: true,
        modal: true,
        width: dialogWidth,
        title: dialogTitle,
        hide: "fade",
        show: "fade",
        open: function () {
            OpenHandler(this, actionName, newWindow, divHtmlContent);
        },
        buttons: {
            Annuler: function () {
                CancelHandler(this, editFormId);
            },
            Valider: function () {
                ValidateHandler(this, dialogTitle, dialogWidth, actionName,  editFormId, dataTableId, newWindow, SendAjaxRequest);
            }
        }
    });
}


function OpenHandler(container, actionName, newWindow, divHtmlContent) {
    
    if (newWindow == "undefined") newWindow = false;
    if (!newWindow) {
        if (typeof divHtmlContent == "undefined") {
            var waitingDialog = GetLoadingSpinnerDialog(GetLoadingSpinnerContent);
            $(container).load(actionName, function() {
                if ($("#ErrorMessage").length) {
                    $(container).dialog({
                        buttons: {
                            OK: function() {
                                $(container).dialog("close");
                                $(container).dialog("destroy").remove();
                            }
                        },
                        title: "Petit problème..."
                    });
                }
                waitingDialog.dialog("close");
                waitingDialog.dialog("destroy").remove();
            });
        } else {
            $(container).html(divHtmlContent);
        }
    } else {
        $(container).dialog("close");
        $(container).dialog("destroy").remove();
        window.open(actionName, "Documentation PFD", "Settings");
    }
}

function CancelHandler(container, editFormId) {
    var form = $("#" + editFormId);
    $(container).dialog("close");
    $(container).dialog("destroy").remove();
    form.remove();
}

function ValidateHandler(container, dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow, ajaxRequestFunc) {
    //$(container).dialog("close");
    ajaxRequestFunc(container, dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow);
    //$("#" + editFormId).remove();
}


function SendAjaxRequest(container, dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow) {
    var form = $("#" + editFormId);

    //var waitingDialog = GetLoadingSpinnerContent().dialog({
    //    autoOpen: true,
    //    modal: true,
    //    dialogClass: "hideTitleBar",
    //    width: 240,
    //    title: "",
    //    hide: "fade",
    //    show: "fade"
    //});
    var waitingDialog = GetLoadingSpinnerDialog(GetLoadingSpinnerContent);

    //$(".ui-dialog-titlebar").hide();
    $(".hideTitleBar div:first").remove();

    $.ajax(
    {
        url: form.attr("action"),
        type: form.attr("method"),
        contentType: false,
        processData: false,
        data: new FormData(form[0]),
        success: function(result) {
            if (result.success) {
                waitingDialog.dialog("close");
                waitingDialog.dialog("destroy").remove();
                $(container).dialog("destroy").remove();
                $("#" + dataTableId).dataTable().fnDraw();
            } else {
                //ShowDialog(dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow, result);
                waitingDialog.dialog("close");
                waitingDialog.dialog("destroy").remove();
                $(container).dialog("destroy").remove();
                ShowDialog(dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow, result);
            }
        }
    });
}

function SendAjaxRequestSerialized(container, dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow) {
    var form = $("#" + editFormId);

    var waitingDialog = GetLoadingSpinner().dialog({
        autoOpen: true,
        modal: true,
        width: 240,
        title: "",
        hide: "fade",
        show: "fade"
    });
    $(".ui-dialog-titlebar").hide();

    $.ajax(
    {
        url: form.attr("action"),
        type: form.attr("method"),
        data: form.serialize(),
        success: function(result) {
            //form.remove();
            //var table = $("#" + dataTableId).dataTable();
            //table.fnDraw();
            if (result.success) {
                waitingDialog.dialog("close");
                waitingDialog.dialog("destroy").remove();
                $(container).dialog("destroy").remove();
                $("#" + dataTableId).dataTable().fnDraw();
            } else {
                waitingDialog.dialog("close");
                waitingDialog.dialog("destroy").remove();
                $(container).dialog("destroy").remove();
                ShowDialog(dialogTitle, dialogWidth, actionName, editFormId, dataTableId, newWindow, result);
            }
        }
    });
}

function GetLoadingSpinnerDialog(getLoadingSpinnerContentFunc) {
        return getLoadingSpinnerContentFunc().dialog({
        autoOpen: true,
        modal: true,
        dialogClass: "hideTitleBar",
        width: 240,
        title: "",
        hide: "fade",
        show: "fade"
    });
}

function GetLoadingSpinnerContent() {
    return $("" +
        "<div id='LoadingSpinner' style='text-align: center; padding-top: 40px; padding-bottom: 20px;'>" +
        "<img src='/Content/Images/LoadingSpinner.gif' alt='Loading Spinner' />" +
        "<br/>" +
        "Veuillez patienter..." +
        "</div>");
}