function InsertImage() {
    var data = new FormData();
    var files = $("#section-image").get(0).files;
    var fileName = $("#section-title").val();
    // Add the uploaded file to the form data collection  


        for (f = 0; f < files.length; f++) {
            data.append(fileName, files[f]);
        }
        data.append("name", fileName); // aopend what ever data you want to send along with the files. See how you extract it in the controller.

    // Ajax upload  
    $.ajax({
        type: "POST",
        url: "../Api/files",
        contentType: false,
        processData: false,
        data: data,
        success: Success1,
        error: error
    });

    return false;
}
function Success1(data) {
    console.log("Success Adding Files to section");
}
function error() {
    alert("Error Adding Files to section");
}

function InsertImageToClass() {
    var data = new FormData();
    var files = $("#class-image").get(0).files;
    var fileName = $("#class-title").val();
    // Add the uploaded file to the form data collection  


    for (f = 0; f < files.length; f++) {
        data.append(fileName, files[f]);
    }
    data.append("name", fileName); // aopend what ever data you want to send along with the files. See how you extract it in the controller.

    // Ajax upload  
    $.ajax({
        type: "POST",
        url: "../Api/files",
        contentType: false,
        processData: false,
        data: data,
        success: Success2,
        error: error2
    });

    return false;
}
function Success2(data) {
    console.log("Success Adding Files to class");
}
function error2() {
    alert("Error Adding Files to class");
}

function InsertAudioToHomework() {
    var data = new FormData();
    var files = $("#homework-file").get(0).files;
    var fileName = $("#homework-title").val();
    // Add the uploaded file to the form data collection  


    for (f = 0; f < files.length; f++) {
        data.append(fileName, files[f]);
    }
    data.append("name", fileName); // aopend what ever data you want to send along with the files. See how you extract it in the controller.

    // Ajax upload  
    $.ajax({
        type: "POST",
        url: "../Api/files",
        contentType: false,
        processData: false,
        data: data,
        success: Success3,
        error: error3
    });

    return false;
}
function Success3(data) {
    console.log("Success Adding Files to homework");
}
function error3() {
    alert("Error Adding Files to homework");
}