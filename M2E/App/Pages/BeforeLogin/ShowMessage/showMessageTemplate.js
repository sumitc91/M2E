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
        $scope.Header.message = "Password reset link has been sent.";
        $scope.Header.className = "alert-success";
        $scope.Header.iconClassName = "fa-check";
        $scope.Content.header1 = "MadeToEarn";
        $scope.Content.companyName = "";
        $scope.Content.contentClasstheme = "callout-info";
        $scope.Content.header2IconClassName = "fa fa-info";
        $scope.Content.header2 = "Please check your email";
        $scope.Content.email = getParameterByName("email");
    }
    else if ($scope.code == 3) {
        $scope.ForgetPasswordContent = false;
        $scope.RegisterContent = false;
        $scope.Header.message = "Internal server error has occured.";
        $scope.Header.className = "alert-danger";
        $scope.Header.iconClassName = "fa-ban";
        $scope.Content.header1 = "500";
        $scope.Content.companyName = "Error";
        $scope.Content.contentClasstheme = "callout-danger";
        $scope.Content.header2IconClassName = "fa fa-warning text-yellow";
        $scope.Content.header2 = "Something went wrong. we will soon fix it. Sorry for inconvenience.";
    }
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
