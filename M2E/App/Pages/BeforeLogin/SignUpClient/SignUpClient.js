
BeforeLoginApp.controller('signUpClientController', function ($scope, $http, $routeParams, CookieUtil) {
    $scope.ClientFormData = {
        FirstName: "",
        LastMame: "",
        EmailId: "",
        Password: "",
        ConfirmPassword: "",
        CompanyName: ""
    };

    $scope.EmailIdAlert = {
        visible: false,
        message: ''
    };
    $scope.PasswordAlert = {
        visible: false,
        message: ''
    };
    $('#client_signup_company_textBox_id').hide();
    $("[name='sliding_client_company_checkbox']").bootstrapSwitch();
    $('input[name="sliding_client_company_checkbox"]').on({
        'switchChange.bootstrapSwitch': function (event, state) {
            if ($(this).is(':checked')) {
                $('#client_signup_company_textBox_id').show();
            } else {
                $('#client_signup_company_textBox_id').hide();
            }
        }
    });

    $scope.ClientSignUp = function () {
        var clientSignUpData = {
            FirstName: $scope.ClientFormData.FirstName,
            LastMame: $scope.ClientFormData.LastMame,
            EmailId: $scope.ClientFormData.EmailId,
            Password: $scope.ClientFormData.Password,
            CompanyName: $scope.ClientFormData.CompanyName
        }
        var url = ServerContextPah + '/Auth/Login/web';
        var validateEmail = false;
        var validatePassword = false;
        if (isValidEmailAddress($scope.ClientFormData.EmailId) && $scope.ClientFormData.EmailId != "") {
            validateEmail = true;
            $scope.EmailIdAlert.visible = false;
            $scope.EmailIdAlert.message = "";
        }
        else {
            $scope.EmailIdAlert.visible = true;
            $scope.EmailIdAlert.message = "Incorrect Email Id !!!";
        }

        if ($scope.ClientFormData.Password == $scope.ClientFormData.ConfirmPassword) {
            validatePassword = true;
            $scope.PasswordAlert.visible = false;
            $scope.PasswordAlert.message = "";
        }
        else {
            $scope.PasswordAlert.visible = true;            
            $scope.PasswordAlert.message = "Password didn't match !!!";
        }

        if (validateEmail && validatePassword) {
            startBlockUI('wait..', 3);
            $http({
                url: url,
                method: "POST",
                data: clientSignUpData,
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data, status, headers, config) {
                //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
                stopBlockUI();

            }).error(function (data, status, headers, config) {

            });
        }
        else {
            showToastMessage("Error", "Some Fields are Invalid !!!")            
        }




    }


});

function startBlockUI(mssg,size) {
    $.blockUI({
        message: '<h' + size + '><img src="../../App/img/loading/loading123.gif" />' + mssg + '</h' + size + '>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function stopBlockUI() {
    $.unblockUI();
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
};

function isValidFormField(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
};