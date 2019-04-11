$(document).ready(function () {

    $("#validation-form").bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            fullname: {
                validators: {
                    stringLength: {
                        min: 5,
                        message: 'בבקשה להכניס שם פרטי עם לפחות 5 תווים'
                    },

                    notEmpty: {
                        message: 'הכנס שם מלא'
                    }
                }
            },
            password: {
                validators: {
                    stringLength: {
                        min: 6,
                        message: 'הכנס סיסמא בין 6 ל 16 תווים'

                    },
                    notEmpty: {
                        message: 'בבקשה הכנס סיסמא'
                    }
                }
            },
            username: {
                validators: {
                    stringLength: {
                        min: 6,
                        message: 'הכנס שם משתמש בין 6 ל 30 תווים',

                    },
                    notEmpty: {
                        message: 'בבקשה הכנס סיסמא'
                    }
                }
            },
            phone: {
                validators: {
                    stringLength: {
                        min: 10,
                        max: 11,
                        message: 'מספר הטלפון צריך לכלול 10 ספרות'

                    },
                    notEmpty: {
                        message: 'בבקשה הכנס מספר טלפון'
                    }
                }
            },
            mail: {
                validators: {
                    notEmpty: {
                        message: 'הכנס כתובת אימייל'
                    },

                    emailAddress: {
                        message: 'הכנס כתובת אימייל חוקית'
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

//function FillAllFileds() {
//    let dropDown = document.getElementsByTagName("select");
//    for (var i = 0; i < dropDown.length; i++) {
//        if (dropDown[i].value == -1) {
//            dropDown[i].parentElement.style.color = 'red';
//            Swal.fire({
//                type: 'נמצאה בעיה',
//                title: 'שדות לא מסומנים',
//                text: 'בבקשה למלא את השדה המסומן באדום'
//            });
//            return;
//        }
//        else {
//            dropDown[i].parentElement.style.color = 'green';
//        }
//    }
//    AddUser();
//}




