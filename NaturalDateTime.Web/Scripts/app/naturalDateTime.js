var app = angular.module('naturalDateTime', ['ngRoute']);

app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {
            templateUrl: '/main',
            controller: 'mainController'
        }).
        when('/about', {
            templateUrl: '/about'
        }).
        when('/examples', {
            templateUrl: '/examples',
            controller: 'examplesController'
        });
  }]);

app.run(["$rootScope", "$location", "$routeParams", function ($rootScope, $location, $routeParams) {
      $rootScope.$on('$routeChangeSuccess', function () {
          var output = $location.path() + "?";
          angular.forEach($routeParams, function (value, key) {
              output += key + "=" + value + "&";
          })
          output = output.substr(0, output.length - 1);
          ga('send', 'pageview', output);
      });
  }]);
