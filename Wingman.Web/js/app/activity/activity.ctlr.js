﻿angular.module('app').controller('ActivityController', function ($scope, ActivityResource, SubmissionResource, ResponseResource, TopicResource, $http, apiUrl) {

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

    $scope.pickedResponse = function (response) {
        $http.post(apiUrl + "submissions/close", response)
        .success(function () {
            alert("Success");
        });
        activate();
    };

    function activate() {
        $scope.topics = TopicResource.query();
        ActivityResource.getUserSubmissions().then(function (response) {
            $scope.submissions = response;
        });
        ActivityResource.getUserResponses().then(function (response) {
            $scope.responses = response;
        });
    }

    activate();
});