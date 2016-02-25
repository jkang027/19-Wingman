angular.module('app').controller('SubmissionController', function ($scope, SubmissionResource) {

    function activate() {
        $scope.submission = SubmissionResource.query();
    }

    $scope.deleteSubmission = function (submission) {
        submission.$remove(function () {
            alert('Question Removed');
            activate();
        });
    };

    $scope.createSubmission = function () {
        SubmissionResource.save($scope.newSubmission, function () {
            $scope.newSubmission = {};
        });
    };

    activate();
});