/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shyApp.post_categories', ['shyApp.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('post_categories', {
            url: "/post_categories",
            templateUrl: "/app/components/post_categories/postCategoryListView.html",
            parent: 'base',
            controller: "postCategoryListController"
        })
            .state('add_post_category', {
                url: "/add_post_category",
                parent: 'base',
                templateUrl: "/app/components/post_categories/postCategoryAddView.html",
                controller: "postCategoryAddController"
            })
            .state('edit_post_category', {
                url: "/edit_post_category/:id",
                templateUrl: "/app/components/post_categories/postCategoryEditView.html",
                controller: "postCategoryEditController",
                parent: 'base',
            });
    }
})();