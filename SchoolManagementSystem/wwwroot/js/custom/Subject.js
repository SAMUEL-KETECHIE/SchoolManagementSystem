/*
 * Creator: Samuel Wendolin Ketechie
 * Date: March 8,2019
 */

let apiUrl = "/api/admin";

let subjects = {};

$(function () {
    getAllSubjects();
});

//Search Subject
//(1)
function searchSubject(searchObj) {
    $.ajax({
        url: apiUrl + "/getsubjectbyinfo?info=" + searchObj.info,
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            if (result !== null) {
                subjects = result.data;
                renderGrid(subjects);
            }
        }
    });
}


//(2)
function getSubjectBySearch(e) {
    e.preventDefault();
    var keyActionClick = e.type === 'click';
    if (e.type === 'keyup' && e.which === 13 || keyActionClick === true) {
        var search = $("#txtSearchSubject").val();
        if (search === "" || search.length === 0 || search === null) {
            getAllSubjects();
        }
        else {
            searchObj = {
                info: search
            };
            searchSubject(searchObj);
        }
    }
}

$("#txtSearchSubject").on('keyup', getSubjectBySearch);
$("#btnSearchSubject").on('click', getSubjectBySearch);

//Get all Subjects
function getAllSubjects() {
    $.ajax({
        url: apiUrl + "/getallsubjects",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null) {
                subjects = data.data;
                renderGrid(subjects);
            }
        }
    });
}

//Render Grid
function renderGrid(subjectData) {
    let todayDate = new Date().toString();
    let exportedData = "Subjects_As_At_" + todayDate;
    let fields = {
        subjectId: { editable: false, validation: { required: true } },
        subjectName: { editable: true, validation: { required: true } }
    };
    let data_source = {
        transport: {
            read: function (entries) {
                entries.data = subjectData;
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
                id: "subjectId",
                fields: fields
            }
        },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    };

    $("#subjectsGrid").kendoGrid({
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
                field: "subjectName",
                title: "Subject Name",
                width: 250
            },
            
            {
                command: [
                    {
                        name: "edit"
                        // click: 
                    }
                ],
                width:80
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
