
BeforeLoginApp.controller('signUpClientController', function ($scope, $http, $routeParams, CookieUtil) {
    $scope.ClientFormData = {
        FirstName: "",
        LastName: "",
        EmailId: "",
        Password: "",
        ConfirmPassword: "",
        CompanyName: ""
    };
    $scope.showErrors = false;
    $scope.EmailIdAlert = {
        visible: false,
        message: ''
    };
    $scope.PasswordAlert = {
        visible: false,
        message: ''
    };
    $scope.FirstNameAlert = {
        visible: false,
        message: ''
    };
    $scope.LastNameAlert = {
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
            LastName: $scope.ClientFormData.LastName,
            Username: $scope.ClientFormData.EmailId,
            Password: $scope.ClientFormData.Password,
            CompanyName: $scope.ClientFormData.CompanyName,
            Type: 'client',
            Source: 'web',
            Referral: 'sumitReferral' // TODO: need to update with referral id.
        }
        var url = ServerContextPah + '/Auth/CreateAccount';
        var validateEmail = false;
        var validatePassword = false;
        var validateFirstName = false;
        var validateLastName = false;
        $scope.showErrors = false;

        if ($scope.ClientFormData.FirstName != "") {
            validateFirstName = true;
            $scope.FirstNameAlert.visible = false;
            $scope.FirstNameAlert.message = "";
        }
        else {
            $scope.FirstNameAlert.visible = true;
            $scope.FirstNameAlert.message = "First Name Cannot be Empty !!!";
        }

        if ($scope.ClientFormData.LastName != "") {
            validateLastName = true;
            $scope.LastNameAlert.visible = false;
            $scope.LastNameAlert.message = "";
        }
        else {
            $scope.LastNameAlert.visible = true;
            $scope.LastNameAlert.message = "Last Name Cannot be Empty !!!";
        }

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
            if ($scope.ClientFormData.Password != "") {
                validatePassword = true;
                $scope.PasswordAlert.visible = false;
                $scope.PasswordAlert.message = "";
            }
            else {
                $scope.PasswordAlert.visible = true;
                $scope.PasswordAlert.message = "Your Password Cannot be Empty !!!";
            }

        }
        else {
            $scope.PasswordAlert.visible = true;
            $scope.PasswordAlert.message = "Password didn't match !!!";
        }

        if (validateEmail && validatePassword && validateFirstName && validateLastName) {
            startBlockUI('wait..', 3);
            $http({
                url: url,
                method: "POST",
                data: clientSignUpData,
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data, status, headers, config) {
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
                    
            }).error(function (data, status, headers, config) {

            });
        }
        else {
            $scope.showErrors = true;
            showToastMessage("Error", "Some Fields are Invalid !!!");
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