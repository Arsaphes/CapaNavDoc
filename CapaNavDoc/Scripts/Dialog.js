function ShowDialog(title, width, url, editFormId, dataTableId, newWindow) {
    $("<div></div>").dialog({
        autoOpen: true,
        modal: true,
        width: width,
        title: title,
        hide: "shake",
        show: "fade",
        open: function () {
            OpenHandler(this, url, newWindow);
        },
        buttons: {
            Annuler: function () {
                CancelHandler(this, editFormId);
            },
            Valider: function () {
                ValidateHandler(this, editFormId, dataTableId, SendAjaxRequest);
            }
        }
    });
}


function OpenHandler(container, url, newWindow) {
    // ReSharper disable once CoercedEqualsUsing
    if (newWindow == "undefined") newWindow = false;
    if (!newWindow) {
        $(container).load(url);
    } else {
        $(container).dialog("close");
        window.open(url, "Documentation PFD", "Settings");
    }
}

function CancelHandler(container, editFormId) {
    var form = $("#" + editFormId);
    $(container).dialog("close");
    form.remove();
}

function ValidateHandler(container, editFormId, dataTableId, ajaxRequestFunc) {
    if (!Validate(editFormId)) return;
    $(container).dialog("close");
    ajaxRequestFunc(editFormId, dataTableId);
    $("#" + editFormId).remove();
}


function SendAjaxRequest(editFormId, dataTableId) {
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
        contentType: false,
        processData: false,
        data: new FormData(form[0]),
        success: function() {
            waitingDialog.dialog("close");
            $("#" + dataTableId).dataTable().fnDraw();
        }
    });
}

function SendAjaxRequestSerialized(editFormId, dataTableId) {
    var form = $("#" + editFormId);
    $.ajax(
    {
        url: form.attr("action"),
        type: form.attr("method"),
        data: form.serialize(),
        success: function() {
            form.remove();
            var table = $("#" + dataTableId).dataTable();
            table.fnDraw();
        }
    });
}

function GetLoadingSpinner() {
    return $("" +
        "<div style='text-align: center; padding-top: 40px; padding-bottom: 20px;'>" +
        "<img src='/Content/Images/LoadingSpinner.gif' alt='Loading Spinner' />" +
        "<br/>" +
        "Veuillez patienter..." +
        "</div>");
}