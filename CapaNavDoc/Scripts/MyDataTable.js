function SetDataTable(dataTableId, ajaxSource, columnsDefArray, updatectionUrl, editFormTitle, editFormWidth, deleteActionUrl, deleteFormTitle, deleteFormWidth) {

    var idColumn = [
        {
            "sName": "ID",
            "visible": false
        }];

    var specialColumns = [
        {
            "sName": "Update",
            "mRender": function(data, type, full) {
                var url = updatectionUrl + "?id=" + full[0];
                var result = '<img src=\"/Content/Icons/Pencil-icon.png\" alt=\"Crayon\" onclick=\'ShowDialog(\"' + editFormTitle + '\", ' + editFormWidth + ', \"' + url + '\", \"EditForm\", \"' + dataTableId + '\")\' />';
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
            "columnDefs": [{ "className": "CenteredColumn", "targets": [5, 6] }]
        });

    //$("#col1, #col2")
    //    .bind("keyup",
    //        function () {
    //            table.fnDraw();
    //        });
};