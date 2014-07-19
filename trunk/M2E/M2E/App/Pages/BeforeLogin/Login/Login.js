BeforeLoginApp.controller('beforeLoginSignInController', function ($scope, $http, $rootScope, CookieUtil) {
    $scope.EmailId = "";
    $scope.Password = "";
    $scope.showHeaderErrors = false;
    $scope.showFooterErrors = false;
    $scope.EmailIdAlert = {
        visible: false,
        message: ''
    };
    $scope.PasswordAlert = {
        visible: false,
        message: ''
    };
    $scope.HeaderAlert = {
        visible: false,
        message: ''
    };
    $scope.ForgetPasswordAlert = {
        visible: false,
        message: ''
}

    $scope.Login = function () {

        var userLoginData = {
            Username: $scope.EmailId,
            Password: $scope.Password,
            Type: 'web',
            KeepMeSignedInCheckBox: 'checked',            
        }
        var url = ServerContextPah + '/Auth/Login';
        var validatePassword = false;

        if (isValidEmailAddress($scope.EmailId)) {
            $scope.EmailIdAlert.visible = false;
            $scope.EmailIdAlert.message = "";
        }
        else {
            $scope.EmailIdAlert.visible = true;
            $scope.EmailIdAlert.message = "Entered emil id is invalid. Please enter valid email id.";
        }
        if ($scope.Password != "") {
            validatePassword = true;
            $scope.PasswordAlert.visible = false;
            $scope.PasswordAlert.message = "";
        }
        else {
            $scope.PasswordAlert.visible = true;
            $scope.PasswordAlert.message = "Password cannot be empty !!!";
        }
        
        if (isValidEmailAddress($scope.EmailId) && validatePassword) {
            startBlockUI('wait..', 3);
            $http({
                url: url,
                method: "POST",
                data: userLoginData,
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                stopBlockUI();
                if (data.Status == "401") {
                    showToastMessage("Notice", "The username/password combination is incorrect !");
                    $scope.showHeaderErrors = true;
                    $scope.HeaderAlert.visible = true;
                    $scope.HeaderAlert.message = "The username/password combination you entered is incorrect. Please try again(make sure your caps lock is off).";
                    $scope.ForgetPasswordAlert.visible = true;
                    $scope.ForgetPasswordAlert.message = "Forgot your password?";
                }
                else if (data.Status == "500") {
                    $scope.showHeaderErrors = true;
                    $scope.HeaderAlert.visible = true;
                    $scope.HeaderAlert.message = "Internal server error occured. Please try again.";
                    showToastMessage("Error", "Internal Server Error Occured !");
                }
                else if (data.Status == "403") {
                    showToastMessage("Warning", "Your Account is not verified. Please check your mail !");
                    $scope.showHeaderErrors = true;
                    $scope.HeaderAlert.visible = true;
                    $scope.HeaderAlert.message = "Your Account is not verified yet. Please check your mail and verfiy your account.";
                }
                else if (data.Status == "200") {
                    showToastMessage("Success", "Successfully Logged in !");
                    console.log("data : " + data);
                    location.href = "/client";
                }
                    
            }).error(function (data, status, headers, config) {

            });
        }
        else {
            $scope.showFooterErrors = true;
            showToastMessage("Error", "Some Fields are Invalid !!!");
        }

    }
});

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
};

function isValidFormField(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
};