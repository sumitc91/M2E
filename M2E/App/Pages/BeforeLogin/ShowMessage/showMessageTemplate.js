BeforeLoginApp.controller('showMessageTemplate', function ($scope, $http, $routeParams, $location) {
    $scope.header = $routeParams.header;
    $scope.content = $routeParams.content;
});



