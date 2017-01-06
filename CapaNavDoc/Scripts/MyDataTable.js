var editForm = "EditForm";
var confirmForm = "ConfirmationForm";

function GetIdColumn() {
    return [{ "sName": "ID", "visible": false }];
}

function GetImageColumn(icon, alt, url, formTitle, formWidth, editForm, dataTableId, newWindow) {
    return [
        {
            "bSortable": false,
            "mRender": function(data, type, full) {
                var fullUrl = url + "?id=" + full[0];
                return GetHtmlImage(icon, alt, formTitle, formWidth, fullUrl, editForm, dataTableId, newWindow);
            }
        }
    ];
}

function GetDateLinkColumn(url, formTitle, formWidth, editForm, dataTableId, colNumber, newWindow) {
    var monitoringCssClassRed = "Cursor-Pointer Background-Red";
    var monitoringCssClassGreen = "Cursor-Pointer Background-Green";
    return [
        {
            "bSortable": false,
            "mRender": function(data, type, full) {
                var fullUrl = url + "?id=" + full[0];
                var dateItems = full[colNumber].split("-");
                var date = new Date(parseInt(dateItems[2]), parseInt(dateItems[1]) - 1, parseInt(dateItems[0]));
                var today = new Date();
                var cssClass = date > today ? monitoringCssClassGreen : monitoringCssClassRed;
                return GetHtmlLink(cssClass, full[9], formTitle, formWidth, fullUrl, editForm, dataTableId, newWindow);
            }
        }
    ];
}

function GetEmptyColumns(nbCol) {
    var cols = [];
    for (var i = 0; i < nbCol; i++) {
        cols = cols.concat([null]);
    }
    return cols;
}

function GetShowDialog(title, width, url, editFormId, dataTableId, newWindow) {
    var showDialog = "ShowDialog(\"";
    showDialog += title.replace("'","&#39;");
    showDialog += "\", \"";
    showDialog += width;
    showDialog += "\", \"";
    showDialog += url;
    showDialog += "\", \"";
    showDialog += editFormId,
    showDialog += "\", \"";
    showDialog += dataTableId;
    showDialog += "\", \"";
    showDialog += newWindow;
    showDialog += "\")";
    return showDialog;
}

function GetHtmlImage(icon, alt, dialogTitle, dialogWidth, url, editFormId, dataTableId, newWindow) {
    var result = "<img class=\'Cursor-Pointer\' src=";
    result += "\'" + icon + "\' ";
    result += "alt=\'" + alt + "\' ";
    result += "onclick=\'" + GetShowDialog(dialogTitle, dialogWidth, url, editFormId, dataTableId, newWindow) + "\' />";  
    return result;
}

function  GetHtmlLink(cssClass, text, dialogTitle, dialogWidth, url, editFormId, dataTableId, newWindow) {
    var result = "<a class=\'";
    result += cssClass;
    result += "\' onclick=\'" + GetShowDialog(dialogTitle, dialogWidth, url, editFormId, dataTableId, newWindow) + "\'>";
    result += text;
    result += "</a>";
    return result;
}


function SetDataTable(dtId, ajaxSrc, colDef, uUrl, uFrmTitle, uFrmW, dUrl, dFrmTitle, dFrmW) {
    var columns = GetIdColumn()
        .concat(GetEmptyColumns(1))
        .concat(GetImageColumn("/Content/Icons/Pencil-icon.png", "Pencil", uUrl, uFrmTitle, uFrmW, editForm, dtId))
        .concat(GetImageColumn("/Content/Icons/Close-2-icon.png", "Cross", dUrl, dFrmTitle, dFrmW, confirmForm, dtId));

    $("#" + dtId).dataTable({
        "bServerSide": true,
        "sAjaxSource": ajaxSrc,
        "bProcessing": true,
        "aoColumns": columns,
        //"fnServerParams": function (aoData) {
        //    aoData.push({ "name": "mycolumn1", "value": $('#col1').val() });
        //    aoData.push({ "name": "mycolumn2", "value": $('#col2').val() });
        //},
        "columnDefs": [{ "className": "Column-Center", "targets": [columns.length - 2, columns.length - 1] }]
    });
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
            "columnDefs": [{ "className": "Column-Center", "targets": [all.length - 2, all.length - 1] }]
        });

    $("#" + dtId + " tbody").on("click", "tr td img", function () {
        
        var nTr = this.parentNode.parentNode;
        var image = this;

        if(!image.src.match("Minus-icon") && !image.src.match("Add-Green-Button-icon")) return;

        if (image.src.match("Minus-icon")) {
            image.src = "/Content/Icons/Add-Green-Button-icon.png";
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
    var monitoringCssClassRed = "Cursor-Pointer Background-Red";
    var monitoringCssClassGreen = "Cursor-Pointer Background-Green";

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
                var dateItems = full[10].split("-");
                var date = new Date(parseInt(dateItems[2]), parseInt(dateItems[1]) - 1, parseInt(dateItems[0]));
                var today = new Date();
                var cssClass = date > today ? monitoringCssClassGreen : monitoringCssClassRed;
                return GetHtmlLink(cssClass, full[10], uFrmTitle2, uFrmW2, url, editForm, dtId);
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
            "columnDefs": [{ "className": "Column-Center", "targets": [all.length-4, all.length-2, all.length-1] }]});
};



function SetMaintenanceDataDataTable(dtId, ajaxSrc, nbDataCol, uUrl, uFrmTitle, uFrmW, dUrl, dFrmTitle, dFrmW, uUrl2, uFrmTitle2, uFrmW2, uUrl3, uFrmTitle3, uFrmW3) {
    var columns = GetIdColumn()
        .concat(GetEmptyColumns(nbDataCol))
        .concat(GetImageColumn("/Content/Icons/Adobe-PDF-Document-icon.png", "Pdf", uUrl3, uFrmTitle3, uFrmW3, editForm, dtId, true))
        .concat(GetDateLinkColumn(uUrl2, uFrmTitle2, uFrmW2, editForm, dtId, 9))
        .concat(GetImageColumn("/Content/Icons/Pencil-icon.png", "Pencil", uUrl, uFrmTitle, uFrmW, editForm, dtId))
        .concat(GetImageColumn("/Content/Icons/Close-2-icon.png", "Cross", dUrl, dFrmTitle, dFrmW, confirmForm, dtId));

    $("#" + dtId).dataTable({
            "bServerSide": true,
            "sAjaxSource": ajaxSrc,
            "bProcessing": true,
            "aoColumns": columns,
            "columnDefs": [{ "className": "Column-Center", "targets": [columns.length-4, columns.length-2, columns.length-1] }]});
};