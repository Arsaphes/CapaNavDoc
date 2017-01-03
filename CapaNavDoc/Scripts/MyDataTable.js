function GetShowDialog(title, width, url, editFormId, dataTableId) {
    var showDialog = "ShowDialog(\"";
    showDialog += title;
    showDialog += "\", \"";
    showDialog += width;
    showDialog += "\", \"";
    showDialog += url;
    showDialog += "\", \"";
    showDialog += editFormId,
    showDialog += "\", \"";
    showDialog += dataTableId;
    showDialog += "\")";
    return showDialog;
}

function GetHtmlImage(icon, alt, dialogTitle, dialogWidth, url, editFormId, dataTableId) {
    var result = "<img src=";
    result += "\'" + icon + "\' ";
    result += "alt=\'" + alt + "\' ";
    result += "onclick=\'" + GetShowDialog(dialogTitle, dialogWidth, url, editFormId, dataTableId) + "\' />";  
    return result;
}

function  GetHtmlLink(cssClass, text, dialogTitle, dialogWidth, url, editFormId, dataTableId) {
    var result = "<a class=\'";
    result += cssClass;
    result += "\' onclick=\'" + GetShowDialog(dialogTitle, dialogWidth, url, editFormId, dataTableId) + "\'>";
    result += text;
    result += "</a>";
    return result;
}


function SetDataTable(dtId, ajaxSrc, colDef, uUrl, uFrmTitle, uFrmW, dUrl, dFrmTitle, dFrmW) {

    var editForm = "EditForm";
    var confirmForm = "ConfirmationForm";

    var idColumn = [{"sName": "ID", "visible": false}];
    var specialColumns = [
        {   "sName": "Update",
            "bSortable": false,
            "mRender": function(data, type, full) {
                var url = uUrl + "?id=" + full[0];
                return GetHtmlImage("/Content/Icons/Pencil-icon.png", "Pencil", uFrmTitle, uFrmW, url, editForm, dtId);
            }},
        {   "sName": "Delete",
            "bSortable": false,
            "mRender": function(data, type, full) {
                var url = dUrl + "?id=" + full[0];
                return GetHtmlImage("/Content/Icons/Close-2-icon.png", "Cross", dFrmTitle, dFrmW, url, confirmForm, dtId);
            }}];
    var all = idColumn.concat(colDef).concat(specialColumns);

    $("#" + dtId).dataTable({
            "bServerSide": true,
            "sAjaxSource": ajaxSrc,
            "bProcessing": true,
            "aoColumns": all,
            //"fnServerParams": function (aoData) {
            //    aoData.push({ "name": "mycolumn1", "value": $('#col1').val() });
            //    aoData.push({ "name": "mycolumn2", "value": $('#col2').val() });
            //},
            "columnDefs": [{ "className": "CenteredColumn", "targets": [all.length-2, all.length-1] }]});
    //$("#col1, #col2")
    //    .bind("keyup",
    //        function () {
    //            table.fnDraw();
    //        });
};

function SetMasterDataTable(dtId, ajaxSrc, colDef, uUrl, uFrmTitle, uFrmW, dUrl, dFrmTitle, dFrmW, dvUrl) {

    var editForm = "EditForm";
    var confirmForm = "ConfirmationForm";

    var idColumn = [
        {   "sName": "ID",
            "visible": false
        },
        {   "bSortable": false,
            "bSearchable": false,
            "mRender": function(data, type, full) {
            return '<img src="/Content/Icons/Add-Green-Button-icon.png" alt="Expand/Collapse" rel="' + full[0] + '"/>';
            }}];
    var specialColumns = [
        {   "sName": "Update",
            "bSortable": false,
            "mRender": function (data, type, full) {
                var url = uUrl + "?id=" + full[0];
                return GetHtmlImage("/Content/Icons/Pencil-icon.png", "Pencil", uFrmTitle, uFrmW, url, editForm, dtId); 
            }},
        {   "sName": "Delete",
            "bSortable": false,
            "mRender": function (data, type, full) {
                var url = dUrl + "?id=" + full[0];
                return GetHtmlImage("/Content/Icons/Close-2-icon.png", "Cross", dFrmTitle, dFrmW, url, confirmForm, dtId);
            }}];
    var all = idColumn.concat(colDef).concat(specialColumns);

    var table = $("#" + dtId)
        .dataTable({
            "bServerSide": true,
            "sAjaxSource": ajaxSrc,
            "bProcessing": true,
            "aoColumns": all,
            "columnDefs": [{ "className": "CenteredColumn", "targets": [all.length - 2, all.length - 1] }]
        });

    $("#" + dtId + " tbody").on("click", "tr td img", function () {
        var nTr = this.parentNode.parentNode;
        var image = this;
        if (this.src.match("Minus-icon")) {
            this.src = "/Content/Icons/Add-Green-Button-icon.png";
            table.fnClose(nTr);
        }
        else {
            this.src = "/Content/Icons/Minus-icon.png";
            var id = $(this).attr("rel");
            $.get(dvUrl, { id: id }, function (details) { table.fnOpen(nTr, details, "DetailedRow") });
            $("#" + dtId + " tbody").on("click", "tr td input[type=button]", function() {
                var form = $("#DetailForm");
                $.ajax(
                {
                    url: form.attr("action"),
                    type: form.attr("method"),
                    data: form.serialize(),

                    success: function () {
                        form.remove();
                    }
                });
                image.src = "/Content/Icons/Add-Green-Button-icon.png";
                table.fnClose(nTr);
            });
        }
    });
};

function SetEquipmentDataTable(dtId, ajaxSrc, colDef, uUrl, uFrmTitle, uFrmW, dUrl, dFrmTitle, dFrmW, uUrl2, uFrmTitle2, uFrmW2, uUrl3, uFrmTitle3, uFrmW3) {

    var editForm = "EditForm";
    var confirmForm = "ConfirmationForm";
    var monitoringCssClass = "Cursor-Pointer";

    var idColumn = [{"sName": "ID", "visible": false}];
    var specialColumns = [
        {   "sName": "CentersActions",
            "bSortable": false,
            "mRender": function(data, type, full) {
                var url = uUrl3 + "?id=" + full[0];
                return GetHtmlImage("/Content/Icons/Zoom-icon.png", "Magnifier", uFrmTitle3, uFrmW3, url, editForm, dtId);
            }},
        {   "sName": "Monitoring",
            "bSortable": false,
            "mRender": function(data, type, full) {
                var url = uUrl2 + "?id=" + full[0];
                return GetHtmlLink(monitoringCssClass, full[10], uFrmTitle2, uFrmW2, url, editForm, dtId);
            }},
        {   "sName": "Update",
            "bSortable": false,
            "mRender": function(data, type, full) {
                var url = uUrl + "?id=" + full[0];
                return GetHtmlImage("/Content/Icons/Pencil-icon.png", "Pencil", uFrmTitle, uFrmW, url, editForm, dtId);
            }},
        {   "sName": "Delete",
            "bSortable": false,
            "mRender": function(data, type, full) {
                var url = dUrl + "?id=" + full[0];
                return GetHtmlImage("/Content/Icons/Close-2-icon.png", "Cross", dFrmTitle, dFrmW, url, confirmForm, dtId);
            }}];
    var all = idColumn.concat(colDef).concat(specialColumns);

    $("#" + dtId).dataTable({
            "bServerSide": true,
            "sAjaxSource": ajaxSrc,
            "bProcessing": true,
            "aoColumns": all,
            "columnDefs": [{ "className": "CenteredColumn", "targets": [all.length-4, all.length-2, all.length-1] }]});
};