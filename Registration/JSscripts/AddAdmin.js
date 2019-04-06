var allAdminsArr = [];
$(document).ready(function () {  
    $("#registration_form").submit(submitDetails);
});

$(document).ready(function () {


    $('#testCheck').bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            firstName: {
                validators: {
                    stringLength: {
                        min: 2,
                        message: 'בבקשה להכניס שם פרטי עם לפחות 2 תווים'
                    },

                    notEmpty: {
                        message: 'הכנס שם פרטי'
                    }
                }
            },
            lastName: {
                validators: {
                    stringLength: {
                        min: 2,
                        message: 'בבקשה להכניס שם משפחה עם לפחות 2 תווים'
                    },

                    notEmpty: {
                        message: 'הכנס שם משפחה'
                    }
                }
            },
            email: {
                validators: {
                    notEmpty: {
                        message: 'הכנס כתובת אימייל'
                    },

                    emailAddress: {
                        message: 'הכנס כתובת אימייל חוקית'
                    }
                }
            },
            userName: {
                validators: {
                    stringLength: {
                        min: 5,
                        message: 'הכנס שם משתמש עם לפחות 5 תווים'
                    },

                    notEmpty: {
                        message: 'הכנס שם משתמש'
                    }
                }
            },
            password: {
                validators: {
                    stringLength: {
                        min: 6,
                        message: 'הכנס סיסמא בין 6 ל 16 תווים',
                        max: 16
                    },

                    identical: {
                        field: 'confirmpassword',
                        message: 'הסיסמא לא תואמת לסיסמא שהזנת'
                    },

                    notEmpty: {
                        message: 'בבקשה הכנס סיסמא'
                    }
                }
            },
            confirmpassword: {
                validators: {
                    notEmpty: {
                        message: 'הכנס את הסיסמא שוב'
                    },

                    identical: {
                        field: 'password',
                        message: 'הסיסמא לא תואמת לסיסמא שהזנת'
                    }
                }
            }
        },
        submitHandler: function (validator, form) {
            $('#registration_success').slideDown({ opacity: "show" }, "slow");
            $.post(form.attr('action'), form.serialize(),
                function () {

                },
                'json');
        }
    });
  
});

function submitDetails() {
    AddAdmin();
    return false;
 
}

function AddAdmin() {
    ajaxCall("GET", "../api/admin/getAlladmins", "", GetAllAdminSuccess, ErrorAllAdminSuccess)
   
}

function checkIfUserNameExists(Admin_UserName) {
    for (var i = 0; i < allAdminsArr.length; i++) {

        if (allAdminsArr[i].Admin_UserName == Admin_UserName) {
            return true;
        }
    }
    return false;
}
function checkIfEmailAddressExists(Admin_Email) {
    for (var i = 0; i < allAdminsArr.length; i++) {

        if (allAdminsArr[i].Admin_Email == Admin_Email) {
            return true;
        }
    }
    return false;
}

function SuccessAddNewAdmin() {
    console.log("Success Add New Admin To DB");
    counter = 0;
    swal("Added!", "Administretor Is Successfully Added!", "Success");
}
function ErrorAddNewAdmin() {
    console.log("Error Add New Admin To DB");
}

function GetAllAdminSuccess(allAdmins) {
    console.log("Success Get All Admin From DB");
    allAdminsArr = allAdmins;
    Admin = {
        Admin_Firsname: document.getElementById("firstName").value,
        Admin_LastName: document.getElementById("lastName").value,
        Admin_Email: document.getElementById("email").value,
        Admin_Password: document.getElementById("password").value,
        Admin_UserName: document.getElementById("userName").value
    }

    let temp1 = checkIfUserNameExists(Admin["Admin_UserName"]);
    let temp2 = checkIfEmailAddressExists(Admin["Admin_Email"]);

    if (temp1 != true) {
        if (temp2 != true)
            ajaxCall("POST", "../api/admin/addNewAdmin", JSON.stringify(Admin), SuccessAddNewAdmin, ErrorAddNewAdmin);
        else
            alert("Email All Ready In Use, Please Insert a Different One");
    }
    else
        alert("Username All Ready In Use, Please Insert a Different One");
}
function ErrorAllAdminSuccess() {
    console.log("Error Get All Admin From DB");
}

