/// <reference path="../../Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shyApp.posts', ['shyApp.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('post', {
                url: "/post",
                parent: 'base',
                templateUrl: "/app/components/post/postListView.html",
                controller: "postListController"
            }).state('post_add', {
                url: "/post_add",
                parent: 'base',
                templateUrl: "/app/components/post/postAddView.html",
                controller: "postAddController"
            })
            .state('post_import', {
                url: "/post_import",
                parent: 'base',
                templateUrl: "/app/components/post/postImportView.html",
                controller: "postImportController"
            })
            .state('post_edit', {
                url: "/post_edit/:id",
                parent: 'base',
                templateUrl: "/app/components/post/postEditView.html",
                controller: "postEditController"
            });
    }
})();