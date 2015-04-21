app.controller('TwilioController', function ($scope, $http, $location) {
    $scope.msg = {
        From: "+18324101832",
        To: "+18326713608",
        Body: "Este Msg es desde la compu"
    };

    $scope.ListPoll;
    $scope.CampaignKeyWord = [];
    $scope.SurveyClients = {};

    $scope.GetCampaignKeyWords = function () {
        $http.get('/api/apiCampaign').success(function (data, status) {

            for (var x in data) {
                var i = data[x];
                if (i.CampaignActive == true) {
                    $scope.CampaignKeyWord.push(i.CampaignKeyWord)
                    console.log($scope.CampaignKeyWord);
                }
            }
        }).error(function (data, status) {
            alert("Error getting campaigns : " + status);
        })
    }


    $scope.GetListMsgs = {};

    $scope.SendMsg = function () {
        $http.post('/api/apiTwilio', $scope.msg).success(function (data, status) {
            alert("MSG has been sent");
        }).error(function (data, status) {
            alert("Error sending MSG");
        })
    }

    $scope.getList = function () {
        // Gets todays List of Msgs
        $http.get('/api/apiTwilio/').success(function (data, status) {
            for (var x in data) {
                var i = data[x];
                console.log(i.Body);
                if (i.Body == $scope.CampaignKeyWord) {
                    console.log(i.From + " Has a matching Keyword");
                    $scope.SurveyClients.push(i);
                }

            }
        }).error(function (data, status) {
            alert("Error in get list: " + status);
        })
    }

    $scope.MsgPoll = function() {
        $scope.ListPoll = setTimeout(function () {
            $scope.getList();
            $scope.MsgPoll();
        }, 5000);
    }

    $scope.StopMsgPoll = function() {
        clearTimeout($scope.ListPoll);
    }
})