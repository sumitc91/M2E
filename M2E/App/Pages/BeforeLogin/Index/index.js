BeforeLoginApp.controller('beforeLoginIndex', function ($scope, $http, $rootScope, CookieUtil) {
    $scope.logoImage = { url: logoImage };
    $('title').html("MadeToEarn");
    $scope.beforeLoginFooterCopyRightInfo = {
        companyName: "MadeToEarn"
    };
});
