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
    //renderGrid(num);
    getAllStudents();
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
    let fields = {
        studentId: { editable: false, validation: { required: true } },
        studentName: { editable: true, validation: { required: true } },
        studentAddress: { editable: true, validation: { required: false } },
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

                //url: apiUrl + "/getallstudents",
                //type: "Get",
                //dataType: "json",
                //data: stdData,
                //contentType: "application/json"
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
            fileName: "Students.pdf",
            allPages: true,
            avoidLinks: true,
            landscape: true,
            repeatHeaders: true,
            filterable: true,
            scale: 0.8
        },
        excel: {
            fileName: "Students.xlsx",
            allPages: true,
            repeatHeaders: true,
            filterable: true
        },
        dataSource: data_source,
        columns: [
            {
                field: "studentName",
                title: "Student Name"
            },
            {
                field: "studentNo",
                title: "Student No."
            },
            {
                field: "studentAddress",
                title: "Address/Location"
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
                title: "Parent/Guardian"
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
                template: '<input type="checkbox" #= isActive ? \'checked="isActive"\' : "" # disabled="disabled" />',
                width: 60
            },
            {
                field: "image",
                title: "Image",
                template: '<img src="#= image #" alt="image" class="img-responsive img-circle"/>'
            },
            {
                field: "classId",
                title: "Class",
                template: '#= getClass(classId) #'
            },
            {
                command: [
                    {
                        name: "edit",
                        iconClass: "fa fa-pencil-square"
                        // click: 
                    }
                ],
                width: 60
            },
            {
                command: [
                    {
                        name: "delete",
                        iconClass: "fa fa-trash"
                        // click: 
                    }
                ],
                width: 60
            }
        ],
        resizable: true,
        navigatable: true,
        scrollable: true,
        sortable: { mode: 'multiple' },
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

