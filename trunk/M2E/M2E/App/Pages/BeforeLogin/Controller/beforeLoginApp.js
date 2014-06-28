
var BeforeLoginApp = angular.module('BeforeLoginApp', ['ngCookies']);

BeforeLoginApp.config(function ($routeProvider) {

    $routeProvider.when("/", { templateUrl: "../../App/Pages/BeforeLogin/Index/Index.html" }).
                   when("/signup/user/:ref", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signupuser.html" }).
                   when("/signup/client/:ref", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signupclient.html" }).
                   when("/signup/user", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signupuser.html" }).
                   when("/signup/client", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signupclient.html" }).
                   when("/login", { templateUrl: "../../App/Pages/BeforeLogin/Login/Login.html" }).
                   when("/login/:code", { templateUrl: "../../Resource/templates/beforeLogin/contentView/ajax/signInTemplate.html" }).
                   when("/facebookLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/facebookLogin.html" }).
                   when("/facebookLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/facebookLogin.html" }).
                   when("/googleLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/googleLogin.html" }).
                   when("/googleLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/googleLogin.html" }).
                   when("/linkedinLogin/:userType", { templateUrl: "../../Resource/templates/beforeLogin/contentView/linkedinLogin.html" }).
                   when("/linkedinLogin", { templateUrl: "../../Resource/templates/beforeLogin/contentView/linkedinLogin.html" }).
                   when("/validate/:userName/:guid", { templateUrl: "../../Resource/templates/beforeLogin/contentView/validateAccount.html" }).
                   when("/tnc", { templateUrl: "../../Resource/templates/beforeLogin/contentView/termsAndConditions.html" }).
                   when("/404", { templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" }).
                   when("/signup", { templateUrl: "../../Resource/templates/beforeLogin/contentView/signup.html" }).
                   when("/forgetpassword", { templateUrl: "../../Resource/templates/beforeLogin/contentView/forgetPassword.html" }).
                   otherwise({ templateUrl: "../../Resource/templates/beforeLogin/contentView/404.html" });

});

BeforeLoginApp.run(function ($rootScope, $location) { //Insert in the function definition the dependencies you need.
    
    $rootScope.$on("$locationChangeStart",function(event, next, current){
        //Do your things        
        //var path = $location.path();
        gaWeb("BeforeLogin-Page Visited", "Page Visited", next);
        var path = next.split('#');        
        gaPageView(path,'title');            
    });
});


