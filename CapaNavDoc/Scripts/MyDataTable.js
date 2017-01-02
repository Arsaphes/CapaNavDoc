function SetDataTable(
    dataTableId, ajaxSource, columnsDefArray, 
    updatectionUrl, updateFormTitle, updateFormWidth, 
    deleteActionUrl, deleteFormTitle, deleteFormWidth) {

    var idColumn = [{"sName": "ID", "visible": false}];
    var specialColumns = [
        {
            "sName": "Update",
            "mRender": function(data, type, full) {
                var url = updatectionUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Pencil-icon.png\" alt=\"Crayon\" onclick=\'ShowDialog(\"' + updateFormTitle + '\", ' + updateFormWidth + ', \"' + url + '\", \"EditForm\", \"' + dataTableId + '\")\' />';
                return result;
            }
        },
        {
            "sName": "Delete",
            "mRender": function(data, type, full) {
                var url = deleteActionUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Close-2-icon.png\" alt=\"Crayon\" onclick=\'ShowDialog(\"' + deleteFormTitle + '\", ' + deleteFormWidth + ', \"' + url + '\", \"ConfirmationForm\", \"' + dataTableId + '\")\' />';
                return result;
            }
        }
    ];
    var all = idColumn.concat(columnsDefArray).concat(specialColumns);

    var table = $("#" + dataTableId)
        .dataTable({
            "bServerSide": true,
            "sAjaxSource": ajaxSource,
            "bProcessing": true,
            "aoColumns": all,
            //"fnServerParams": function (aoData) {
            //    aoData.push({ "name": "mycolumn1", "value": $('#col1').val() });
            //    aoData.push({ "name": "mycolumn2", "value": $('#col2').val() });
            //},
            "columnDefs": [{ "className": "CenteredColumn", "targets": [all.length-2, all.length-1] }]
        });

    //$("#col1, #col2")
    //    .bind("keyup",
    //        function () {
    //            table.fnDraw();
    //        });
};

function SetMasterDataTable(
    dataTableId, ajaxSource, columnsDefArray, 
    updateActionUrl, updateFormTitle, updateFormWidth, 
    deleteActionUrl, deleteFormTitle, deleteFormWidth, 
    detailViewUrl) {

    var idColumn = [
        {
            "sName": "ID",
            "visible": false
        },
        {
            "bSortable": false,
            "bSearchable": false,
            "mRender": function(data, type, full) {
                return '<img src="/Content/Icons/Add-Green-Button-icon.png" alt="expand/collapse" rel="' + full[0] + '"/>';
            }
        }
    ];
    var specialColumns = [
        {
            "sName": "Update",
            "mRender": function (data, type, full) {
                var url = updateActionUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Pencil-icon.png\" alt=\"Crayon\" onclick=\'ShowDialog(\"' + updateFormTitle + '\", ' + updateFormWidth + ', \"' + url + '\", \"EditForm\", \"' + dataTableId + '\")\' />';
                return result;
            }
        },
        {
            "sName": "Delete",
            "mRender": function (data, type, full) {
                var url = deleteActionUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Close-2-icon.png\" alt=\"Crayon\" onclick=\'ShowDialog(\"' + deleteFormTitle + '\", ' + deleteFormWidth + ', \"' + url + '\", \"ConfirmationForm\", \"' + dataTableId + '\")\' />';
                return result;
            }
        }
    ];
    var all = idColumn.concat(columnsDefArray).concat(specialColumns);

    var table = $("#" + dataTableId)
        .dataTable({
            "bServerSide": true,
            "sAjaxSource": ajaxSource,
            "bProcessing": true,
            "aoColumns": all,
            "columnDefs": [{ "className": "CenteredColumn", "targets": [all.length - 2, all.length - 1] }]
        });

    $("#" + dataTableId + " tbody").on("click", "tr td img", function () {
        var nTr = this.parentNode.parentNode;
        var image = this;
        if (this.src.match("Minus-icon")) {
            this.src = "/Content/Icons/Add-Green-Button-icon.png";
            table.fnClose(nTr);
        }
        else {
            this.src = "/Content/Icons/Minus-icon.png";
            var id = $(this).attr("rel");
            $.get(detailViewUrl, { id: id }, function (details) { table.fnOpen(nTr, details, "DetailedRow") });
            $("#" + dataTableId + " tbody").on("click", "tr td input[type=button]", function() {
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

function SetEquipmentDataTable(
    dataTableId, ajaxSource, columnsDefArray, 
    updatectionUrl, updateFormTitle, updateFormWidth, 
    deleteActionUrl, deleteFormTitle, deleteFormWidth,
    updateMonitoringActionUrl, updateMonitoringFormTitle, updateMonitoringFormWidth,
    updateCentersActionsUrl, updateCentersActionsFormTitle, updateCentersActionsFormWidth) {

    var idColumn = [{"sName": "ID", "visible": false}];
    var specialColumns = [
        {
            "sName": "CentersActions",
            "mRender": function(data, type, full) {
                var url = updateCentersActionsUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Zoom-icon.png\" alt=\"Magnifier\" onclick=\'ShowDialog(\"' + updateCentersActionsFormTitle + '\", ' + updateCentersActionsFormWidth + ', \"' + url + '\", \"EditForm\", \"' + dataTableId + '\")\' />';
                return result;
            }
        },
        {
            "sName": "Monitoring",
            "mRender": function(data, type, full) {
                var url = updateMonitoringActionUrl + "?id=" + full[0];
                var result = '<a class="CursorPointer" onclick=\'ShowDialog(\"' + updateMonitoringFormTitle + '\", ' + updateMonitoringFormWidth + ', \"' + url + '\", \"EditForm\", \"' + dataTableId + '\")\'>' + full[10] + "</a>";
                return result;
            }
        },
        {
            "sName": "Update",
            "mRender": function(data, type, full) {
                var url = updatectionUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Pencil-icon.png\" alt=\"Crayon\" onclick=\'ShowDialog(\"' + updateFormTitle + '\", ' + updateFormWidth + ', \"' + url + '\", \"EditForm\", \"' + dataTableId + '\")\' />';
                return result;
            }
        },
        {
            "sName": "Delete",
            "mRender": function(data, type, full) {
                var url = deleteActionUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Close-2-icon.png\" alt=\"Crayon\" onclick=\'ShowDialog(\"' + deleteFormTitle + '\", ' + deleteFormWidth + ', \"' + url + '\", \"ConfirmationForm\", \"' + dataTableId + '\")\' />';
                return result;
            }
        }
    ];
    var all = idColumn.concat(columnsDefArray).concat(specialColumns);

    $("#" + dataTableId)
        .dataTable({
            "bServerSide": true,
            "sAjaxSource": ajaxSource,
            "bProcessing": true,
            "aoColumns": all,
            "columnDefs": [{ "className": "CenteredColumn", "targets": [all.length-4, all.length-2, all.length-1] }]
        });
};