function InitDialogs(rootDivId, title, editFormId, width) {

    var uniqueDivId = $("div[id*=" + rootDivId + "]");
    for (var i = 0; i < uniqueDivId.length; i++) {
        InitDialog(uniqueDivId[i].id, title, editFormId, width);
    }
}

function InitDialog(divId, title, editFormId, width) {

    var divIdSharp = "#" + divId;

    var editFormIdSharp = "#" + editFormId;
    var id = $(divIdSharp + "-Opener").data("request-id");
    var link = $(divIdSharp).data("request-url") + "?id=" + id;

    $(divIdSharp)
        .dialog({
            autoOpen: false,
            modal: true,
            width: width,
            title: title,
            hide: "shake",
            show: "fade",
            open: function() { $(this).load(link); },
            buttons: {
                Annuler: function() { $(this).dialog("close"); },
                Valider: function() {
                    var form = $(editFormIdSharp);
                    if (!Validate()) return;
                    $.ajax(
                    {
                        url: form.attr("action"),
                        type: form.attr("method"),
                        data: form.serialize(),
                        success: function() { $(divIdSharp).dialog("close"); }
                    });
                }
            }
        });

    $(divIdSharp + "-Opener")
        .click(function() {
            $(divIdSharp).dialog("open");
        });

    $(divIdSharp + "-Closer")
        .click(function() {
            $(divIdSharp).dialog("close");
        });


};