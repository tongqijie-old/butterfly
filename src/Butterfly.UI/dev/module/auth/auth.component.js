angular.module('auth').component('auth', {
    templateUrl: 'module/auth/auth.template.html',
    controller: ['$window', '$interval', '$scope', '$cookies', '$routeParams',
        function AuthController($window, $interval, $scope, $cookies, $routeParams) {

            $scope.delay = 3;

            var stop = $interval(function () {
                $scope.delay = $scope.delay - 1;
                if ($scope.delay == 0) {
                    $interval.cancel(stop);
                    $window.location.href = 'http://localhost:50119/auth/blog?ref=' + encodeURIComponent('http://localhost:59978/auth/p');
                }
            }, 1000);
        }]
});