function uiWizardForm(notify) {
    var scope;

    return {
        restrict: "A",
        controller: function ($scope) {
            scope = $scope;
            var button = $('a[href="#' + 'finish"]');
            button.attr("href", '#' + 'finish' + '-disabled');
            button.parent().addClass("disabled");
        },
        compile: function ($element) {

            $element.steps({
                bodyTag: "fieldset",
                onStepChanging: function (event, currentIndex, newIndex) {
                    var countIndexes = $("#form fieldset").length -1;
                    // Always allow going backward even if the current step contains invalid fields!
                    if (currentIndex > newIndex) {
                        return true;
                    }

                    // Forbid suppressing "Warning" step if the user is to young
                    //if (newIndex === 3 && Number($("#age").val()) < 18) {
                    //    return false;
                    //}

                    var form = $(this);
                    //// Clean up if user went backward before
                    if (currentIndex < newIndex) {
             
                        //error label
                        $(".ng-invalid .ng-invalid-required").each(function () {
                            var currentElementName = $(this).attr("ng-model");
                            $('label[for="' + currentElementName + '"]').addClass('pw-label-invalid');

                        });
                        $(".ng-empty").each(function () {
                            var currentElementName = $(this).attr("ng-model");
                            $('label[for="' + currentElementName + '"]').addClass('pw-label-invalid');

                        });
                        //error label

                        //select 
                        $(".ng-invalid .ng-invalid-required").addClass("pw-select-invalid");
                        $('.ng-valid').removeClass('pw-select-invalid');
                        //select 

                        // To remove error styles
                        $(".body:eq(" + newIndex + ") label.error", form).remove();
                        $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
                        $(".body:eq(" + newIndex + ") .pw-label-invalid", form).removeClass("pw-label-invalid");
                        $(".body:eq(" + newIndex + ") .pw-select-invalid", form).removeClass("pw-select-invalid");
                    }
                    if (newIndex == countIndexes) {
                        var button = $('a[href="#' + 'finish' + '-disabled"]');
                        button.attr("href", '#' + 'finish');
                        button.parent().removeClass();
                    }
                    //// Disable validation on fields that are disabled or hidden.
                    form.validate().settings.ignore = ":disabled,:hidden";

                    // Start validation; Prevent going forward if false
                    return form.valid() && !$(".body:eq(" + currentIndex + ") .ng-invalid", form).hasClass('ng-invalid-required');
                },
                onStepChanged: function (event, currentIndex, priorIndex) {
                    // Suppress (skip) "Warning" step if the user is old enough.
                    //if (currentIndex === 2 && Number($("#age").val()) >= 18) {
                    //    $(this).steps("next");
                    //}

                    scope.onStepChanged(event, currentIndex, priorIndex);
                    //// Suppress (skip) "Warning" step if the user is old enough and wants to the previous step.
                    //if (currentIndex === 2 && priorIndex === 3) {
                    //    $(this).steps("previous");
                    //}
                },
                onFinishing: function (event, currentIndex) {
                    var form = $(this);
              
                    //
                    // Disable validation on fields that are disabled.
                    // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
                     form.validate().settings.ignore = ":disabled";

                    // Start validation; Prevent form submission if false
                    return form.valid();
                },
                onFinished: function (event, currentIndex) {
                    var form = $(this);
                    scope.postRequest();
                    // Submit form input
                    //form.submit();
                },

                /* Labels */
                labels: {
                    //current: "current step:",
                    //pagination: "Pagination",
                    finish: "Подписать и отправить",
                    next: "Дальше",
                    previous: "Назад",
                    cancel: "Отмена",
                    loading: "Загрузка ..."
                }
            }).validate({
                errorPlacement: function (error, element) {
                    element.before(error);
                    $("label").remove(".error");
                },
                rules: {
                    confirm: {
                        equalTo: "#password"
                    }
                }
            });

        }
    }
}
/**
 *
 * Pass all functions into module
 */

angular
    .module('app')
    .directive('uiWizardForm', uiWizardForm);