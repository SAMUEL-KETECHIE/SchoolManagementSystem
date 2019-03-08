/*
 * Creator: Samuel Wendolin Ketechie
 * Date: March 8,2019
 */

let apiUrl = "/api/admin";

let classes = {};

$(function () {
    getAllClasses();
});

//Search Subject
//(1)
function searchClass(searchObj) {
    $.ajax({
        url: apiUrl + "/getclassbyinfo?info=" + searchObj.info,
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            if (result !== null) {
                classes = result.data;
                renderGrid(classes);
            }
        }
    });
}


//(2)
function getClassBySearch(e) {
    e.preventDefault();
    var keyActionClick = e.type === 'click';
    if (e.type === 'keyup' && e.which === 13 || keyActionClick === true) {
        var search = $("#txtSearchClass").val();
        if (search === "" || search.length === 0 || search === null) {
            getAllClasses();
        }
        else {
            searchObj = {
                info: search
            };
            searchClass(searchObj);
        }
    }
}

$("#txtSearchClass").on('keyup', getClassBySearch);
$("#btnSearchClass").on('click', getClassBySearch);

//Get all Subjects
function getAllClasses() {
    $.ajax({
        url: apiUrl + "/getallclasses",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null) {
                classes = data.data;
                renderGrid(classes);
            }
        }
    });
}

//Render Grid
function renderGrid(classData) {
    let todayDate = new Date().toString();
    let exportedData = "Classes_As_At_" + todayDate;
    let fields = {
        classId: { editable: false, validation: { required: true } },
        className: { editable: true, validation: { required: true } }
    };
    let data_source = {
        transport: {
            read: function (entries) {
                entries.data = classData;
                entries.success(entries.data);
            },
            create: function (entries) {
                entries.success(entries.data);
            },
            update: function (entries) {
                entries.success(entries.data);
            },
            destroy: function (entries) {
                entries.success(entries.data);
            },
            parameterMap: function (data) {
                return kendo.stringify(data);
            }
        },
        schema: {
            model: {
                id: "classId",
                fields: fields
            }
        },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    };

    $("#classesGrid").kendoGrid({
        toolbar: ["pdf", "excel"],
        pdf: {
            fileName: exportedData + ".pdf",
            allPages: true,
            avoidLinks: true,
            landscape: true,
            repeatHeaders: true,
            filterable: true,
            scale: 0.8
        },
        excel: {
            fileName: exportedData + ".xlsx",
            allPages: true,
            repeatHeaders: true,
            filterable: true
        },
        dataSource: data_source,
        columns: [
            {
                field: "className",
                title: "Class Name",
                width: 250
            },

            {
                command: [
                    {
                        name: "edit"
                        // click: 
                    }
                ],
                width: 80
            },
            {
                command: [
                    {
                        name: "delete"
                        // click: 
                    }
                ],
                width: 80
            }
        ],
        resizable: true,
        navigatable: true,
        scrollable: true,
        pageable: {
            pageSize: 50,
            pageSizes: [50],
            previousNext: true,
            buttonCount: 5
        },
        selectable: true,
        editable: "popup"
    });
}
