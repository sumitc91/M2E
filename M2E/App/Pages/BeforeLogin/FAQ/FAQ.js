BeforeLoginApp.controller('beforeLoginFAQ', function ($scope,$location, $http, $rootScope, CookieUtil, $anchorScroll) {
    $scope.scrollTo = function (id) {
        $anchorScroll();
        $location.hash(id);
    };
});
