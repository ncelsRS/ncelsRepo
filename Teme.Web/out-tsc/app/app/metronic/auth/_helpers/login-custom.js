var LoginCustom = (function () {
    function LoginCustom() {
    }
    LoginCustom.handleSignInFormSubmit = function () {
        $('#m_login_signin_submit').click(function (e) {
            var form = $(e.target).closest('form');
            form.validate({
                rules: {
                    email: {
                        required: true,
                        email: true,
                    },
                    password: {
                        required: true,
                    },
                },
            });
            if (!form.valid()) {
                e.preventDefault();
                return;
            }
        });
    };
    LoginCustom.displaySignUpForm = function () {
        var login = $('#m_login');
        login.removeClass('m-login--forget-password');
        login.removeClass('m-login--signin');
        login.addClass('m-login--signup');
        login.find('.m-login__signup').animateClass('flipInX animated');
    };
    LoginCustom.displaySignInForm = function () {
        var login = $('#m_login');
        login.removeClass('m-login--forget-password');
        login.removeClass('m-login--signup');
        try {
            $('form').data('validator').resetForm();
        }
        catch (e) {
        }
        login.addClass('m-login--signin');
        login.find('.m-login__signin').animateClass('flipInX animated');
    };
    LoginCustom.displayForgetPasswordForm = function () {
        var login = $('#m_login');
        login.removeClass('m-login--signin');
        login.removeClass('m-login--signup');
        login.addClass('m-login--forget-password');
        login.find('.m-login__forget-password').animateClass('flipInX animated');
    };
    LoginCustom.handleFormSwitch = function () {
        $('#m_login_forget_password').click(function (e) {
            e.preventDefault();
            LoginCustom.displayForgetPasswordForm();
        });
        $('#m_login_forget_password_cancel').click(function (e) {
            e.preventDefault();
            LoginCustom.displaySignInForm();
        });
        $('#m_login_signup').click(function (e) {
            e.preventDefault();
            LoginCustom.displaySignUpForm();
        });
        $('#m_login_signup_cancel').click(function (e) {
            e.preventDefault();
            LoginCustom.displaySignInForm();
        });
    };
    LoginCustom.handleSignUpFormSubmit = function () {
        $('#m_login_signup_submit').click(function (e) {
            var btn = $(e.target);
            var form = $(e.target).closest('form');
            form.validate({
                rules: {
                    fullname: {
                        required: true,
                    },
                    email: {
                        required: true,
                        email: true,
                    },
                    password: {
                        required: true,
                    },
                    rpassword: {
                        required: true,
                    },
                    agree: {
                        required: true,
                    },
                },
            });
            if (!form.valid()) {
                e.preventDefault();
                return;
            }
        });
    };
    LoginCustom.handleForgetPasswordFormSubmit = function () {
        $('#m_login_forget_password_submit').click(function (e) {
            var btn = $(e.target);
            var form = $(e.target).closest('form');
            form.validate({
                rules: {
                    email: {
                        required: true,
                        email: true,
                    },
                },
            });
            if (!form.valid()) {
                e.preventDefault();
                return;
            }
        });
    };
    LoginCustom.init = function () {
        LoginCustom.handleFormSwitch();
        LoginCustom.handleSignInFormSubmit();
        LoginCustom.handleSignUpFormSubmit();
        LoginCustom.handleForgetPasswordFormSubmit();
    };
    return LoginCustom;
}());
export { LoginCustom };
//# sourceMappingURL=login-custom.js.map