var app = angular.module('naturalDateTime.admin', ['ui.grid', 'ui.grid.pagination', 'ui.grid.resizeColumns', 'ui.grid.selection', 'ui.bootstrap']);
app.controller('latestQuestionLogsController', ['$scope', '$http', 'uiGridConstants', '$modal', function ($scope, $http, uiGridConstants, $modal) {

    $scope.showBotRequests = false;

    $scope.onOptionsChanged = function () {
        getQuestionLogEntries();
    };

    $scope.myAppScopeProvider = {
        showInfo: function (row) {
            var modalInstance = $modal.open({
                controller: 'questionLogDetailsController',
                templateUrl: 'questionLogDetails.html',
                resolve: {
                    questionLog: function () {
                        return row.entity;
                    }
                }
            });
        }
    }

    $scope.gridOptions = {
        showFooter: false,
        enableSorting: false,
        enableColumnMenus: false,
        multiSelect: false,
        enableRowSelection: true,
        enableSelectAll: false,
        enableRowHeaderSelection: false,
        useExternalPagination: false,
        columnDefs: [
          { name: 'SydneyTime', displayName: 'Sydney Time', width: 220 },
          { name: 'Question' },
          { name: 'Client', width: 70 },
          { name: 'Version', width: 80 },
          { name: 'IsBot', displayName: 'Bot', width: 50 }
        ],
        appScopeProvider: $scope.myAppScopeProvider,
        rowTemplate: "<div ng-dblclick=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>"
    };

    var getQuestionLogEntries = function () {
        var url = '';
        if ($scope.showBotRequests) {
            url = '/admin/latestBotQuestionLogs';
        }
        else {
            url = '/admin/latestUserQuestionLogs';
        }
            
        $http.get(url)
        .success(function (data) {
            $scope.gridOptions.totalItems = data.TotalResults;
            $scope.gridOptions.data = data.QuestionLogs;
        });
    };

    getQuestionLogEntries();
}
]);

app.controller('questionLogDetailsController', ['$scope', '$modal', '$modalInstance', '$filter', '$interval', 'questionLog',
    function ($scope, $modal, $modalInstance, $filter, $interval, questionLog) {
        $scope.questionLog = questionLog;

        $scope.ok = function () {
            $scope.questionLog = null;
            $modalInstance.close();
        };
    }
]);