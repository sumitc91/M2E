BeforeLoginApp.controller('resetPasswordTemplate', function ($scope, $http, $routeParams, $location) {

    var resetPasswordRequest = {
            userName: $routeParams.userName,
            guid: $routeParams.guid
    };
    $scope.FormData = {
        pass: '',
        repass: ''
    };
    $scope.Header = {
        message: '',
        className: '',
        iconClassName: ''
    };
    $scope.Content = {
        header1: '',
        header2: '',
        contentClasstheme: '',
        header2IconClassName: '',
        message: '',
        companyName: ''
    };
    $http({
        url: '/Auth/ValidateForgetPassword',
        method: "POST",
        data: resetPasswordRequest,
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data, status, headers, config) {
        if (data == "200") {
            showToastMessage("Success", "Password has been successfully changed.");
            location.href = "#/login";
        }
        else if (data == "404") {
            $scope.ForgetPasswordContent = false;
            $scope.ForgetPasswordAlertContent.visible = true;
            $scope.ForgetPasswordAlertContent.message = "Entered email id is not registerd with us. Please enter your email address which is registered with us to set new password.";
        }
        else if (data == "402") {
            $scope.ForgetPasswordContent = false;
            $scope.ForgetPasswordForm = false;
            $scope.ForgetPasswordAlertContent.visible = true; $scope.ForgetPasswordAlertContent.message = "Email Address-" + $('#forgetPasswordInputBoxId').val() + " is not valideted yet. please Please check your email for validation.";
            $scope.ResendValidationOrSignup.visible = true;
            $scope.ResendValidationOrSignup.title = "Don't have emaill address validation Link?";
            $scope.ResendValidationOrSignup.buttonName = "Resend validation link";
            $scope.ResendValidationOrSignup.functionName = "ResendValidationCodeRequest()";
        }
        else if (data == "500") {
            location.href = "/?email=" + $('#forgetPasswordInputBoxId').val() + "#/showmessage/3/";
        }
    }).error(function (data, status, headers, config) {

    });
    var validatePassword = true;
    $scope.PasswordAlertContent = {
        visible: false,
        message: ''
    }
    $scope.resetPasswordRequest = function () {
        if ($scope.ClientFormData.Password == $scope.ClientFormData.ConfirmPassword) {
            if ($scope.FormData.pass != "") {
                validatePassword = true;

            }
            else {
                validatePassword = false;
                $scope.PasswordAlertContent.visible = true;
                $scope.PasswordAlertContent.message = "Your Password Cannot be Empty!!! Please re-enter password";
            }

        }
        else {
            validatePassword = false;
            $scope.PasswordAlertContent.visible = true;
            $scope.PasswordAlertContent.message = "Password didn't match!!! Please re-enter password";
        }
        if (validatePassword) {
            startBlockUI('wait..', 3);
            $http({
                url: url,
                method: "POST",
                data: clientSignUpData,
                headers: { 'Content-Type': 'application/json' }
            }).success(function(data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                stopBlockUI();
                if (data.Status == "409")
                    showToastMessage("Warning", "Username already registered !");
                else if (data.Status == "500")
                    showToastMessage("Error", "Internal Server Error Occured !");
                else if (data.Status == "200") {
                    showToastMessage("Success", "Account successfully created ! check your email for validation.");
                    location.href = "/?email=" + $scope.ClientFormData.EmailId + "#/showmessage/1/";
                }

            }).error(function(data, status, headers, config) {

            });
        } else {
            $scope.showErrors = true;
            showToastMessage("Error", "Some Fields are Invalid !!!");
        }
    }
});

function stopBlockUI() {
    $.unblockUI();
}