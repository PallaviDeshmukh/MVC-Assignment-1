$(function () {
    $("#grid").jqGrid({
        url: "/Birthday/Index",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['UserID', 'UserName', 'Password', 'BirthDate'],
        colModel: [
            { key: true, hidden: true, name: 'UserID', index: 'UserID', editable: true },
            { key: false, name: 'UserName', index: 'UserName', editable: true },
            { key: false, name: 'Password', index: 'Password', editable: true },
            { key: false, name: 'BirthDate', index: 'BirthDate', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
            
            ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'User List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/Birthday/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/Birthday/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/Birthday/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this user?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});