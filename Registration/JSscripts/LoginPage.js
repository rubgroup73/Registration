
$(document).ready(function () {

    $("#formLogin").submit(returnFlase);
});

function returnFlase() {
    CheckAuth();
    return false;
}
function CheckAuth() {
    Admin = {
        Admin_UserName: document.getElementById("login__username").value,
        Admin_Password: document.getElementById("login__password").value
    }
    ajaxCall("POST", "../api/loginAuth", JSON.stringify(Admin), AuthSuccess, AuthError);
}

//*************All Ajax Success And Error Functions**********************************

//ajaxCall("POST", "../api/loginAuth", JSON.stringify(Admin), AuthSuccess, AuthError);
//************************************************************************************
function AuthSuccess(data) {
    if (data == true)
        window.location.href = "ContentReview.html";
    else
        alert("The Password Or The Username In Incorrect");

}
function AuthError() {
    console.log("Error Receiving Admin Details From DB");
}