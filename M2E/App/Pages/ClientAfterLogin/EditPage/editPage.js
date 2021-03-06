ClientAfterLoginApp.controller('ClientAfterLoginEditPage', function ($scope, $http, $rootScope, CookieUtil) {
    $('title').html("edit page"); //TODO: change the title so cann't be tracked in log
    loadClientDetails();

    function loadClientDetails() {
        var url = ServerContextPah + '/Client/GetClientDetails?username=' + userSession.username;
        startBlockUI('wait..', 3);
        $http({
            url: url,
            method: "POST",
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data, status, headers, config) {
            //$scope.persons = data; // assign  $scope.persons here as promise is resolved here
            stopBlockUI();
            if (data.Status == "200") {
                $rootScope.clientDetailResponse = data.Payload;
            }
            else if (data.Status == "404") {

                alert("This template is not present in database");
            }
            else if (data.Status == "500") {

                alert("Internal Server Error Occured");
            }
        }).error(function (data, status, headers, config) {

        });
    }
});
