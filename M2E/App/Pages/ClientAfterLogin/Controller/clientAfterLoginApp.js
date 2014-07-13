
var ClientAfterLoginApp = angular.module('ClientAfterLoginApp', ['ngCookies']);

ClientAfterLoginApp.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../App/Pages/ClientAfterLogin/Index/Index.html" }).
                   when("/edit", { templateUrl: "../../App/Pages/ClientAfterLogin/EditPage/EditPage.html" }).
                   when("/createTemplate", { templateUrl: "../../App/Pages/ClientAfterLogin/CreateTemplate/CreateTemplate.html" }).
                  
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

ClientAfterLoginApp.run(function ($rootScope, $location) { //Insert in the function definition the dependencies you need.
    
    $rootScope.$on("$locationChangeStart",function(event, next, current){        ;
        gaWeb("BeforeLogin-Page Visited", "Page Visited", next);
        var path = next.split('#');        
        gaPageView(path,'title');            
    });
});


function loadjscssfile(filename, filetype) {
    var fileref = "";
    if (filetype == "js") { //if filename is a external JavaScript file
        fileref = document.createElement('script');
        fileref.setAttribute("type", "text/javascript");
        fileref.setAttribute("src", filename);
    }
    else if (filetype == "css") { //if filename is an external CSS file
        fileref = document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", filename);
    }
    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref);
}

//loadjscssfile("../../App/Pages/BeforeLogin/SignUpClient/signUpClientController.js", "js"); //dynamically load and add this .js file
//loadjscssfile("../../App/Pages/BeforeLogin/Controller/common/CookieService.js", "js"); 