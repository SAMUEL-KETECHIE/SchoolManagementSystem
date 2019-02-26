/*
 * Creator: Samuel Wendolin Ketechie
 * Date: February 25,2019
 */
$(function () {

});

//Get Classes
function getAllClasses() {
    $.ajax({
        url: "",
        type: "Get",
        dataType: "json",
        contentType: "application/json"
    }).success(function (data) {
        let opt = '<option value="-1">Please Select a Class</option>';
        if (data !== null)
            for (var i = 0; i < data.length; i++) {
                opt += '<option value="' + data[i].ClassId + '">' + data[i].ClassName + '</option>';
            }
        $("#stdclass").html(opt);
    });
}







//Button Operations

function showStudentGrid(e) {
    e.preventDefault();
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

