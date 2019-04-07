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




