/**
 * INSPINIA - Responsive Admin Theme
 * 2.5
 *
 * Custom scripts
 */

$(document).ready(function () {

    // Append config box / Only for demo purpose
    //$.get("views/skin-config.html", function (data) {
    //    $('body').append(data);
    //});

    // Full height of sidebar
    function fix_height() {
        var heightWithoutNavbar = $("body > #wrapper").height() - 61;
        $(".sidebard-panel").css("min-height", heightWithoutNavbar + "px");

        var navbarHeigh = $('nav.navbar-default').height();
        var wrapperHeigh = $('#page-wrapper').height();

        if(navbarHeigh > wrapperHeigh){
            $('#page-wrapper').css("min-height", navbarHeigh + "px");
        }

        if(navbarHeigh < wrapperHeigh){
            $('#page-wrapper').css("min-height", $(window).height()  + "px");
        }

        if ($('body').hasClass('fixed-nav')) {
            if (navbarHeigh > wrapperHeigh) {
                $('#page-wrapper').css("min-height", navbarHeigh - 60 + "px");
            } else {
                $('#page-wrapper').css("min-height", $(window).height() - 60 + "px");
            }
        }

    }


    $(window).bind("load resize scroll", function() {
        if(!$("body").hasClass('body-small')) {
            fix_height();
        }
    });

    // Move right sidebar top after scroll
    $(window).scroll(function(){
        if ($(window).scrollTop() > 0 && !$('body').hasClass('fixed-nav') ) {
            $('#right-sidebar').addClass('sidebar-top');
        } else {
            $('#right-sidebar').removeClass('sidebar-top');
        }
    });


    setTimeout(function(){
        fix_height();
    })
});

// Minimalize menu when screen is less than 768px
$(function() {
    $(window).bind("load resize", function() {
        if ($(document).width() < 769) {
            $('body').addClass('body-small')
        } else {
            $('body').removeClass('body-small')
        }
    })
})



function dateformatHtml(data, type, full, meta) {
    var date = new Date(parseInt(data.slice(6, -2)));
    var month = date.getMonth() + 1;
    return date.getDate() + "." + (month.length > 1 ? month : "0" + month) + "." + date.getFullYear();

}

function getExpertUnit($scope, $http, name, code) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetExpertUnit',
        data: 'JSON',
        params: { code: code }
    }).success(function (result) {
        $scope[name] = result;
    });
}

function loadAttach(FileUploader, $scope, $http, id, name, path) {
    $http({
        method: 'GET',
        url: '/Upload/GetAttachListAll',
        data: 'JSON',
        params: { id: id, type: name }
    }).success(function (result) {
        result.forEach(function (item, i, arr) {
            $scope.uploaders[item.Id] = new FileUploader({
                url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
            });
            $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                fileItem.Id = response.Id;

            };
            item.Items.forEach(function (file, i, arr) {
                var dummy = new FileUploader.FileItem($scope.uploaders[item.Id], {
                    lastModifiedDate: file.sysCreatedDate,
                    size: file.AttachSize,
                    name: file.AttachName


                });
                dummy.Id = file.AttachId;
                dummy.progress = 100;
                dummy.isUploaded = true;
                dummy.isSuccess = true;

                $scope.uploaders[item.Id].queue.push(dummy);
            });
        });

        $scope[name] = result;
    });
}
function loadAttachByUrl(FileUploader, $scope, $http, id, name, path, url) {
    $http({
        method: 'GET',
        url: url,
        data: 'JSON',
        params: { id: id, type: name }
    }).success(function (result) {
        result.forEach(function (item, i, arr) {
            $scope.uploaders[item.Id] = new FileUploader({
                url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
            });
            $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                fileItem.Id = response.Id;

            };
            item.Items.forEach(function (file, i, arr) {
                var dummy = new FileUploader.FileItem($scope.uploaders[item.Id], {
                    lastModifiedDate: file.sysCreatedDate,
                    size: file.AttachSize,
                    name: file.AttachName


                });
                dummy.Id = file.AttachId;
                dummy.progress = 100;
                dummy.isUploaded = true;
                dummy.isSuccess = true;

                $scope.uploaders[item.Id].queue.push(dummy);
            });
        });

        $scope[name] = result;
    });
}
function loadAttachDic(FileUploader, $scope, $http, name, path) {
    $http({
        method: 'GET',
        url: '/Dictionaries/GetReference',
        data: 'JSON',
        params: { type: name }
    }).success(function (result) {
        if ($scope.uploaders == undefined) {
            $scope.uploaders = {};
        }
        result.forEach(function (item, i, arr) {
            $scope.uploaders[item.Id] = new FileUploader({
                url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
            });
            $scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                fileItem.Id = response.Id;
            };

        });
        $scope[name] = result;
    });
}

function loadEnums($scope, name, $http) {
    $http({
        method: 'GET',
        url: '/Enums/GetReference',
        data: 'JSON',
        params: { type: name }
    }).success(function (result) {
        $scope[name] = result;
    });
}