angular.module('app').controller('WingmanController', function ($scope, SubmissionResource, ResponseResource) {
    //TODO: Add Kanban stuff.
    function activate() {
        $scope.submissions = SubmissionResource.query();
        $scope.responses = ResponseResource.query();
     
    }

    $scope.createResponse = function () {
        ResponseResource.save($scope.newResponse, function () {
            $scope.newResponse = {};           
        });
    };




    activate();
});