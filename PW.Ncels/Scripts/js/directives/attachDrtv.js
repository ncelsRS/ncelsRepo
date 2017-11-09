
function attachEdit(FileUploader, $http) {

    return {
        restrict: 'E',
        transclude: false,
        scope: {
            attachCodes: '='
        },
        templateUrl: '/Content/templates/attachEdit.html',
        link: function (scope, tElement, tAttrs) {
            scope.history = tAttrs.history === 'true';
            scope.originFileId = null;
            scope.$watch('attachCodes', function (newValue, oldValue) {
                if (newValue && scope.Items)
                    scope.Items.forEach(function (item, i, arr) {
                        if (item.Code) {
                            item.showAttach = newValue.indexOf(item.Code) !== -1;
                        } else {
                            item.showAttach = true;
                        }
                    });
            }, true);
            scope.loading = false;
            var type = tAttrs.type;
            var url = tAttrs.url;
            var path = tAttrs.path;
            var metadata = tAttrs.metadata === 'true';
            var isShowComment = tAttrs.showcomment === 'true';
            if (scope.history && !metadata) metadata = true;
            var filePostUrl = '/Upload/FilePost?code=';
            if (metadata) {
                filePostUrl = '/Upload/FilePost?saveMetadata=true&code=';
            }

            if (scope.queue == undefined) {
                scope.uploaders = {};
            }
            if (scope.AttachRemove == undefined) {
                scope.AttachRemove = function (item) {
                    $http({
                        url: '/Upload/FileDelete?' + item.Id,
                        method: 'POST'
                    }).success(function (response) {
                        if (!response.IsError) {
                            item.remove();
                            scope.uploaders = {};
                            initFileList();
                        } else {                            
                        }
                    });
                }
            }
            if (scope.AttachSign == undefined) {
                scope.AttachSign = function (item) {
                    if (item.ParentId)
                        item.url = item.url + '&originFileId=' + item.ParentId;
                    item.upload();
                }
            }
            if (scope.AttachVersion == undefined) {
                scope.AttachVersion = function (item, uploaderElId) {
                    scope.originFileId = item.MetadataId;
                    angular.element('#_' + uploaderElId).trigger('click');
                };
            }
            
            if (scope.acceptFileConfirm == undefined) {
                scope.acceptFileConfirm = function (item) {
                    $http({
                        url: '/Upload/AcceptFileConfirm?id=' + item.MetadataId,
                        method: 'POST'
                    }).success(function (response) {
                        if (response.Success) {
                            var originItem = item.uploader.queue.find(function (el) {
                                return el.MetadataId === item.MetadataId;
                            });
                            originItem.StatusCode = response.statusCode;
                            originItem.StatusName = response.statusName;
                        }
                    });
                };
            }
            if (scope.rejectDialogShow == undefined) {
                scope.rejectDialogShow = function (item, uploaderElId) {
                    $http({
                        url: '/Upload/GetCommentFileLink?id=' + item.MetadataId,
                        method: 'POST'
                    }).success(function (response) {
                        if (response.Success) {
                            $("#noteRejectFile").val(response.comment);
                            $("#idRejectFile").val(response.fileId);
                            $("#itemRejectFile").val(uploaderElId);
                            angular.element('#rejectFileModal').modal();
                        } 
                    });
                };
            }
            if (scope.rejectFileConfirm == undefined) {
                scope.rejectFileConfirm = function () {
                    var note = $("#noteRejectFile").val();
                    var fileId = $("#idRejectFile").val();
                    var itemId = $("#itemRejectFile").val();

                    $http({
                        url: '/Upload/RejectFileConfirm?id=' + fileId + '&note=' + note,
                        method: 'POST'
                    }).success(function (response) {
                        if (response.Success) {
                            var originItem = scope.uploaders[itemId].queue.find(function (el) {
                                return el.MetadataId === fileId;
                            });
                            originItem.StatusCode = response.statusCode;
                            originItem.StatusName = response.statusName;
                            angular.element('#rejectFileModal').modal('hide');
                        }
                    });
                };
            }
       
            initFileList();
            function initFileList() {
                scope.loading = true;
                $http({
                    method: 'GET',
                    url: url,
                    data: 'JSON',
                    params: { id: path, type: type, byMetadata: metadata, isShowComment: isShowComment }
                }).success(function (result) {
                    
                    scope.loading = false;
                    result.forEach(function (item, i, arr) {
                        if (scope.attachCodes && item.Code) {
                            item.showAttach = scope.attachCodes.indexOf(item.Code) !== -1;
                        } else {
                            item.showAttach = true;
                        }
                        scope.uploaders[item.Id] = new FileUploader({
                            url: filePostUrl + item.Id + '&path=' + path
                        });
                        scope.uploaders[item.Id].code = item.Code;
                        scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                            if (response.HistoryId) {
                                var originItem = fileItem.uploader.queue.find(function (el) {
                                    return el.MetadataId == response.MetadataId;
                                });
                                originItem.Id = response.HistoryPath;
                                originItem.OriginFileId = originItem.MetadataId;
                                originItem.MetadataId = response.HistoryId;
                            }
                            fileItem.Id = response.Id;
                            fileItem.MetadataId = response.MetadataId;
                            fileItem.OriginFileId = null;
                            fileItem.Version = response.Version;

                        };
                        scope.uploaders[item.Id].onAfterAddingFile = function (item) {
                            item.ParentId = scope.originFileId;
                            if (scope.originFileId) {
                                var originItem = item.uploader.queue.find(function (el) {
                                    return el.MetadataId == scope.originFileId;
                                });
                                originItem.OriginFileId = scope.originFileId;
                            }
                            scope.originFileId = null;
                        };
                        item.Items.forEach(function (file, i, arr) {
                            var dummy = new FileUploader.FileItem(scope.uploaders[item.Id], {
                                lastModifiedDate: file.sysCreatedDate,
                                size: file.AttachSize,
                                name: file.AttachName
                            });
                            dummy.Id = file.AttachId;
                            dummy.progress = 100;
                            dummy.isUploaded = true;
                            dummy.isSuccess = true;
                            dummy.singInfo = file.singInfo;
                            dummy.OriginFileId = file.OriginFileId;
                            dummy.MetadataId = file.MetadataId;
                            dummy.CreateDate = file.CreateDate;
                            dummy.OwnerName = file.OwnerName;
                            dummy.Version = file.Version;
                            dummy.IsSigned = file.IsSigned;
                            dummy.StatusCode = file.StatusCode;
                            dummy.StatusName = file.StatusName;
                            var isDesign = false;
                            if (file.StatusCode !== "") {
                                isDesign = true;
                            }
                            dummy.isDesign = isDesign;
                            scope.uploaders[item.Id].queue.push(dummy);
                        });
                    });

                    scope.Items = result;
                });
            }            
        }
    };

};

