function treeSelect($http) {
    var scope;

    return {
        restrict: "A",
        controller: function ($scope) {
            scope = $scope;
        },
        compile: function ($element, attr) {
            var url = attr.treeSelect;
            var event = attr.treeevent;
            var objectid = attr.objectid;
            var taskid = attr.taskid;
            $http({
                method: 'GET',
                url: url + "?id=" + objectid,
                data: 'JSON'
            }).success(function (result) {
                $element.jstree({
                    'core': {
                        'check_callback': true,
                        'data': result.Data
                    },
                    'plugins': ['types', 'dnd'],
                    'types': {
                        'default': {
                            'icon': 'fa fa-user'
                        },
                        'new': {
                            'icon': 'fa fa-exclamation-circle text-success'
                        },
                        'work': {
                            'icon': 'fa fa-play-circle text-warning'
                        },
                        'executed': {
                            'icon': 'fa fa-check-circle text-info2'
                        },
                        'review': {
                            'icon': 'fa fa-folder'
                        },
                        'calculation': {
                            'icon': 'fa fa-folder'
                        },
                        'completeness': {
                            'icon': 'fa fa-folder'
                        },
                        'resolution': {
                            'icon': 'fa fa-folder'
                        },
                        'ses': {
                            'icon': 'fa fa-folder'
                        },
                        'eco': {
                            'icon': 'fa fa-folder'
                        },
                        'conclusion': {
                            'icon': 'fa fa-folder'
                        },
                        'expertise': {
                            'icon': 'fa fa-folder'
                        },
                        'correspondence': {
                            'icon': 'fa fa-folder'
                        },
                        'contract': {
                            'icon': 'fa fa-folder'
                        }

                    }
                });
                $element.on("loaded.jstree", function (e, data) {
                    $element.jstree(true).select_node(taskid);
                    $element.on("changed.jstree", function (e, data) {
                        scope[event](data.selected);
                    });
                });

                scope.reloadTree = function (items) {
                    var currentNode = $element.jstree("get_selected");
   
                    items.forEach(function (item, i, arr) {
                        var id = $element.jstree('create_node', currentNode, item, 'last');
                    });
                   
                }

                scope.deleteNode = function (id) {
                    var currentNode = $element.jstree("get_selected");
                    $element.jstree("delete_node", currentNode);
                   
                }
            });

            

        }
    };
}

function dynamic($compile) {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, ele, attrs) {
            scope.$watch(attrs.dynamic, function (html) {
                if (html != undefined) {
                    scope.TepmHtml = ele.html();
                    ele.html(html);
                    $compile(ele.contents())(scope);
                } 
            });
        }
    };
}

function touchSpin() {
    return {
        restrict: 'A',
        scope: {
            spinOptions: '='
        },
        link: function (scope, element, attrs) {
            scope.$watch(scope.spinOptions, function () {
                render();
            });
            var render = function () {
                $(element).TouchSpin(scope.spinOptions);
            };
        }
    }
};

angular
    .module('app')
    .directive('dynamic', dynamic)
    .directive('treeSelect', treeSelect)
    .directive('touchSpin', touchSpin);