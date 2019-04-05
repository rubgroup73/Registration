
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
function AuthSuccess(data) {
    if (data == true)
        window.location.href = "ContentReview.html";
    else
        alert("The Password Or The Username In Incorrect");

}
function AuthError() {
    alert("Error Receiving Admin Details From DB");
}