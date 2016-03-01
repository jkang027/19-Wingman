angular.module('app').controller('ActivityController', function ($scope, ActivityResource, SubmissionResource, ResponseResource, TopicResource) {



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





    function activate() {
        $scope.topics = TopicResource.query();

        $scope.submissions = ActivityResource.query(function (data) {
            $scope.submissions = data;
            $scope.submissions.forEach(function (submission) {
                submission.responses = ResponseResource.responses({ SubmissionId: submission.SubmissionId });
            });
        });
    }


    activate();
});