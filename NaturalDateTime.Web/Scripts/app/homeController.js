var app = angular.module('naturalDateTime', []);
app.controller('naturalDateTime').controller('homeController', ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.loading = false;
    $scope.askQuestion = function () {
        $scope.name = $scope.question;
        $scope.loading = true;
        $http.post("/api/question", { question: $scope.question, client: "web", client_version: "2.0" }).
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

    var randomQuestions = ["e.g. Whats the time in Sydney when it's 10:30PM in New York?", "e.g. When does daylight saving time start in Nottingham?", "e.g. If its 8pm in Cleveland, whats the time in California?", "e.g. When does daylight saving time start in 2016 in Chicago?"];
    var randomIndex = Math.floor(Math.random() * (randomQuestions.length));
    $scope.placeholder = randomQuestions[randomIndex];
}]);