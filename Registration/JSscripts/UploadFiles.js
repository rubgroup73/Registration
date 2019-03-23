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
    alert("Success Adding Files");
}
function error() {
    alert("Error Adding Files");
}