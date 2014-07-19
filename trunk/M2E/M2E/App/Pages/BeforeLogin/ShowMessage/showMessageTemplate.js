BeforeLoginApp.controller('showMessageTemplate', function ($scope, $http, $routeParams, $location) {
    $scope.code = $routeParams.code;
    $scope.Header = {
        message: '',
        className: '',
        iconClassName:''
    };
    $scope.RegisterContent = false;
    $scope.ForgetPasswordContent = false;
    $scope.Content = {
        header1: '',
        header2: '',
        companyName: '',
        email: '',
        contentClasstheme: '',
        header2IconClassName: ''
    };
    if ($scope.code == 1) {
        $scope.RegisterContent = true;
        $scope.Header.message = "Verify your email address.";
        $scope.Header.className = "alert-success";
        $scope.Header.iconClassName = "fa-check";
        $scope.Content.header1 = "Welcome to";
        $scope.Content.companyName = "MadeToEarn";
        $scope.Content.contentClasstheme = "callout-info";
        $scope.Content.header2IconClassName = "fa fa-info";
        $scope.Content.header2 = "Confirm Your Email Address.";
        $scope.Content.email = getParameterByName("email");
    }
    else if ($scope.code == 2) {
        $scope.ForgetPasswordContent = true;
        $scope.Header.message = "Password Reset.";
        $scope.Header.className = "alert-success";
        $scope.Header.iconClassName = "fa-check";
        $scope.Content.header1 = "MadeToEarn";
        $scope.Content.companyName = "";
        $scope.Content.contentClasstheme = "callout-info";
        $scope.Content.header2IconClassName = "fa fa-info";
        $scope.Content.header2 = "Check Your Email Inbox";
        $scope.Content.email = getParameterByName("email");
    }
});

BeforeLoginApp.controller('forgetPasswordTemplate', function ($scope, $http, $routeParams, $location) {
    
    $scope.ForgetPasswordSendRequest = function () {
        if ($('#forgetPasswordInputBoxId').val() != "" && $('#forgetPasswordInputBoxId').val() != null) {

        } else {
            showToastMessage("Error", "Password field cann't be empty.");
        }
    }
});



function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}