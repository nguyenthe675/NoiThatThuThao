/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shyApp.app_users', ['shyApp.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('app_users', {
            url: "/app_users",
            templateUrl: "/app/components/application_users/applicationUserListView.html",
            parent: 'base',
            controller: "applicationUserListController"
        })
            .state('add_app_user', {
                url: "/add_app_user",
                parent: 'base',
                templateUrl: "/app/components/application_users/applicationUserAddView.html",
                controller: "applicationUserAddController"
            })
            .state('edit_app_user', {
                url: "/edit_app_user/:id",
                templateUrl: "/app/components/application_users/applicationUserEditView.html",
                controller: "applicationUserEditController",
                parent: 'base',
            });
    }
})();