/*
 * Creator: Samuel Wendolin Ketechie
 * Date: March 7,2019
 */

let apiUrl = "/api/admin";

let teachers = {};
let subjects = {};

$(function () {
    getAllSubjects();
});

function getAllClasses() {
    $.ajax({
        url: apiUrl + "/getallclasses",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null) {
                classes = data.data;
                getAllStudents();
            }
        }
    });
}

//(1)
function searchTeacher(searchObj) {
    $.ajax({
        url: apiUrl + "/getteacherbyinfo?info=" + searchObj.info,
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            if (result !== null) {
                teachers = result.data;
                renderGrid(teachers);
            }
        }
    });
}


//(2)
function getTeacherBySearch(e) {
    e.preventDefault();
    var keyActionClick = e.type === 'click';
    if ((e.type === 'keyup' && e.which === 13) || keyActionClick === true) {
        var search = $("#txtSearchTeacher").val();
        if (search === "" || search.length === 0 || search === null) {
            getAllTeachers();
        }
        else {
            searchObj = {
                info: search
            };
            searchTeacher(searchObj);
        }
    }
}

$("#txtSearchTeacher").on('keyup', getTeacherBySearch);
$("#btnSearchTeacher").on('click', getTeacherBySearch);

//Get all Teachers
function getAllTeachers() {
    $.ajax({
        url: apiUrl + "/getallteachers",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null) {
                teachers = data.data;
                renderGrid(teachers);
            }
        }
    });
}

function getAllSubjects() {
    $.ajax({
        url: apiUrl + "/getallsubjects",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null) {
                subjects = data.data;
                getAllTeachers();
            }
        }
    });
}

function subjectEditor(container, options) {
    $('<input type="text" id="subject" readonly data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .width('100%')
        .kendoComboBox({
            dataSource: subjects,
            dataValueField: "subjectId",
            dataTextField: "subjectName",
            highlightFirst: true,
            suggest: true,
            optionLabel: ""
        });
}

function renderGrid(teacherData) {
    let todayDate = new Date().toString();
    let exportedData = "Teachers_As_At_" + todayDate;
    let fields = {
        teacherId: { editable: false, validation: { required: true } },
        teacherName: { editable: true, validation: { required: true }, height: 40 },
        teacherAddress: { editable: true, validation: { required: false }, height: 40 },
        teacherNo: { editable: true, validation: { required: false } },
        isActive: { editable: true, validation: { required: false } },
        image: { editable: true, validation: { required: false } },
        subjectId: { editable: true, validation: { required: false } }
    };
    let data_source = {
        transport: {
            read: function (entries) {
                entries.data = teacherData;
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
                id: "teacherId",
                fields: fields
            }
        },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    };

    $("#teachersGrid").kendoGrid({
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
                field: "teacherName",
                title: "Teacher's Name",
                width: 125
            },
            {
                field: "teacherNo",
                title: "Teacher No.",
                width: 155
            },
            {
                field: "teacherAddress",
                title: "Address/Location",
                width: 250
            },

            {
                field: "isActive",
                title: "Active?",
                template: '<input type="checkbox" #= isActive ? \'checked="isActive"\' : "" # disabled="disabled" />',
                width: 60
            },
            {
                field: "image",
                title: "Image",
                width: 90,
                template: '<img src="#=image #" alt="image" class="img-responsive img-circle"/>'
            },
            {
                field: "subjectId",
                title: "Default Subject",
                width: 100,
                editor: subjectEditor,
                template: '#= getSubject(subjectId) #'
            },
            {
                command: [
                    {
                        name: "edit"
                        // click: 
                    }
                ],
                width: 100
            },
            {
                command: [
                    {
                        name: "delete"
                        // click: 
                    }
                ],
                width: 100
            }
        ],
        resizable: true,
        navigatable: true,
        scrollable: true,
        pageable: {
            pageSize: 50,
            pageSizes: [50, 100],
            previousNext: true,
            buttonCount: 5
        },
        selectable: true,
        editable: "popup"
    });
}

function getSubject(id) {
    for (let i = 0; i < subjects.length; i++) {
        if (subjects[i].subjectId === id) {
            return subjects[i].subjectName;
        }
    }
}
