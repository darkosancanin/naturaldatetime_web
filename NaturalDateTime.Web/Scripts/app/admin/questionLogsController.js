var app = angular.module('naturalDateTime.admin', ['ui.grid', 'ui.grid.pagination', 'ui.grid.resizeColumns', 'ui.grid.selection', 'ui.bootstrap']);
app.controller('questionLogsController', ['$scope', '$http', 'uiGridConstants', '$modal', function ($scope, $http, uiGridConstants, $modal) {

    var paginationOptions = {
        pageNumber: 1,
        pageSize: 25,
        sort: null
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
        multiSelect: false,
        enableRowSelection: true,
        enableSelectAll: false,
        enableRowHeaderSelection: false,
        paginationPageSizes: [25, 50, 75],
        paginationPageSize: 25,
        useExternalPagination: true,
        columnDefs: [
          { name: 'SydneyTime', displayName: 'Sydney Time', width: 200 },
          { name: 'Question' }
        ],
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
            gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                paginationOptions.pageNumber = newPage;
                paginationOptions.pageSize = pageSize;
                getQuestionLogEntries();
            });
        },
        appScopeProvider: $scope.myAppScopeProvider,
        rowTemplate: "<div ng-dblclick=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>"
    };

    var getQuestionLogEntries = function () {
        var url = '/admin/questionLogEntries?page=' + paginationOptions.pageNumber + '&pageSize=' + paginationOptions.pageSize;

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