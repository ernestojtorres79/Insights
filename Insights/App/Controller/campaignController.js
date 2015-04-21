app.controller('campaignController', function ($scope, $http, $location, $route, $routeParams) {
    $scope.newCampaign = {};
    $scope.upCampaign = {};
    $scope.singleCampaign = {};
    $scope.AllQuestions = {};
    $scope.newQuestion = {};
    $scope.singleQuestion = {};
   

    //$scope.hashCode = function (s) {
    //    return s.split("").reduce(function (a, b) { a = ((a << 5) - a) + b.charCodeAt(0); return a & a }, 0);
    //}

    $scope.newActiveCampaign = function () {
        console.log($scope.newCampaign);
        $http.post('/api/apiCampaign/', $scope.newCampaign).success(function (data, success) {
            console.log($scope.newCampaign);
            $location.path('/home');
        }).error(function (data, status) {
            alert(status);
        });
    }

    $scope.getSingleCampaign = function () {
        $http.get('/api/apiCampaign/' + $routeParams.id).success(function (data, status) {
            $scope.singleCampaign = data;
        }).error(function (data, status) {
            alert(status);
        });
    }

    $scope.updateCampaign = function () {
        console.log($scope.singleCampaign);
        $http.put('/api/apiCampaign/', $scope.singleCampaign).success(function (data, status) {
            $location.path('/home');
        }).error(function (data, status) {
            alert(status);
        })
    }

    $scope.getAllQuestions = function () {
        $http.get('/api/apiQuestionsList/' + $routeParams.id).success(function (data, status) {
            $scope.AllQuestions = data;
        }).error(function (data, status) {
            alert("Can Find Api");
        })
    }

    $scope.addNewQuestion = function () {
        $scope.newQuestion.CampaignId = $routeParams.id;
        console.log($scope.newQuestion);
        $http.post('/api/apiQuestion/', $scope.newQuestion).success(function (data, status) {
            alert("Question has been Added");
            $route.reload();
        }).error(function (data, status) {
            alert(status);
        })
    }

    $scope.getSingleQuestion = function () {
        console.log($routeParams.id);
        $http.get('/api/apiQuestion/' + $routeParams.id).success(function (data, status) {
            $scope.singleQuestion = data;
            console.log(data);
        }).error(function (data, status) {
            alert("Api Question not valid :" + status);
        })
    }

    $scope.updateQuestion = function () {
        console.log($scope.singleQuestion);
        $http.put('/api/apiQuestion', $scope.singleQuestion).success(function (data, status) {
            $location.path('/questionsList/' + $scope.singleQuestion.CampaignId);
        }).error(function (data, status) {
            alert(status);
        })
    }
})