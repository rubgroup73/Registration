
/*************************Hours Maker******************/
function createHourList() {
    var hourList1 = "<option value='-1' selected>בחר</option>";
    var hourList2 = "<option value='-1' selected>בחר</option>";

    for (let i = 0; i < hourArr.length; i++) {
        hourList1 += "<option value = '" + i + "'>" + hourArr[i] + "</option>";
        hourList2 += "<option value = '" + i + "'>" + hourArr[i] + "</option>";
    }
    document.getElementById("prefhour1").innerHTML = hourList1;
    document.getElementById("prefhour2").innerHTML = hourList1;

}
/*************************Ajax User Registration******************/
function AddUser() {
    User = { // Note that the name of the fields must be identical to the names of the properties of the object in the server
        FullName: $("#fullname").val(),
        Gender: $("#gender").val(),
        Status: $("#famstatus").val(),
        YearsOfEducation: $("#education").val(),
        UserName: $("#username").val(),
        Password: $("#password").val(),
        Residence: $("#residence").val()
    }

    ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);
}
function userAddSuccefully(data) {
    swal("Added!", "User is successfully Added!", "success");
}
function userNotAdded() {
    Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'Something went wrong!'

    })
}
function formSubmited() {
    AddUser();
    return false;
}
/*************************Auto Fill Script******************/
function autofillScript() {
    document.getElementById("password").value = "passwordForTest";
    document.getElementById("username").value = "geeeveeer";
    document.getElementById("city").value = "Tel-Aviv";
    document.getElementById("mail").value = "ruppin@gmail.com";
    document.getElementById("phone").value = "0987654321";
    document.getElementById("fullname").value = "test name";
}