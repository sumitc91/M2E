BeforeLoginApp.controller('validateEmailTemplate', function ($scope, $http, $routeParams, $location) {
    var ValidateAccountRequest = {
        userName: $routeParams.userName,
        guid: $routeParams.guid
    };
    $http({
        url: '/Auth/ValidateAccount',
        method: "POST",
        data: ValidateAccountRequest,
        headers: { 'Content-Type': 'application/json' }
    }).success(function (data, status, headers, config) {
        //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
        if (data.Status == "200") {
            alert("successfully validated");
        }
        else if (data.Status == "500") {
            alert("internal server error");
        }
        console.log(data);
    }).error(function (data, status, headers, config) {
        
    });
});



