ClientAfterLoginApp.controller('ClientAfterLoginIndex', function ($scope, $http, $rootScope, CookieUtil) {
    $scope.logoImage = { url: logoImage };  
    $('title').html("Client - MadeToEarn");

    $scope.InProgressTaskList = [{ showEllipse: true, title: "my first template", timeShowType: "info", showTime: "5 hours", editId: "", creationDate: "an 2014" },
        { showEllipse: true, title: "my second template", timeShowType: "danger", showTime: "2 hours", editId: "", creationDate: "feb 2013" },
        { showEllipse: true, title: "my third template", timeShowType: "warning", showTime: "1 day", editId: "", creationDate: "march 3023" },
        { showEllipse: true, title: "my fourth template", timeShowType: "success", showTime: "3 days", editId: "", creationDate: "aug 1203" },
        { showEllipse: true, title: "my fifth template", timeShowType: "default", showTime: "5 hours", editId: "", creationDate: "nov 2015" }
    ];
});
