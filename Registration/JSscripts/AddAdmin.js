var allAdminsArr = [];
$(document).ready(function () {  
    $("#registration_form").submit(submitDetails);
});

//ברגע שהדף מוכן נעשת ולידציה על כל מה שמוכנס לפורם
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
    Admin = {
        Admin_Email: document.getElementById("email").value,     
        Admin_UserName: document.getElementById("userName").value
    }
    let userName = Admin["Admin_UserName"];
    let userEmail = Admin["Admin_Email"];
    ajaxCall("GET", "../api/admin/getAlladmins?username=" + userName + "&email=" + userEmail, "", GetAllAdminSuccess, ErrorAllAdminSuccess);
}


function SuccessAddNewAdmin() {
    console.log("Success Add New Admin To DB");
    counter = 0;
    swal("Added!", "Administretor Is Successfully Added!", "Success");
}
function ErrorAddNewAdmin() {
    console.log("Error Add New Admin To DB");
}

function GetAllAdminSuccess(adminExists) {
    console.log("Success Get All Admin From DB");

    if (adminExists != true) {
        Admin = {
            Admin_Firsname: document.getElementById("firstName").value,
            Admin_LastName: document.getElementById("lastName").value,
            Admin_Email: document.getElementById("email").value,
            Admin_Password: document.getElementById("password").value,
            Admin_UserName: document.getElementById("userName").value
        }

        ajaxCall("POST", "../api/admin/addNewAdmin", JSON.stringify(Admin), SuccessAddNewAdmin, ErrorAddNewAdmin);
    }
    else {
        alert("Email All Ready In Use, Please Insert a Different One");
    }
}
    
function ErrorAllAdminSuccess() {
    console.log("Error Get All Admin From DB");
}

