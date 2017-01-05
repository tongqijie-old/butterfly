angular.module('articleDetail').component('articleDetail', {
    // Note: The URL is relative to our `index.html` file
    templateUrl: 'module/article-detail/article-detail.template.html',
    controller: ['$routeParams', '$http', '$sce', '$scope',
      function ArticleDetailController($routeParams, $http, $sce, $scope) {
          var self = this;

          $http.get('as/gabi?id=' + $routeParams.articleId)
              .then(function (response) {
                  if (response.status == 200) {
                      if (response.data.error == undefined || response.data.error == false) {
                          self.article = response.data.data;
                          $scope.html = response.data.data.content;
                      }
                      else {
                          // business error
                      }
                  }
                  else {
                      // network error
                  }
              });
      }]
});