angular.module('app').controller('ActivityController', function ($scope, SubmissionResource, TopicResource) {

    function activate() {
        $scope.submissions = SubmissionResource.query();
        $scope.topics = TopicResource.query();
    }

    $scope.topicselect = function (topic) {
        $scope.Topic = topic;
        $scope.TopicId = topic.TopicId;
        $scope.newSubmission.TopicId = topic.TopicId;
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
            $scope.Topic = {};
        });
    };

    activate();
});