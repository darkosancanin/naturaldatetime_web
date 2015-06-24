angular.module('naturalDateTime').controller('mainController', ["$scope", "$http", "$routeParams", "$location", function ($scope, $http, $routeParams, $location) {
    $scope.debugInfoEnabled = false;
    $scope.loading = false;
    $scope.note = undefined;
    $scope.askQuestion = function () {
        $scope.name = $scope.question;
        $scope.loading = true;
        $http.get("api/question?q=" + encodeURIComponent($scope.question)).
          success(function (data, status, headers, config) {
              $scope.loading = false;
              $scope.answer = data.AnswerText;
              $scope.note = data.Note;
              $scope.debugInfo = data.DebugInformation;
          }).
          error(function (data, status, headers, config) {
              $scope.loading = false;
              $scope.answer = "Oops, something went wrong. Please try again.";
          });
    };

    if ($routeParams.q != undefined && $routeParams.q.length > 0) {
        $scope.question = $routeParams.q
        $scope.askQuestion();
    }

    if ($routeParams.debug != undefined && $routeParams.debug === 'true') {
        $scope.debugInfoEnabled = true;
    }


    var randomQuestions = ["e.g. Whats the time in Sydney when it's 10:30PM in New York?", "e.g. When does daylight saving time start in Japan?", "e.g. If its 8pm in Cleveland, whats the time in California?", "e.g. When does daylight saving time start in 2016 in Chicago?"];
    var randomIndex = Math.floor(Math.random() * (randomQuestions.length));
    $scope.placeholder = randomQuestions[randomIndex];
}]);