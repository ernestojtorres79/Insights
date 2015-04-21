var app = angular.module('app', ['ngRoute']);

app.config(function ($routeProvider, $httpProvider) {
    $routeProvider.when('/', {
        templateUrl: "/App/Views/Index.html",
        controller: "NavBarController",
        title: "Insights by Maja GPX"
    }).when('/home', {
        templateUrl: "/App/Views/Home.html",
        controller: "HomeController",
        title: "Insights by Maja GPX"
    }).when('/profile', {
        templateUrl: "/App/Views/Profile.html",
        controller: "ProfileController",
        title: "Insights by Maja GPX"
    }).when('/createCampaign', {
        templateUrl: "/App/Views/createCampaign.html",
        controller: "campaignController",
        title: "Insights by Maja GPX"
    }).when('/addQues/:id', {
        templateUrl: "/App/Views/addQues.html",
        controller: "campaignController",
        title: "Insights by Maja GPX"
    }).when('/update/:id', {
        templateUrl: "/App/Views/updateCampaign.html",
        controller: "campaignController",
        title: "Insights by Maja GPX"
    }).when('/questionsList/:id', {
        templateUrl: "/App/Views/questionsList.html",
        controller: "campaignController",
        title: "Insights by Maja GPX"
    }).when('/singleques/:id', {
        templateUrl: "/App/Views/singleQuestion.html",
        controller: "campaignController",
        title: "Insights by Maja GPX"
    }).when('/twilioTest', {
        templateUrl: "/App/Views/testTwilio.html",
        controller: "TwilioController",
        title: "Insights by Maja GPX"
    }).otherwise({
        redirectTo: '/'
    });
    $httpProvider.interceptors.push('TokenInterceptor');
});