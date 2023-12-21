function clearForm() {
    var registrationForm = $('#registrationForm');

    if (registrationForm && registrationForm.length) {
        registrationForm[0].reset();
        registrationForm.find('.invalid-feedback').text('');
        registrationForm.find('.is-invalid').removeClass('is-invalid');
    }
}

function registerUser() {
    javascript: (function () {
        document.getElementById("fullName").value = "Ashok Maru"
        document.getElementById("password").value = "123@Admin";
        document.getElementById("confirmPassword").value = "123@Admin";
        document.getElementById("email").value = "maruashok11@gmail.com";
        setTimeout(function () {
            document.querySelector("#registrationForm button[type='submit']").click();
        }, 50);
    })();
}

$(document).ready(function () {
    $("#registrationForm").validate({
        errorClass: "is-invalid",
        errorPlacement: function (error, element) {
            error.appendTo(element.next("div"));
        },
        rules: {
            fullName: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            password: {
                minlength: 6,
                required: true,
            },
            confirmPassword: {
                required: true,
                equalTo: "#password"
            }
        },
        messages: {
            fullName: {
                required: "Please enter your full name"
            },
            email: {
                required: "Please enter your email",
                email: "Please enter a valid email address"
            },
            password: {
                required: "Please enter a password",
                minlength: "Your password must be at least 6 characters long"
            },
            confirmPassword: {
                required: "Please confirm your password",
                equalTo: "Passwords do not match"
            }
        },
        submitHandler: function (form) {
            $('#formError').empty();
            $.ajax({
                method: 'POST',
                url: '/api/user/register',
                contentType: 'application/json',
                data: JSON.stringify({ fullName: $(fullName).val(), email: $(email).val(), password: $(password).val() }),
                success: function (data) {
                    if (data.success) {
                        window.location = '/Home/Thanks';
                    }
                },
                error: function (error) {
                    $('#formError').show();
                    $('#formError').append('<p>' + error.responseJSON.message + '</p>');
                }
            });
        }
    });
});