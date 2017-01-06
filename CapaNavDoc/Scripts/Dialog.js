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
    form.remove();
    $(container).dialog("close");
}

function ValidateHandler(container, editFormId, dataTableId, ajaxRequestFunc) {
    if (!Validate(editFormId)) return;
    ajaxRequestFunc(editFormId, dataTableId);
    $(container).dialog("close");

}


function SendAjaxRequest(editFormId, dataTableId) {
    var form = $("#" + editFormId);
    $.ajax(
    {
        url: form.attr("action"),
        type: form.attr("method"),
        contentType: false,
        processData: false,
        data: new FormData(form[0]),
        success: function() {
            form.remove();
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