
var ClientAfterLoginApp = angular.module('ClientAfterLoginApp', ['ngCookies']);

ClientAfterLoginApp.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../App/Pages/ClientAfterLogin/Index/Index.html" }).
                   when("/signup/user/:ref", { templateUrl: "../../App/Pages/BeforeLogin/SignUpUser/SignUpUser.html" }).
                   when("/signup/client/:ref", { templateUrl: "../../App/Pages/BeforeLogin/SignUpClient/SignUpClient.html" }).
                   when("/signup/user", { templateUrl: "../../App/Pages/BeforeLogin/SignUpUser/SignUpUser.html" }).
                   when("/signup/client", { templateUrl: "../../App/Pages/BeforeLogin/SignUpClient/SignUpClient.html" }).
                   when("/login", { templateUrl: "../../App/Pages/BeforeLogin/Login/Login.html" }).
                   when("/faq", { templateUrl: "../../App/Pages/BeforeLogin/FAQ/FAQ.html" }).
                   when("/facebookLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/facebookLogin.html" }).
                   when("/facebookLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/facebookLogin.html" }).
                   when("/googleLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/googleLogin.html" }).
                   when("/googleLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/googleLogin.html" }).
                   when("/linkedinLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/linkedinLogin.html" }).
                   when("/linkedinLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/linkedinLogin.html" }).
                   when("/validate/:userName/:guid", { templateUrl: "../../Resource/templates/beforeLogin/contentView/validateAccount.html" }).
                   when("/tnc", { templateUrl: "../../Resource/templates/beforeLogin/contentView/termsAndConditions.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/AboutUs", { templateUrl: "../../App/Pages/BeforeLogin/AboutUs/AboutUs.html" }).
                   when("/forgetpassword", { templateUrl: "../../Resource/templates/beforeLogin/contentView/forgetPassword.html" }).
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