/*
 * Creator: Samuel Wendolin Ketechie
 * Date: March 5,2019
 */

let apiUrl = "/api/admin";

let students = {};
let classes = {};
let num = {
    info: "WENSCHSTU0001"
};

$(function () {
    // document.querySelector("#snack_bar_warning").style.display = "none";
    //document.querySelector("#snack_bar_success").style.display = "none";
    getAllClasses();
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

//(2)
function getStudentBySearch(e) {
    e.preventDefault();
    var keyActionClick = e.type === 'click';
    if ((e.type === 'keyup' && e.which === 13) || keyActionClick === true) {
        var search = $("#txtSearch").val();
        if (search === "" || search.length === 0 || search === null) {
            getAllStudents();
        }
        else {
            searchObj = {
                info: search
            };
            searchStudent(searchObj);
        }
    }
}

$("#txtSearch").on('keyup', getStudentBySearch);
$("#btnSearch").on('click', getStudentBySearch);
//(1)
function searchStudent(searchObj) {
    $.ajax({
        url: apiUrl + "/getstudentbyinfo?info=" + searchObj.info,
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            if (result !== null) {
                students = result;
                renderGrid(students);
            }
        }
    });
}

function getAllStudents() {
    $.ajax({
        url: apiUrl + "/getallstudents",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            if (result !== null) {
                students = result;
                renderGrid(students);
            }
        }
    });
}

function renderGrid(stdData) {
    let todayDate = new Date().toString();
    let exportedData = "Students_As_At_" + todayDate;
    let fields = {
        studentId: { editable: false, validation: { required: true } },
        studentName: { editable: true, validation: { required: true },height:40 },
        studentAddress: { editable: true, validation: { required: false }, height: 40 },
        studentNo: { editable: true, validation: { required: false } },
        dateOfBirth: { editable: true, validation: { required: false } },
        age: { editable: true, validation: { required: false } },
        gender: { editable: true, validation: { required: false } },
        parentName: { editable: true, validation: { required: false } },
        dateEnrolled: { editable: true, validation: { required: false } },
        isActive: { editable: true, validation: { required: false } },
        image: { editable: true, validation: { required: false } },
        classId: { editable: true, validation: { required: false } }
    };
    let data_source = {
        transport: {
            read: function (entries) {
                entries.data = stdData.data;
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
                id: "studentId",
                fields: fields
            }
        },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    };

    $("#studentsGrid").kendoGrid({
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
                field: "studentName",
                title: "Student Name",
                width: 125
            },
            {
                field: "studentNo",
                title: "Student No.",
                width: 120
            },
            {
                field: "studentAddress",
                title: "Address/Location",
                width: 250
            },
            {
                field: "dateOfBirth",
                title: "Date Of Birth",
                template: "#= kendo.toString(kendo.parseDate(dateOfBirth, 'yyyy-MM-dd'), 'dd MMM yyyy') #",
                width: 100
            },
            {
                field: "age",
                title: "Age",
                width: 50
            },
            {
                field: "gender",
                title: "Gender",
                width: 60
            },
            {
                field: "parentName",
                title: "Parent/Guardian",
                width: 125
            },
            {
                field: "dateEnrolled",
                title: "Date Enrolled",
                template: "#= kendo.toString(kendo.parseDate(dateEnrolled, 'yyyy-MM-dd'), 'dd MMM yyyy') #",
                width: 100
            },
            {
                field: "isActive",
                title: "Active?",
                template: '<input class="pull-right" type="checkbox" #= isActive ? \'checked="isActive"\' : "" # disabled="disabled" />',
                width: 60
            },
            {
                field: "image",
                title: "Image",
                width: 90,
                template: '<img src="#=image #" alt="image" class="img-responsive img-circle"/>'
            },
            {
                field: "classId",
                title: "Class",
                width: 100,
                template: '#= getClass(classId) #'
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
                width: 120
            }
        ],
        resizable: true,
        navigatable: true,
        scrollable: true,
        pageable: {
            pageSize: 50,
            pageSizes: [50, 100, 250, 500, 750, 1000, 1500],
            previousNext: true,
            buttonCount: 5
        },
        selectable: true,
        editable: "popup"
    });
}

function getClass(id) {
    for (let i = 0; i < classes.length; i++) {
        if (classes[i].classId === id) {
            return classes[i].className;
        }
    }
}

