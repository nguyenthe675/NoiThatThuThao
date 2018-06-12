(function (app) {
    app.controller('postCategoryAddController', postCategoryAddController);

    postCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function postCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.postCategory = {
            CreatedDate: new Date(),
            Status: true,
        }
        $scope.flatFolders = [];
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }

        $scope.AddPostCategory = AddPostCategory;

        function AddPostCategory() {
            apiService.post('api/postcategory/create', $scope.postCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('post_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadParentCategory() {
            apiService.get('api/postcategory/getallparents', null, function (result) {
                console.log(result);
                $scope.parentCategories = commonService.getTree(result.data, "ID", "ParentID");
                $scope.parentCategories.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Cannot get list parent');
            });
        }
        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };
        loadParentCategory();
    }

})(angular.module('shyApp.post_categories'));