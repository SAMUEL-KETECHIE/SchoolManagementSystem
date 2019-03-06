/*
 * Creator: Samuel Wendolin Ketechie
 * Date: February 25,2019
 */

let apiUrl = "/api/admin";

let classes = {};
let students = {};
let teachers = {};
let subjects = {};
let imageValue = "";


$(function () {
    document.querySelector("#snack_bar_warning").style.display = "none";
    document.querySelector("#snack_bar_success").style.display = "none";
    
    getAllClasses();
    getAllStudents();
    getAllTeachers();
    getAllSubjects();
});

function initDate() {
    let today = new Date().toLocaleDateString("dd-MMM-yyyy");
    $("#stddob").val(today);
    $("#stdenrolldate").val(today);
}
//Image Uploading
function UploadImage(dataFunc = null, fileid) {
    var formData = new FormData();
    formData.append('file', fileid); // stdimage is the input type="file" control

    $.ajax({
        url: apiUrl + "/saveimage",
        type: 'POST',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            imageValue = result.toString();
            return dataFunc(imageValue);
        },
        error: function (jqXHR) {
            alert("Image failed to saved");
        }
    });

}
//Get Classes
function getAllClasses() {
    $.ajax({
        url: apiUrl + "/getallclasses",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            let opt = '<option value="-1">-- Select a Class --</option>';
            if (data !== null) {
                let output = data.data;
                for (let i = 0; i < output.length; i++) {
                    opt += '<option value="' + output[i].classId + '">' + output[i].className + '</option>';
                }
                classes = data.data;
                $("#stdclass").html(opt);
                let classObj = classes;
                let classCount = counter(classObj);
                $("#totalClass").html(classCount);

            }
                
        }
    });
}

//Get Students
function getAllStudents() {
    $.ajax({
        url: apiUrl + "/getallstudents",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null)
                students = data.data;
            let totalStudents = counter(students);
            $("#totalStuds").html(totalStudents);
        }
    });
}

//Get all Teachers
function getAllTeachers() {
    $.ajax({
        url: apiUrl + "/getallteachers",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null)
                teachers = data.data;
            let totalTeachers = counter(teachers);
            $("#totalTeach").html(totalTeachers);
        }
    });
}

//Get all Subjects
function getAllSubjects() {
    $.ajax({
        url: apiUrl + "/getallsubjects",
        type: "Get",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data !== null)
                subjects = data.data;
            let totalSubjects = counter(subjects);
            $("#totalSub").html(totalSubjects);

            let opt = '<option value="-1">-- Select a Subject --</option>';
            for (let i = 0; i < subjects.length; i++) {
                opt += '<option value="' + subjects[i].subjectId + '">' + subjects[i].subjectName + '</option>';
            }
            $("#teachersubject").html(opt);
        }
    });
}

//funtion for counting elements in an array
function counter(e) {
    let count = 0;
    for (let i = 0; i < e.length; i++) {
        count++;
    }
    return count;
}

//Save New Subject
$("#btn_subjectsave").on('click', function (e) {
    e.preventDefault();
    if (confirm("Are you sure you want to add this subject ?")) {
        saveSubject();
    }
});

function saveSubject() {
    var model = {
        subjectName: $("#subjectname").val()
    };

    $.ajax({
        url: apiUrl + "/addnewsubject",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(model),
        success: function (data) {
            if (data !== null) {
                $("#subjectname").val("");
                showSnackBarSuccess();
            } else {
                showSnackBarWarning();
            }
        }
    });
}

//Save Student
$("#btn_stdsave").on('click', function (e) {
    e.preventDefault();
    if (confirm("Are you sure you want to add this student ?")) {
        UploadImage(saveStudent, $('#stdimage')[0].files[0]);
    }
});

function saveStudent(img) {

    var model = {
        studentName: $("#stdname").val(),
        classId: $("#stdclass").val(),
        dateOfBirth: $("#stddob").val(),
        studentAddress: $("#stdaddress").val(),
        parentName: $("#stdparent").val(),
        dateEnrolled: $("#stdenrolldate").val(),
        gender: $("#stdgender").val(),
        image: img
    };
    
    $.ajax({
        url: apiUrl + "/addnewstudent",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(model),
        success: function (data) {
            if (data !== null) {
                $("#stdname").val("");
                $("#stdclass").val("");
                $("#stddob").val("");
                $("#stdaddress").val("");
                $("#stdparent").val("");
                $("#stdenrolldate").val("");
                $("#stdgender").val("");
                $("#stdimage").val("");
                showSnackBarSuccess();
            } else {
                showSnackBarWarning();
            }
        }
    });
}

//Save Teacher
$("#btn_teachersave").on('click', function (e) {
    e.preventDefault();
    if (confirm("Are you sure you want to add this teacher ?")) {
        UploadImage(saveTeacher, $('#teacherimage')[0].files[0]);
    }
});

function saveTeacher(img) {

    var model = {
        teacherName: $("#teachername").val(),
        subjectId: $("#teachersubject").val(),
        teacherAddress: $("#teacheraddress").val(),
        image: img
    };

    $.ajax({
        url: apiUrl + "/addnewteacher",
        type: "Post",
        contentType: "application/json",
        data: JSON.stringify(model),
        success: function (data) {
            if (data !== null) {
                $("#teachername").val("");
                $("#teachersubject").val("");
                $("#teacheraddress").val("");
                $("#teacherimage").val("");
                showSnackBarSuccess();
            } else {
                showSnackBarWarning();
            }
        }
    });
}


//Snack Bars
function showSnackBarSuccess() {
    let x = document.getElementById("snack_bar_success");
    x.className = "show";
    setTimeout(function () {
        x.className = x.className.replace("show", "");
    }, 9050);

}

function showSnackBarWarning() {
    let x = document.getElementById("snack_bar_warning");
    x.className = "show";
    setTimeout(function () {
        x.className = x.className.replace("show", "");
    }, 9050);

}


//Button Operations
function showStudentGrid(e) {
    e.preventDefault();
    initDate();
    let x = document.getElementById("studentGrid");
    x.className = "show";
    $("#addStudent").attr("disabled", "disabled");

}

function showTeacherGrid(e) {
    e.preventDefault();
    let x = document.getElementById("teacherGrid");
    x.className = "show";
    $("#addTeacher").attr("disabled", "disabled");

}

function showSubjectGrid(e) {
    e.preventDefault();
    let x = document.getElementById("subjectGrid");
    x.className = "show";
    $("#addSubject").attr("disabled", "disabled");
}

function hideStudGrid(e) {
    e.preventDefault();
    let x = document.getElementById("studentGrid");
    x.className = "hide";
    $("#addStudent").attr("disabled", false);
}

function hideTeachGrid(e) {
    e.preventDefault();
    let s = document.getElementById("teacherGrid");
    s.className = "hide";
    $("#addTeacher").attr("disabled", false);
}

function hideSubGrid(e) {
    e.preventDefault();
    let p = document.getElementById("subjectGrid");
    p.className = "hide";
    $("#addSubject").attr("disabled", false);
}

$("#addStudent").on('click', showStudentGrid);
$("#addTeacher").on('click', showTeacherGrid);
$("#addSubject").on('click', showSubjectGrid);
$("#btn_finish_stud").on('click', hideStudGrid);
$("#btn_finish_teach").on('click', hideTeachGrid);
$("#btn_finish_sub").on('click', hideSubGrid);