function attachRead($http) {
    return {
        restrict: 'E',

        transclude: false,
        scope: {},
        templateUrl: '/Content/templates/attachRead.html',
        replace: true,
        link: function (scope, tElement, tAttrs) {
            var type = tAttrs.type;
            var id = tAttrs.id;
            var url = tAttrs.url;
            scope.history = tAttrs.history === 'true';
            var metadata = tAttrs.metadata === 'true';
            var isShowComment = tAttrs.showcomment === 'true';
            if (scope.history && !metadata) metadata = true;
            if (url == undefined) {
                url = '/Upload/GetAttachList';
            }
            //scope.$watch(tAttrs.id, function (value) {
            $http({
                method: 'GET',
                url: url,
                data: 'JSON',
                params: { type: type, id: id, byMetadata: metadata, isShowComment: isShowComment }
            }).success(function (result) {
                scope.Items = result;
            });
            //});

        }
    };
}

function attachSimpleRead($http) {
    return {
        restrict: 'E',

        transclude: false,
        scope: {},
        templateUrl: '/Content/templates/attachSimpleRead.html',
        replace: true,
        link: function (scope, tElement, tAttrs) {

            var type = tAttrs.type;
            var id = tAttrs.id;
            var url = tAttrs.url;
            if (url == undefined) {
                url = '/Upload/GetAttachList';
            }
            scope.$watch(tAttrs.id, function (value) {
                $http({
                    method: 'GET',
                    url: url,
                    data: 'JSON',
                    params: { type: type, id: value }
                }).success(function (result) {
                    scope.Items = result;
                });
            });

        }
    };
}

