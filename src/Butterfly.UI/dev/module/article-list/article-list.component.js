angular.module('articleList').component('articleList', {
    // Note: The URL is relative to our `index.html` file
    templateUrl: 'module/article-list/article-list.template.html',
    controller: ['$routeParams', '$http', '$location', '$anchorScroll',
        function ArticleListController($routeParams, $http, $location, $anchorScroll) {
            var self = this;

            var pageNumber = Number($routeParams.pageNumber);
            if (pageNumber == NaN) {
                return;
            }

            //$location.hash('pageTop');
            //$anchorScroll();

            $http.get('as/gabp?pn=' + pageNumber, {
                headers: {
                    'Content-Type': 'application/json',
                    'ACCEPT': 'application/json'
                }
            }).then(function (response) {
                if (response.status == 200) {
                    if (response.data.error == undefined || response.data.error == false) {
                        self.articles = response.data.data.a;
                        self.paging = response.data.data.pg;
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