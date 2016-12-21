function initDialod(divId, width) {
    $(divId)
        .dialog({
            autoOpen: false,
            title: "Title",
            width: width,
            height: "auto",
            modal: true,
            show: "fade",
            hide: "fade",
            classes: {
                "ui-dialog": "DialogBox",
                "ui-dialog-titlebar": "Dialog-NoTitleBar"
            }
        });
}

function openPopup(divId) {
    $(divId).dialog("open");
}