function attachEditSing(FileUploader, $http) {
    return {
        restrict: 'E',
        transclude: false,
        scope: {},
        templateUrl: '/Content/templates/attachEditSign.html',
        link: function (scope, tElement, tAttrs) {

            var type = tAttrs.type;
            var id = tAttrs.id;
            var url = tAttrs.url;
            var path = tAttrs.path;
            if (scope.uploaders == undefined) {
                scope.uploaders = {};
            }
            if (scope.AttachRemove == undefined) {
                scope.AttachRemove = function (item) {
                    $http({
                        url: '/Upload/FileDelete',
                        method: 'POST',
                        data: JSON.stringify({ Id: item.Id })
                    }).success(function (response) {
                        if (!response.IsError) {
                            item.remove();
                        } else {

                        }
                    });
                }
            }
            if (scope.AttachSign == undefined) {
                scope.AttachSign = function (item) {
                    superSignByPrismWorks(item._file);

                    // item.url = item.url + '&md5=' + document.getElementById('signXml').value + '&info=' + document.getElementById('signInfo').value;
                    // document.getElementById('signXml').value = '';
                    // document.getElementById('signInfo').value = '';

                    $http({
                        url: '/Upload/FileSing',
                        method: 'POST',
                        //  data: JSON.stringify({ XmlSing: '<?xml version="1.0" encoding="UTF-8" standalone="no"?><md5>deb925653fbe803dd944ef7f737eb77d<ds:Signature xmlns:ds="http://www.w3.org/2000/09/xmldsig#"> <ds:SignedInfo><ds:CanonicalizationMethod Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315"/><ds:SignatureMethod Algorithm="http://www.w3.org/2001/04/xmldsig-more#rsa-sha1"/><ds:Reference URI=""><ds:Transforms><ds:Transform Algorithm="http://www.w3.org/2000/09/xmldsig#enveloped-signature"/><ds:Transform Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments"/></ds:Transforms><ds:DigestMethod Algorithm="http://www.w3.org/2001/04/xmldsig-more#sha1"/><ds:DigestValue>Nz75LgOKJdEPFWusWgVUEOWT2NM=</ds:DigestValue></ds:Reference></ds:SignedInfo><ds:SignatureValue>lHwnKS81Sd4L1sBhI1t+blN+FrJfehafV048w2kFfhpx8uPrNXCfVrywFUIwEpp2WzBtCX4LAerReYP0GCIsGA9TonrK5vkWlODk4SIKBsddpFV6jzgTy5yt5cU+HZSqX/3rmTqMdvJ/MomzsQVXzbK0qDq2+dXaW7Rk0Ivzwkg9dZQ/80L1a5ZoYjEaSPs5HyWPbyAjo2b/pKRmj9HVGK64OVdh/U1GzjCNSiSuIWDGFOasORFkIl6CfpYTwWUC3mpQru9njwofhUC1O/d82BegNBeezTBB0vCCCgBhViUuuIs14WzLjMm9xkpcJ05bkyk1VHdHBA20JTNBhlnPMg==</ds:SignatureValue><ds:KeyInfo><ds:X509Data><ds:X509Certificate>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</ds:X509Certificate></ds:X509Data></ds:KeyInfo></ds:Signature></md5>', InfoSing: 'АСТАНА ДЕЙСТВУЮЩИЙ ФИЗИЧЕСКИ ДЕЙСТВУЮЩИЙ IIN745896125463 KZ АСТАНА null null null' })
                        data: JSON.stringify({ XmlSing: document.getElementById('signXml').value, InfoSing: document.getElementById('signInfo').value })
                    }).success(function (response) {
                        item.upload();
                    });
                }
            }
            if (id == undefined) {
                $http({
                    method: 'GET',
                    url: '/Dictionaries/GetReference',
                    data: 'JSON',
                    params: { type: type }
                }).success(function (result) {

                    result.forEach(function (item, i, arr) {
                        scope.uploaders[item.Id] = new FileUploader({
                            url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
                        });
                        scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                            fileItem.Id = response.Id;
                        };
                        scope.uploaders[item.Id].onBeforeUploadItem = function (item) {

                            //item.headers = { "XmlSing": document.getElementById('signXml').value, "InfoSing": document.getElementById('signInfo').value };
                            // item.formData.set("XmlSing", '444444');// document.getElementById('signXml').value);
                            // item.formData.set("InfoSing", '777777');// document.getElementById('signInfo').value);
                            console.info('onBeforeUploadItem', item);
                        };
                    });
                    scope.Items = result;
                });
            } else {
                $http({
                    method: 'GET',
                    url: url,
                    data: 'JSON',
                    params: { id: id, type: type }
                }).success(function (result) {
                    result.forEach(function (item, i, arr) {
                        scope.uploaders[item.Id] = new FileUploader({
                            url: '/Upload/FilePost?code=' + item.Id + '&path=' + path
                        });
                        scope.uploaders[item.Id].onSuccessItem = function (fileItem, response, status, headers) {
                            fileItem.Id = response.Id;

                        };
                        debugger;
                        scope.uploaders[item.Id].onBeforeUploadItem = function (item) {

                            //item.headers = { "XmlSing": document.getElementById('signXml').value, "InfoSing": document.getElementById('signInfo').value };

                            // item.formData.set("XmlSing", '444444');// document.getElementById('signXml').value);
                            // item.formData.set("InfoSing", '777777');// document.getElementById('signInfo').value);
                            console.info('onBeforeUploadItem', item);
                        };
                        item.Items.forEach(function (file, i, arr) {
                            var dummy = new FileUploader.FileItem(scope.uploaders[item.Id], {
                                lastModifiedDate: file.sysCreatedDate,
                                size: file.AttachSize,
                                name: file.AttachName


                            });
                            dummy.Id = file.AttachId;
                            dummy.progress = 100;
                            dummy.isUploaded = true;
                            dummy.isSuccess = true;
                            dummy.singInfo = file.singInfo;

                            scope.uploaders[item.Id].queue.push(dummy);
                        });
                    });

                    scope.Items = result;
                });
            }
        }
    };
}
function rejectModalDialog($http) {
    return {
        restrict: 'E',
        transclude: false,
        templateUrl: '/Content/templates/rejectModalDIalog.html',
        replace: true,
        scope: {
            title: '=modalTitle',
            header: '=modalHeader',
            body: '=modalBody',
            footer: '=modalFooter',
            callbackbuttonleft: '&ngClickLeftButton',
            callbackbuttonright: '&ngClickRightButton',
            handler: '=lolo'
        },
        controller: function ($scope) {
            $scope.handler = 'pop';
        },
    };
}
angular
    .module('app')
    .directive('attachRead', attachRead)
    .directive('attachSimpleRead', attachSimpleRead)
    .directive('attachEdit',attachEdit)
//    .directive('rejectModalDialog', rejectModalDialog)
    .directive('attachEditSing', attachEditSing);