function ShowDialog(title, width, url, editFormId, dataTableId) {
    $("<div></div>").dialog({
        autoOpen: true,
        modal: true,
        width: width,
        title: title,
        hide: "shake",
        show: "fade",
        open: function () {
            $(this).load(url);
        },
        buttons: {
            Annuler: function () {
                var form = $("#" + editFormId);
                form.remove();
                $(this).dialog("close");
            },
            Valider: function () {
                var form = $("#" + editFormId);
                if (!Validate(editFormId)) return;
                $.ajax(
                {
                    url: form.attr("action"),
                    type: form.attr("method"),

                    contentType: false,
                    processData: false,
                    data: new FormData(form[0]),

                    //data: form.serialize(),




                    success: function () {
                        form.remove();
                        var table = $("#" + dataTableId).dataTable();
                        table.fnDraw();
                    }
                });
                $(this).dialog("close");
            }
        }
    });
}