function uiSelectSelected() {
    return function ($scope, element, attrs) {
        $scope.$watch(attrs.ngModel, function (value) {
            if (value != null) {
                element.removeClass('pw-select-invalid');
                var selectName = attrs.ngModel;
                $('label[for="' + selectName + '"]').removeClass('pw-label-invalid');
            }
        });
    }
}

function uiInputValid() {
    return function ($scope, element, attrs) {
        $scope.$watch(attrs.ngModel, function (value) {
            if (value != null) {
                var selectName = attrs.ngModel;
                if (!element.hasClass('ng-empty .error')) {
                    $('label[for="' + selectName + '"]').removeClass('pw-label-invalid');
                } else if (element.hasClass('error')) {
                    $('label[for="' + selectName + '"]').addClass('pw-label-invalid');
                }
            }
        });
    }
}

function pwMaxLength() {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            var maxlength = Number(attrs.pwMaxLength);
            function fromUser(text) {
                if (text.length > maxlength) {
                    var transformedInput = text.substring(0, maxlength);
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                    return transformedInput;
                }
                return text;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    }
}
function checkValidate() {
    return {
        link: function (scope, element, attrs) {
            element.click(function () {
                //error label
                $(".ng-invalid .ng-invalid-required, .ng-invalid .ng-invalid-pattern, .ng-invalid .ng-invalid-maxlength, .ng-invalid .ng-invalid-minlength").each(function () {
                    var currentElementName = $(this).attr("ng-model");
                    $('label[for="' + currentElementName + '"]').addClass('pw-label-invalid');

                });
                $(".ng-empty").each(function () {
                    var currentElementName = $(this).attr("ng-model");
                    $('label[for="' + currentElementName + '"]').addClass('pw-label-invalid');
                });
                //error label

                //date
                $(".ng-invalid .ng-invalid-parse").addClass("pw-select-invalid");
                //

                //select 
                $(".ng-invalid .ng-invalid-required, .ng-invalid .ng-invalid-pattern, .ng-invalid .ng-invalid-maxlength, .ng-invalid .ng-invalid-minlength").addClass("pw-select-invalid");
                $('.ng-valid').removeClass('pw-select-invalid');
                //select 

                //multi select
                $(".ng-invalid-required .pw-multi").addClass("pw-select-invalid");
                //multi select

            });
        }
    }
}

function disableFinish() {
    return {
        restrict: 'A',
        link: function (scope, element, attrs, model) {
            var button = $('a[href="#' + 'finish"]');
            button.attr("href", '#' + 'finish' + '-disabled');
            button.parent().addClass("disabled");

            scope.toggle = function () {
                scope.isChecked = !scope.isChecked;
                if (scope.isChecked) {
                    alert("true");

                    var button = $('a[href="#' + 'finish' + '-disabled"]');
                    button.attr("href", '#' + 'finish');
                    button.parent().removeClass();
                } else {
                    alert("false");

                    var button = $('a[href="#' + 'finish"]');
                    button.attr("href", '#' + 'finish' + '-disabled');
                    button.parent().addClass("disabled");
                }
            }
        }
    }
}

function uiMultiRequred() {
    return {
        require: 'ngModel',
        link: function ($scope, element, attrs, ctrl) {
            $scope.$watch(attrs.ngModel, function (value) {
                var form = $(this);

                if (value == "") {
                    element.removeClass('ng-valid-required').addClass('ng-invalid-required pw-multi');
                    element.removeClass('ng-valid');
                    form.$valid = false;
                } else {
                    element.removeClass('ng-invalid-required pw-multi').addClass('ng-valid-required');
                    element.removeClass('pw-select-invalid');
                    var selectName = attrs.ngModel;
                    $('label[for="' + selectName + '"]').removeClass('pw-label-invalid');
                }
            });
        }
    }
}

function zeroValidate() {
    return {
        require: 'ngModel',
        link: function ($scope, element, attrs, ctrl) {
            $scope.$watch(attrs.ngModel, function (value) {
                var form = $(this);
         
                if (value == 0) {
                    element.removeClass('ng-valid-required').addClass('ng-invalid-required pw-multi');
                    element.removeClass('ng-valid');
                    form.$valid = false;
                } else {
                    element.removeClass('ng-invalid-required pw-multi').addClass('ng-valid-required');
                    element.removeClass('pw-select-invalid');
                    var selectName = attrs.ngModel;
                    $('label[for="' + selectName + '"]').removeClass('pw-label-invalid');
                }
            });
        }
    }
}

function uiSelectRequired() {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ctrl) {
            ctrl.$validators.uiSelectRequired = function (modelValue, viewValue) {
                if (attr.uiSelectRequired) {
                    var isRequired = scope.$eval(attr.uiSelectRequired);
                    if (isRequired == false)
                        return true;
                }
                var determineVal;
                if (angular.isArray(modelValue)) {
                    determineVal = modelValue;
                } else if (angular.isArray(viewValue)) {
                    determineVal = viewValue;
                } else {
                    return false;
                }
                return determineVal.length > 0;
            };
        }
    }
}

function onlyDigits() {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                if (inputValue == undefined) return '';
                var transformedInput = inputValue.replace(/[^0-9]/g, '');
                if (transformedInput !== inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }
                return transformedInput;
            });
        }
    };
}

function changeble() {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.$watch(attrs.ngModel, function (v) {
                if (v != null && attrs.changeble === "true") {
                    element.change();
                }

            });
        }
    };
}

angular
    .module('app')
    .directive('disableFinish', disableFinish)
    .directive('uiSelectRequired', uiSelectRequired)
    .directive('uiMultiRequred', uiMultiRequred)
    .directive('checkValidate', checkValidate)
    .directive('pwMaxLength', pwMaxLength)
    .directive('uiSelectSelected', uiSelectSelected)
    .directive('zeroValidate', zeroValidate)
    .directive('onlyDigits', onlyDigits)
    .directive('uiInputValid', uiInputValid)
    .directive('changeble', changeble);