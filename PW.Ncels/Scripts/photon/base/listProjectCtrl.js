function showDrugPriceDialog() {
    $("#priceCountyDiv").modal();
}

function setPriceIsIncluded(control) {
    var fieldValue = $(control).prop('checked');
    var priceId = $(control).attr("price-id");
    var projectId = $(control).attr("projectId");
    var countryId = $(control).attr("countryId");
    var params = JSON.stringify({ 'priceId': priceId, 'isCheck': fieldValue, 'projectId': projectId, 'countryId': countryId });

    $.ajax({
        type: "POST",
        url: '/Price/SetIsIncluded',
        data: params,
        dataType: 'json',
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        },
        error: function () {
            alert("Connection Failed. Please Try Again");
        }
    });
}

function customAlert(message, size) {
    if (size == null)
        size = 'sm';
    showAlert(message, null, null, size);
}
function customWarning(message, size) {
    if (size == null)
        size = 'sm';
    showAlert(message, "Предупреждение", "warning", size);
}

function loadDictionary($scope, name, $http) {
    $http({
        method: "GET",
        url: "/Dictionaries/GetReference",
        data: "JSON",
        params: { type: name }
    }).success(function (result) {
        $scope[name] = result;
    });
}

function loadDictionaryParts($scope, $http) {
    var registerId = $scope.object.Project.RegisterId;
    if (registerId != null) {
        $http({
            method: "GET",
            url: "/Dictionaries/GetParts",
            data: "JSON",
            params: { id: registerId }
        }).success(function (result) {
            $scope.Parts = result;
        });
    }
}

function loadDictionaryByOrderYear($scope, name, $http) {
    $http({
        method: "GET",
        url: "/Dictionaries/GetDicByOrderYear",
        data: "JSON",
        params: { type: name }
    }).success(function (result) {
        $scope[name] = result;
    });
}

function loadDictionaryRePrice($scope, name, $http, priceProjectId) {
    $http({
        method: "GET",
        url: "/Dictionaries/GetRePriceReoson",
        data: "JSON",
        params: { priceProjectId: priceProjectId }
    }).success(function(result) {
        $scope[name] = result;
    });
}

function IsLoadedDoc($http, $scope, $uibModal) {
    $http({
        method: "GET",
        url: "/Upload/CheckAttachList",
        data: "JSON",
        params: { id: $scope.object.Project.Id, type: "sysAttachPriceDict" }
    }).success(function(result) {
        if (result == "True") {
            var modalInstance = $uibModal.open({
                templateUrl: "/Project/Agreement",
                controller: ModalSendPrice,
                scope: $scope
            });
        } else {
            customWarning("Требуются вложения:<br/> «Доверенность или копия» и «Сопроводительная письмо» ");
        }
    });
}

function ModalSendPrice($scope, $http, $uibModalInstance) {
    var type = $scope.object.Project.Type;
    var url = "";
    if (type == 2 || type == 3) {
        url = "/Project/SendProjectPriceParent";
    }
    //if (type == 4) {
    //    window.location.href = "/Project/CreateRejectDocument";
    //    return;
    //}
    else {
        url = "/Price/SendProjectPrice";
    }
    $scope.sendProject = function() {
        $http({
            url: url,
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function(response) {
            if (type == 0) {
                window.location.href = "/Project/PriceLsDetails/" + $scope.object.Project.Id;
            } else if (type == 1) {
                window.location.href = "/Project/PriceImnDetails/" + $scope.object.Project.Id;
            } else if (type == 2) {
                window.location.href = "/Project/RePriceLsDetails/" + $scope.object.Project.Id;
            } 
            else {
                window.location.href = "/Project/RePriceImnDetails/" + $scope.object.Project.Id;
            }
        });
    };

    $scope.cancelSend = function() {
        $uibModalInstance.dismiss("cancel");
    };
}

function priceIsIncluded(data, type, full, meta) {
    var tag = '<input type="checkbox"  style="margin-right:10px;" onchange="setPriceIsIncluded(this)" price-id="' + full.Id + '" projectId="' + full.PriceProjectId + '" countryId="' + full.CountryId + '"';
  
    if (full.IsIncluded === true) {
        tag += ' checked ' ;
    }
    tag += '>';
   // tag += '<a class="glyphicon glyphicon-trash" onclick="removeRecord(this)" style="margin-left:5px;margin-right:5px;"></a>';
   // tag += '<a class="glyphicon glyphicon-edit" onclick="removeRecord(this)" style="margin-left:5px;margin-right:10px;"></a>';
    return tag;
}

function dateformatHtml(data, type, full, meta) {
    
    if (data == null)
        return "";
    var date = new Date(parseInt(data.slice(6, -2)));
    var month = date.getMonth() + 1;
    var dd = date.getDate();
    val = (dd.toString().length > 1 ? dd : "0" + dd) + "." + (month.toString().length > 1 ? month : "0" + month) + "." + date.getFullYear();
    return val;

}

function actionsPriceLsHtmlAction(data, type, full, meta) {
    if (data == null) {
        data = "б/н";
    }
    if (full.IsRegisterProject == true) {
        if (full.Type == 0) {
            return '<a  class="pw-task-link" href="/Project/RegisterLsDetails?id=' +
                full.Id +
                '" >' +
                data +
                "</a>";
        }
        if (full.Type == 1) {
            return '<a  class="pw-task-link" href="/Project/ReRegisterLsDetails?id=' +
                full.Id +
                '" >' +
                data +
                "</a>";
        }
        if (full.Type == 2) {
            return '<a  class="pw-task-link" href="/Project/ChRegisterLsDetails?id=' +
                full.Id +
                '" >' +
                data +
                "</a>";
        }
    } else {
        if (full.Type == 0) {
            return '<a  class="pw-task-link" href="/Project/PriceLsDetails?id=' +
                full.Id +
                '" >' +
                data +
                "</a>";
        }
        if (full.Type == 1) {
            return '<a  class="pw-task-link" href="/Project/PriceImnDetails?id=' +
                full.Id +
                '" >' +
                data +
                "</a>";
        }
        if (full.Type == 2) {
            return '<a  class="pw-task-link" href="/Project/RePriceLsDetails?id=' +
                full.Id +
                '" >' +
                data +
                "</a>";
        }
        if (full.Type == 3) {
            return '<a  class="pw-task-link" href="/Project/RePriceImnDetails?id=' +
                full.Id +
                '" >' +
                data +
                "</a>";
        }
    }
}

function actionsPriceRework(data, type, full, meta) {
    if (data == null) {
        data = "б/н";
    }
    if (full.Type == 0) {
        return '<a  class="pw-task-link" href="/Project/RePriceLs?id=' +
            null +
            "&parentId=" +
            full.Id +
            '" >' +
            data +
            "</a>";
    }
    if (full.Type == 1) {
        return '<a  class="pw-task-link" href="/Project/RePriceImn?id=' +
            null +
            "&parentId=" +
            full.Id +
            '" >' +
            data +
            "</a>";
    }
}

function projectBaseCtrl($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        // DTColumnBuilder.newColumn("Number", "Номер").withOption('name', 'Number').renderWith(actionsHtmlAction),
        // DTColumnBuilder.newColumn("ApplicationDate", "Дата проекта").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("NameRu", "Наименование объекта").withOption("name", "NameRu"),
        DTColumnBuilder.newColumn("ProjectStateName", "Текущий статус").withOption("name", "ProjectStateName"),
        DTColumnBuilder.newColumn("SignEmployeeName", "Исполнитель").withOption("name", "SignEmployeeName")
    ];
}

function commonInit() {
    $(".phone").on("keypress keyup blur", function (event) {
        switch (event.keyCode) {
            case 43:  // + character
            case 40: // the ( character
            case 41: // the ) character
                break;
            default:
                var regex = new RegExp("^[0-9- ]");
                var key = event.key;
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
                break;
        }
    });

    var number;
    var inputs = document.getElementsByTagName('input');

    for (var i = 0; i < inputs.length; i++) {
        number = inputs[i];
        if (inputs[i].type.toLowerCase() == "number") {
            number.onkeydown = function (e) {
                return numberKeyDown(e);
            };
        }
    }

    $(".price").on("keydown", function (e) {
        return numberKeyDown(e);
    });
}

function numberKeyDown(e) {
    // Allow: backspace, delete, tab, escape, enter
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 ||
      // comma
      (e.key == ',') ||
        // Allow: Ctrl+A,Ctrl+C,Ctrl+V, Command+A
      ((e.keyCode == 65 || e.keyCode == 86 || e.keyCode == 67) && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
      (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}

function _highlightTabs(item, index) {
    var elements = $("input[name='" + item.$name + "']");
    if (elements.length < 1) {
        elements = $("select[name='" + item.$name + "']");
        if (elements.length < 1) {
            elements = $("div[name='" + item.$name + "']");
            if (elements.length < 1) {
                return;
            }
        }
    }
    var el = elements[0];
    var tabs = $(".tab-pane:has('#" + el.id + "')");
    if (tabs.length > 0) {
        var tab = tabs[0];
        $("." + tab.id).addClass("tab-valid-error");
    }
}

function _highlightAttach() {
    $(".f-tab-attach").removeClass("tab-valid-error");
    var attachValid = checkAttachFile();
    if (!attachValid) {
        $(".f-tab-attach").addClass("tab-valid-error");
    }
}

function highlightNotValidTabs(form) {
    $(".f-tab").removeClass("tab-valid-error");
    angular.forEach(form.$error.required, function (item, index) {
        _highlightTabs(item, index);
    });
    angular.forEach(form.$error.email, function (item, index) {
        _highlightTabs(item, index);
    });
    angular.forEach(form.$error.pattern, function (item, index) {
        _highlightTabs(item, index);
    });

    _highlightAttach();
}

var emailFormat = "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$";
var decimalRegex = '^[0-9]{1,10}([\\,]?[0-9]+)?$';

function isWindowOpened(windowId) {
    return $('#' + windowId).css('display') != 'none';
};
function wait(ms) {
    var start = new Date().getTime();
    var end = start;
    while (end < start + ms) {
        end = new Date().getTime();
    }
}

function priceLsGrid($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.isProjectSaved = false;
    $scope.isShowFindTab = true;
    loadDictionary($scope, "Country", $http, true);
    //loadDictionaryByOrderYear($scope, 'Currency', $http);
    loadDictionary($scope, "IntroducingMethod", $http);
    loadDictionaryCurrency($scope, $http);
    loadDictionary($scope, "LsType", $http);
    loadDictionary($scope, "RefPriceType", $http);
    loadDictionary($scope, "Reason", $http);

    function actionsPriceHtmlAction(data, type, full, meta) {
        if (full.Status === 0) {
            return '<span class="glyphicon glyphicon-trash" title="Удалить" onclick="removePrice(this)" modelId="' + full.Id + '"></span>';
        }
        return "";
    }

    $(document).ready(function () {
        commonInit();
        $('#priceCountyDiv').on('hidden.bs.modal', function () {
            $scope.priceWindowOpened = false;
            $scope.$digest();
        });
        $('#priceCountyDiv').on('shown.bs.modal', function () {
            $scope.priceWindowOpened = true;
        });
    });

    $scope.emailFormat = "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$";
    $scope.decimalRegex = decimalRegex;

    $scope.cbNoDataChange = function (cbVal) {
        if (cbVal) {
            return "-";
        } else {
            return "";
        }
    };
    $scope.GetCountries = function (search) {
        var c = $scope.Country;
        if (search && c.indexOf(search) === -1) {
            c.unshift(search);
        }
        return c;
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("Type", "Тип").withOption("name", "Number").notVisible(),
        DTColumnBuilder.newColumn("CountryName", "Страна"),
        DTColumnBuilder.newColumn("ManufacturerPriceWithLink", "Зарегистрированная цена производителя (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("LimitPriceWithLink", "Предельная цена (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("AvgOptPriceWithLink", "Средняя оптовая цена (за последний квартал) Указать ссылку на источник информации"),
        DTColumnBuilder.newColumn("AvgRozPriceWithLink", "Средняя розничная цена (за последний квартал) Указать ссылку на источник информации"),
        DTColumnBuilder.newColumn("IsIncluded", "Поставляет").renderWith(priceIsIncluded)
    ];

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения").withClass("dtc_reg_number"),
        DTColumnBuilder.newColumn("reg_date", "Дата регистрационного удостоверения").renderWith(dateformatHtml)
        .withClass("dtc_reg_date"),
        DTColumnBuilder.newColumn("C_int_name", "МНН").withClass("dtc_int_name"),
        DTColumnBuilder.newColumn("SubstanceName", "Состав").withClass("dtc_SubstanceName"),
        DTColumnBuilder.newColumn("name", "Торговое название").withClass("dtc_name"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма").withClass("dtc_dosage_form_name"),
        DTColumnBuilder.newColumn("concentration", "Концентрация").withClass("dtc_concentration"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка (мг)").withClass("dtc_dosage_value"),
        DTColumnBuilder.newColumn("um", "Способ введения").withClass("dtc_um"),
        DTColumnBuilder.newColumn("box_name1", "Первичная упаковка").withClass("dtc_box_name1"),
        DTColumnBuilder.newColumn("box_name2", "Вторичная упаковка").withClass("dtc_box_name2"),
        DTColumnBuilder.newColumn("box_count", "Количество во вторичной упаковке").withClass("dtc_box_count"),
        DTColumnBuilder.newColumn("volume", "Объем").withClass("dtc_volume"),
        DTColumnBuilder.newColumn("expiration_date", "Дата истечение рег.").renderWith(dateformatHtml)
        .withClass("dtc_expiration_date"),
        DTColumnBuilder.newColumn("C_producer_name", "Производитель").withClass("dtc_producer_name"),
        DTColumnBuilder.newColumn("C_country_name", "Страна").withClass("dtc_country_name"),
        DTColumnBuilder.newColumn("description", "Признак ЛС").withClass("dtc_description"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код").withClass("dtc_atc_code")
    ];

    $scope.dtColumns10Full = angular.copy($scope.dtColumns10);
    $scope.dtColumns10Full = $scope.dtColumns10Full.map(function (obj) {
        var newObj = obj;
        newObj["columnVisible"] = true;
        return newObj;
    });

    Array.prototype.getIndexOfObject = function (prop, value) {
        for (var i = 0; i < this.length ; i++) {
            if (this[i][prop] === value) {
                return i;
            }
        }
    }

    $scope.cbChange = function (val, cl) {
        if (val) {
            var insertedInd = $scope.dtColumns10Full.getIndexOfObject("sClass", cl);
            $scope.dtColumns10.splice(insertedInd, 0, $scope.dtColumns10Full[insertedInd]);
            return true;
        } else {
            if ($scope.dtColumns10.length > 1) {
                var ind = $scope.dtColumns10.getIndexOfObject("sClass", cl);
                $scope.dtColumns10.splice(ind, 1);
                return false;
            } else {
                return true;
            }
        }
    };
    $scope.dtColumns11 = [
        DTColumnBuilder.newColumn("Name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("DiseaseOfICD",
            "Заболевание группа по международной классификаций болезней 10 го пересмотра (далее – МКБ-10)*"),
        DTColumnBuilder.newColumn("SysnonimAndRareDesease", "Синонимы и названия редких болезней"),
        DTColumnBuilder.newColumn("CodeICD", "Код по МКБ-10")
    ];

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption("name", "Number"),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption("name", "Number").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption("name", "Number"),
    ];

    var id = $("#projectId").val();
    $scope.object = {
        project: {},
        HolderOrganization: {},
        ProxyOrganization: {},
        CurentPrice: {},
        Price: {}
    };

    var dateNow = new Date();
    //dateNow.setMonth(dateNow.getMonth() + 6);
    dateNow.setDate(dateNow.getDate() + 1);
    $scope.testDate = new Date();
    //DatePicker
    $scope.datePicker = {
        dateStartStatus: { opened: false },
        dateStartOpen: function ($event) {
            $scope.datePicker.dateStartStatus.opened = true;
        },
        dateStartOptions: {
            // maxDate: new Date()
        },
        dateEndStatus: {
            opened: false
        },
        dateEndOpen: function ($event) {
            $scope.datePicker.dateEndStatus.opened = true;
        },
        dateEndOptions: {
            minDate: dateNow
        }
    };

    $scope.unitPriceCalc = function () {
        var OwnerPrice = $scope.object.CurentPrice.OwnerPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (OwnerPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.UnitPrice = $scope.object.CurentPrice.OwnerPrice /
                $scope.object.Project.CountPackage;
            $scope.object.Price.UnitPrice = $scope.object.CurentPrice.UnitPrice;
        }
    };
    $scope.ownerPriceCalc = function () {
        var UnitPrice = $scope.object.CurentPrice.UnitPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (UnitPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.OwnerPrice = $scope.object.CurentPrice.UnitPrice *
                $scope.object.Project.CountPackage;
            $scope.object.Price.OwnerPrice = $scope.object.CurentPrice.OwnerPrice;
        }
    };
    $scope.unitBritishPriceCalc = function () {
        var BritishPrice = $scope.object.CurentPrice.BritishPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (BritishPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.BritishCost = $scope.object.CurentPrice.BritishPrice /
                $scope.object.Project.CountPackage;
        }
    };
    $scope.ownerBritishPriceCalc = function () {
        var BritishCost = $scope.object.CurentPrice.BritishCost;
        var CountPackage = $scope.object.Project.CountPackage;
        if (BritishCost != null && CountPackage != null) {
            $scope.object.CurentPrice.BritishPrice = $scope.object.CurentPrice.BritishCost *
                $scope.object.Project.CountPackage;
        }
    };
    $scope.BoolDic = [
        {
            Id: true,
            Name: "Да"
        }, {
            Id: false,
            Name: "Нет"
        }
    ];

    $scope.sendProject = function () {
        if ($scope.priceLsForm.$valid) {
            $http({
                url: "/Price/SendProjectPrice",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                customAlert("Ок");
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Upload/FileView?id=" + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.signPrice = function (id) {
        highlightNotValidTabs($scope.priceLsForm);
        if ($scope.priceLsForm.$valid) {
            $http({
                method: "GET",
                url: "/Upload/CheckAttachList",
                data: "JSON",
                params: { id: $scope.object.Project.Id, type: "sysAttachPriceDict" }
            }).success(function (result) {
                if (result == "True") {
                    $http({
                        url: "/Project/PriceLsSave",
                        method: "POST",
                        data: JSON.stringify($scope.object)
                    }).success(function (response) {
                        startSign('/Price/SignOperation', id);
                    });
                } else {
                    customWarning("Требуются вложения:<br/> «Доверенность или копия» и «Сопроводительная письмо» ");
                }
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.sendAgree = function () {
        highlightNotValidTabs($scope.priceLsForm);
        if ($scope.priceLsForm.$valid) {
            $http({
                url: "/Project/PriceLsSave",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                IsLoadedDoc($http, $scope, $uibModal);
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };


    $scope.editProject = function (withAlert) {
        $http({
            url: "/Project/PriceLsSave",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            if (withAlert) {
                customAlert("Данные сохранены");
            }
            $scope.isEnableDownload = true;
        });
    };
    $scope.sendProject = function () {
        $http({
            url: "/Price/SendProjectPrice",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            customAlert("Ок");
        });
    };
    $scope.selectGridPrice = function (data) {
        console.log(data);
        $scope.object.Price = data;
    };
    $scope.SetObjectReg = function (isOrphan) {
        if (isOrphan) {
            cleanObject($scope);
            $scope.object.Project.NameRu = $scope.curentReg.Name;
            $scope.object.Project.Id = id;
            $scope.editProject();
        } else {
            $scope.object.CurentPrice.OwnerPrice = '';
            $scope.object.CurentPrice.UnitPrice = '';
            $scope.object.CurentPrice.RefPrice = '';
            $scope.object.ManufacturerOrganization.NameKz = $scope.curentReg.C_producer_name_kz;
            $scope.object.ManufacturerOrganization.NameRu = $scope.curentReg.C_producer_name;
            $scope.object.ManufacturerOrganization.NameEn = $scope.curentReg.C_producer_name_en;

            $scope.object.HolderOrganization.NameKz = $scope.curentReg.owner_name_kz;
            $scope.object.HolderOrganization.NameRu = $scope.curentReg.owner_name_ru;
            $scope.object.HolderOrganization.NameEn = $scope.curentReg.owner_name_en;
            $scope.hoNameKz = $scope.object.HolderOrganization.NameKz;
            $scope.hoNameRu = $scope.object.HolderOrganization.NameRu;
            $scope.hoNameEn = $scope.object.HolderOrganization.NameEn;

            $scope.object.Project.NameKz = $scope.curentReg.name_kz;
            $scope.object.Project.NameRu = $scope.curentReg.name;
            $scope.object.Project.RegNumber = $scope.curentReg.reg_number;
            // для даты регистрационного уд.
            $scope.object.Project.RegDate = new Date($scope.curentReg.reg_date.match(/\d+/)[0] * 1);
            $scope.object.Project.MnnRu = $scope.curentReg.C_int_name;
            $scope.object.Project.FormNameRu = $scope.curentReg.C_dosage_form_name;
            $scope.object.Project.FormNameKz = $scope.curentReg.C_dosage_form_name_kz;
            $scope.object.Project.Dosage = $scope.curentReg.dosage_value;
            $scope.object.Project.CountPackage = $scope.curentReg.box_count;
            $scope.object.Project.Concentration = $scope.curentReg.concentration;
            $scope.object.Project.CodeAtx = $scope.curentReg.C_atc_code;
            $scope.object.Project.Volume = $scope.curentReg.volume;
            $scope.object.ManufacturerOrganization.CountryDicId = $scope.curentReg.C_country_Id;
            $scope.object.HolderOrganization.CountryDicId = $scope.curentReg.owner_country_id;
            //$scope.object.ProxyOrganization.CountryDicId = $scope.curentReg.C_country_Id;
            $scope.object.Project.IntroducingMethodDicId = $scope.curentReg.umId;

            $scope.object.Project.RegisterId = $scope.curentReg.id;
            $scope.object.Project.RegisterDfId = $scope.curentReg.df_id;

            $scope.object.Project.Id = id;


            if ($scope.object.CurentPrice.ManufacturerPrice == "0") {
                $scope.object.CurentPrice.ManufacturerPrice = "";
            }
            if ($scope.object.CurentPrice.CipPrice == "0") {
                $scope.object.CurentPrice.CipPrice = "";
            }
            if ($scope.object.CurentPrice.RefPrice == "0") {
                $scope.object.CurentPrice.RefPrice = "";
            }
            if ($scope.object.CurentPrice.OwnerPrice == "0") {
                $scope.object.CurentPrice.OwnerPrice = "";
            }
            if ($scope.object.CurentPrice.UnitPrice == "0") {
                $scope.object.CurentPrice.UnitPrice = "";
            }
            if ($scope.object.CurentPrice.BritishPrice == "0") {
                $scope.object.CurentPrice.BritishPrice = "";
            }
            if ($scope.object.CurentPrice.BritishCost == "0") {
                $scope.object.CurentPrice.BritishCost = "";
            }
            $scope.object.CurentPrice.OwnerPriceCurrencyDicId = $scope.Currency[0].Id;
            $scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;

            $scope.editProject(false);
        }
        $scope.isProjectSaved = true;
        customAlert("Данные заполнены");
    };
    $scope.curentReg = {};

    $scope.selectGridIntegration = function (data) {
        console.log(data);
        $scope.curentReg = data;

    };
    $scope.editPrice = function () {
        $("#priceCountyDiv").modal();

    };
    $scope.deletePrice = function () {
        var success = function () {
            $http({
                url: "/Price/PriceDelete",
                method: "POST",
                data: JSON.stringify($scope.object.Price)
            }).success(function (response) {
                $scope.reloadGridPrice();
                $scope.addPrice();
            });
        }
        var cancel = function () {
        };
        showConfirmation("Подтверждение", "Вы уверены что хотите очистить данные?", success, cancel);
      

    };
    $scope.createPrice = function () {
        $scope.object.Price = angular.copy($scope.defaultPrice);
        $("#priceCountyDiv").modal();
    };
    $scope.addPrice = function () {
        $scope.object.Price = angular.copy($scope.defaultPrice);
     //   $("#priceCountyDiv").modal();
    };
    $scope.savePrice = function () {
        var isValid = true;
        if (!$scope.object.Price.IsManufacturerPrice) {
            isValid = $scope.object.Price.ManufacturerPrice > 0 &&
                $scope.object.Price.ManufacturerPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.ManufacturerPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsLimitPrice) {
            isValid = isValid &&
                $scope.object.Price.LimitPrice > 0 &&
                $scope.object.Price.LimitPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.LimitPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsAvgOptPrice) {
            isValid = isValid &&
                $scope.object.Price.AvgOptPrice > 0 &&
                $scope.object.Price.AvgOptPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.AvgOptPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsAvgRozPrice) {
            isValid = isValid &&
                $scope.object.Price.AvgRozPrice > 0 &&
                $scope.object.Price.AvgRozPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.AvgRozPriceNote.length > 0;
        }
        //        highlightNotValidTabs($scope.priceImnForm);
        
        if (isValid) {
        $http({
            url: "/Price/PriceSave",
            method: "POST",
            data: JSON.stringify($scope.object.Price)
        }).success(function (response) {
            $scope.addPrice();
            $scope.reloadGridPrice();
            $("#priceCountyDiv").modal('hide');
        });
        } else {
           // customWarning("Заполните поля!");
        }
    };
    $http({
        method: "GET",
        url: "/Project/LoadPriceLs?id=" + id,
        data: "JSON"
    }).success(function (response) {
        response.Project.DoverennostCreatedDate = new Date(response.StartDateYear,
            response.StartDateMonth - 1,
            response.StartDateDay);
        response.Project.DoverennostExpiryDate = new Date(response.EndDateYear,
            response.EndDateMonth - 1,
            response.EndDateDay);

        // для даты регистрационного уд.
        if (response.Project.RegDate != null) {
            response.Project.RegDate = new Date(response.Project.RegDate.match(/\d+/)[0] * 1);
        }

        
        $scope.object = response;
        $scope.defaultPrice = response.Price;
        $scope.addPrice();
        if (response.Project.RegNumber != null) {
            $scope.isEnableDownload = true;
            $scope.isProjectSaved = true;
            $scope.isShowFindTab = false;
            $("#producerTabLink").addClass("active");
            $("#tab-1").addClass("active");
            $("#findTabLink").removeClass("active");
            $("#tab-0").removeClass("active");
        }
    });

}

function priceImnGrid($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.isProjectSaved = false;
    $scope.isShowFindTab = true;

    $(document).ready(function() {
        commonInit();
    });

    $scope.dtColumns1 = [
         DTColumnBuilder.newColumn("PartsName", "Наименование").withOption('name', 'Name'),
         DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withClass("dtc_manufacturerPrice"),
         DTColumnBuilder.newColumn("CipPrice", "CIP цена").withClass("dtc_cipPrice"),
         DTColumnBuilder.newColumn("RefPriceTypeName", "Тип референтной цены"),
         DTColumnBuilder.newColumn("RefPrice", "Референтная цена").withClass("dtc_refPrice"),
         DTColumnBuilder.newColumn("UnitPrice", "Цена заявителя").withClass("dtc_unitPrice")
    ];

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption("name", "CountryName"),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withOption("name", "ManufacturerPrice"),
        DTColumnBuilder.newColumn("ManufacturerPriceNote", "Ссылка на источник информации")
        .withOption("name", "ManufacturerPriceNote"),
    ];
    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("name", "Торговое название"),
        DTColumnBuilder.newColumn("expiration_date", "Дата истечение рег.").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("C_producer_name", "Производитель"),
        DTColumnBuilder.newColumn("C_country_name", "Страна")
    ];

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption("name", "Number"),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption("name", "Number").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption("name", "Number"),
    ];

    $scope.emailFormat = emailFormat;

    var id = $("#projectId").val();
    $scope.object = {};

    $scope.editProject = function (withAlert) {
        if (!$scope.IsCompleteness && $scope.isEnableDownload) {
            $http({
                url: "/Project/PriceImnSaveNotCompleteness",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                if (withAlert) {
                    customAlert("Данные сохранены");
                }
                $scope.isEnableDownload = true;
                $scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;
            });
        } else {
            $http({
                url: "/Project/PriceImnSave",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function(response) {
                if (withAlert) {
                    customAlert("Данные сохранены");
                }
                $scope.isEnableDownload = true;
                $scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;
            });
        }
    };
    var dateNow = new Date();
    //dateNow.setMonth(dateNow.getMonth() + 6);
    dateNow.setDate(dateNow.getDate() + 1);
    $scope.testDate = new Date();
    //DatePicker
    $scope.datePicker = {
        dateStartStatus: { opened: false },
        dateStartOpen: function($event) {
            $scope.datePicker.dateStartStatus.opened = true;
        },
        dateStartOptions: {
            maxDate: new Date()
        },
        dateEndStatus: {
            opened: false
        },
        dateEndOpen: function($event) {
            $scope.datePicker.dateEndStatus.opened = true;
        },
        dateEndOptions: {
            minDate: dateNow
        }
    };

    $scope.BoolDic = [
        {
            Id: true,
            Name: "Да"
        }, {
            Id: false,
            Name: "Нет"
        }
    ];


    $scope.sendProject = function() {
        $http({
            url: "/Price/SendProjectPrice",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function(response) {
            customAlert("Ок");
        });
    };
    $scope.view = function(id) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Upload/FileView?id=" + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.signPrice = function (id) {
        highlightNotValidTabs($scope.priceImnForm);
        if ($scope.priceImnForm.$valid) {
            $http({
                method: "GET",
                url: "/Upload/CheckAttachList",
                data: "JSON",
                params: { id: $scope.object.Project.Id, type: "sysAttachPriceDict" }
            }).success(function (result) {
                if (result == "True") {
                    if (!$scope.IsCompleteness) {
                        $http({
                            url: "/Project/PriceImnSaveNotCompleteness",
                            method: "POST",
                            data: JSON.stringify($scope.object)
                        }).success(function (response) {
                            startSign('/Price/SignOperation', id);
                        });
                    } else {
                        $http({
                            url: "/Project/PriceImnSave",
                            method: "POST",
                            data: JSON.stringify($scope.object)
                        }).success(function (response) {
                            startSign('/Price/SignOperation', id);
                        });
                    }
                } else {
                    customWarning("Требуются вложения:<br/> «Доверенность или копия» и «Сопроводительная письмо» ");
                }
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.sendAgree = function () {
        highlightNotValidTabs($scope.priceImnForm);
        if ($scope.priceImnForm.$valid) {
            if (!$scope.IsCompleteness) {
                $http({
                    url: "/Project/PriceImnSaveNotCompleteness",
                    method: "POST",
                    data: JSON.stringify($scope.object)
                }).success(function(response) {
                    IsLoadedDoc($http, $scope, $uibModal);
                });
            } else {
                $http({
                    url: "/Project/PriceImnSave",
                    method: "POST",
                    data: JSON.stringify($scope.object)
                }).success(function(response) {
                    IsLoadedDoc($http, $scope, $uibModal);
                });
            }
        } else {
            customWarning("Заполните обязательные поля");
        }
    };

    $scope.cbNoDataChange = function (cbVal) {
        if (cbVal) {
            return "-";
        } else {
            return "";
        }
    };

    $scope.SetObjectReg = function() {
        $http({
            method: "GET",
            url: "/Dictionaries/GetParts",
            data: "JSON",
            params: { id: $scope.curentReg.id }
        }).success(function(result) {
            $scope.Parts = result;
        });

        $scope.object.ManufacturerOrganization.NameKz = $scope.curentReg.C_producer_name_kz;
        $scope.object.ManufacturerOrganization.NameRu = $scope.curentReg.C_producer_name;
        $scope.object.ManufacturerOrganization.NameEn = $scope.curentReg.C_producer_name_en;

        $scope.object.HolderOrganization.NameKz = $scope.curentReg.owner_name_kz;
        $scope.object.HolderOrganization.NameRu = $scope.curentReg.owner_name_ru;
        $scope.object.HolderOrganization.NameEn = $scope.curentReg.owner_name_en;
        $scope.hoNameKz = $scope.object.HolderOrganization.NameKz;
        $scope.hoNameRu = $scope.object.HolderOrganization.NameRu;
        $scope.hoNameEn = $scope.object.HolderOrganization.NameEn;


        $scope.object.Project.NameKz = $scope.curentReg.name_kz;
        $scope.object.Project.NameRu = $scope.curentReg.name;
        $scope.object.Project.RegNumber = $scope.curentReg.reg_number;
        // для даты регистрационного уд.
        $scope.object.Project.RegDate = new Date($scope.curentReg.reg_date.match(/\d+/)[0] * 1);
        $scope.object.Project.MnnRu = $scope.curentReg.C_int_name;
        $scope.object.Project.FormNameRu = $scope.curentReg.C_dosage_form_name;
        $scope.object.Project.FormNameKz = $scope.curentReg.C_dosage_form_name_kz;
        $scope.object.Project.Dosage = $scope.curentReg.dosage_value;
        $scope.object.Project.CountPackage = $scope.curentReg.box_count;
        $scope.object.Project.Concentration = $scope.curentReg.concentration;
        $scope.object.Project.CodeAtx = $scope.curentReg.C_atc_code;
        $scope.object.Project.Volume = $scope.curentReg.volume;
        $scope.object.ManufacturerOrganization.CountryDicId = $scope.curentReg.C_country_Id;
        $scope.object.HolderOrganization.CountryDicId = $scope.curentReg.owner_country_id;

        $scope.object.Project.RegisterId = $scope.curentReg.id;
        $scope.object.Project.RegisterDfId = $scope.curentReg.df_id;

        //$scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;

        $scope.object.Project.ImnSecuryTypeDicId = $scope.curentReg.degree_risk_id;

        $scope.object.Project.Id = id;

        $scope.isEnableDownload = false;
        $scope.editProject(false);
        $scope.isProjectSaved = true;
        $scope.IsCompleteness = false;
        customAlert('Данные заполнены');
    }
    $scope.curentReg = {};

    $scope.selectGridIntegration = function(data) {
        console.log(data);
        $scope.curentReg = data;

    };
    $scope.selectGridPrice = function(data) {
        console.log(data);
        $scope.object.Price = data;
    };
    $scope.selectGridCurrentPrice = function (data) {
        if (data.Id == '11111111-1111-1111-1111-111111111111') {
            return;
        }
        console.log(data);
        $scope.object.CurentPrice = data;
    };
    $scope.addPrice = function() {
        $scope.object.Price = angular.copy($scope.defaultPrice);
    };
    $scope.addCurrentPrice = function () {
        $scope.object.CurentPrice = angular.copy($scope.defaultCurrentPrice);
    };
    $scope.deletePrice = function() {
        $http({
            url: "/Price/PriceDelete",
            method: "POST",
            data: JSON.stringify($scope.object.Price)
        }).success(function(response) {
            $scope.reloadGridPrice();
            $scope.addPrice();
        });

    };
    $scope.deleteCurrentPrice = function () {
        $http({
            url: "/Price/PriceDelete",
            method: "POST",
            data: JSON.stringify($scope.object.CurentPrice)
        }).success(function(response) {
            $scope.reloadGridCurrentPrice();
            $scope.addCurrentPrice();
        });

    };
    $scope.savePrice = function () {
        highlightNotValidTabs($scope.priceImnForm);
        if ($scope.priceImnForm.$valid) {
            $http({
                url: "/Price/PriceSave",
                method: "POST",
                data: JSON.stringify($scope.object.Price)
            }).success(function(response) {
                $scope.addPrice();
                $scope.reloadGridPrice();
                customAlert("Ок");
            });
        } else {
            customWarning("Заполните поля!");
        }
    };
    $scope.saveCurrentPrice = function () {
        highlightNotValidTabs($scope.priceImnForm);
        if ($scope.priceImnForm.$valid) {
            $http({
                url: "/Price/DeleteNotCompletenessPrices",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                $http({
                    url: "/Price/PriceSave",
                    method: "POST",
                    data: JSON.stringify($scope.object.CurentPrice)
                }).success(function (response) {
                    $scope.addCurrentPrice();
                    $scope.reloadGridCurrentPrice();
                    customAlert("Ок");
                });
            });
        } else {
            customWarning("Заполните поля!");
        }
    };
    $http({
        method: "GET",
        url: "/Project/LoadPriceImn?id=" + id,
        data: "JSON"
    }).success(function(response) {
        response.Project.DoverennostCreatedDate = new Date(response.StartDateYear,
            response.StartDateMonth - 1,
            response.StartDateDay);
        response.Project.DoverennostExpiryDate = new Date(response.EndDateYear,
            response.EndDateMonth - 1,
            response.EndDateDay);
        // для даты регистрационного уд.
        if (response.Project.RegDate != null) {
            response.Project.RegDate = new Date(response.Project.RegDate.match(/\d+/)[0] * 1);
        }
        $scope.object = response;
        $scope.defaultPrice = response.Price;
        $scope.defaultCurrentPrice = response.CurentPrice;
        $scope.addPrice();
        $scope.addCurrentPrice();
        loadDictionaryParts($scope, $http);

        if (response.Project.RegNumber != null) {
            $scope.isEnableDownload = true;
            $scope.isProjectSaved = true;
            $scope.isShowFindTab = false;
            $("#producerTabLink").addClass("active");
            $("#tab-1").addClass("active");
            $("#findTabLink").removeClass("active");
            $("#tab-0").removeClass("active");
        }
    });

    loadDictionary($scope, "Country", $http, true);
    loadDictionary($scope, "ImnSecuryType", $http);
    loadDictionaryByOrderYear($scope, "Currency", $http);
    loadDictionary($scope, "IntroducingMethod", $http);
    loadDictionary($scope, "LsType", $http);
    loadDictionary($scope, "RefPriceType", $http);
}

function rePriceLsGrid($scope, DTColumnBuilder, $http, $uibModal) {

    $(document).ready(function() {
        commonInit();
        $('#priceCountyDiv').on('hidden.bs.modal', function () {
            $scope.priceWindowOpened = false;
            $scope.$digest();
        });
        $('#priceCountyDiv').on('shown.bs.modal', function () {
            $scope.priceWindowOpened = true;
        });
    });

    $scope.emailFormat = emailFormat;

    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("Type", "Тип").withOption("name", "Number").notVisible(),
        DTColumnBuilder.newColumn("CountryName", "Страна"),
        DTColumnBuilder.newColumn("ManufacturerPriceWithLink", "Зарегистрированная цена производителя (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("LimitPriceWithLink", "Предельная цена (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("AvgOptPriceWithLink", "Средняя оптовая цена (за последний квартал) Указать ссылку на источник информации"),
        DTColumnBuilder.newColumn("AvgRozPriceWithLink", "Средняя розничная цена (за последний квартал) Указать ссылку на источник информации")
    ];

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("reg_date", "Дата регистрационного удостоверения").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("C_int_name", "МНН"),
        DTColumnBuilder.newColumn("SubstanceName", "Состав"),
        DTColumnBuilder.newColumn("name", "Торговое название"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма"),
        DTColumnBuilder.newColumn("concentration", "Концентрация"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка (мг)"),
        DTColumnBuilder.newColumn("um", "Способ введения"),
        DTColumnBuilder.newColumn("box_name1", "Первичная упаковка"),
        DTColumnBuilder.newColumn("box_name2", "Вторичная упаковка"),
        DTColumnBuilder.newColumn("box_count", "Количество во вторичной упаковке"), //Фасовка
        DTColumnBuilder.newColumn("volume", "Объем"),
        //DTColumnBuilder.newColumn("volume_measure", "Ед.изм."),
        DTColumnBuilder.newColumn("expiration_date", "Дата истечение рег.").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("C_producer_name", "Производитель"),
        DTColumnBuilder.newColumn("C_country_name", "Страна"),
        DTColumnBuilder.newColumn("description", "Признак ЛС"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код")
    ];

    $scope.dtColumns11 = [
        DTColumnBuilder.newColumn("Name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("DiseaseOfICD",
            "Заболевание группа по международной классификаций болезней 10 го пересмотра (далее – МКБ-10)*"),
        DTColumnBuilder.newColumn("SysnonimAndRareDesease", "Синонимы и названия редких болезней"),
        DTColumnBuilder.newColumn("CodeICD", "Код по МКБ-10")
    ];

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption("name", "Number"),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption("name", "Number").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption("name", "Number"),
    ];


    var id = $("#projectId").val();
    var parentId = $("#parentId").val();
    $scope.object = {
        project: {},
        HolderOrganization: {},
        ProxyOrganization: {},
        CurentPrice: {},
        Price: {}
    };

    var dateNow = new Date();
    //dateNow.setMonth(dateNow.getMonth() + 6);
    dateNow.setDate(dateNow.getDate() + 1);
    $scope.testDate = new Date();
    //DatePicker
    $scope.datePicker = {
        dateStartStatus: { opened: false },
        dateStartOpen: function($event) {
            $scope.datePicker.dateStartStatus.opened = true;
        },
        dateStartOptions: {
            maxDate: new Date()
        },
        dateEndStatus: {
            opened: false
        },
        dateEndOpen: function($event) {
            $scope.datePicker.dateEndStatus.opened = true;
        },
        dateEndOptions: {
            minDate: dateNow
        }
    };

    $scope.unitPriceCalc = function() {
        var OwnerPrice = $scope.object.CurentPrice.OwnerPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (OwnerPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.UnitPrice = $scope.object.CurentPrice.OwnerPrice /
                $scope.object.Project.CountPackage;
            $scope.object.Price.UnitPrice = $scope.object.CurentPrice.UnitPrice;
        }
    };
    $scope.ownerPriceCalc = function() {
        var UnitPrice = $scope.object.CurentPrice.UnitPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (UnitPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.OwnerPrice = $scope.object.CurentPrice.UnitPrice *
                $scope.object.Project.CountPackage;
            $scope.object.Price.OwnerPrice = $scope.object.CurentPrice.OwnerPrice;
        }
    };
    $scope.unitBritishPriceCalc = function() {
        var BritishPrice = $scope.object.CurentPrice.BritishPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (BritishPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.BritishCost = $scope.object.CurentPrice.BritishPrice /
                $scope.object.Project.CountPackage;
        }
    };
    $scope.ownerBritishPriceCalc = function() {
        var BritishCost = $scope.object.CurentPrice.BritishCost;
        var CountPackage = $scope.object.Project.CountPackage;
        if (BritishCost != null && CountPackage != null) {
            $scope.object.CurentPrice.BritishPrice = $scope.object.CurentPrice.BritishCost *
                $scope.object.Project.CountPackage;
        }
    };

    $scope.editProject = function (withAlert) {
        $http({
            url: "/Project/RePriceLsSave",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            if (withAlert) {
                customAlert("Данные сохранены");
            }
            $scope.isEnableDownload = true;
        });
    };

    $scope.BoolDic = [
        {
            Id: true,
            Name: "Да"
        }, {
            Id: false,
            Name: "Нет"
        }
    ];


    $scope.sendProject = function() {
        $http({
            url: "/Price/SendProjectPrice",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function(response) {
            customAlert("Ок");
        });
    };
    $scope.view = function(id) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Upload/FileView?id=" + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.signPrice = function (id) {
        highlightNotValidTabs($scope.rePriceLsForm);
        if ($scope.rePriceLsForm.$valid) {
            $http({
                method: "GET",
                url: "/Upload/CheckAttachList",
                data: "JSON",
                params: { id: $scope.object.Project.Id, type: "sysAttachPriceDict" }
            }).success(function (result) {
                if (result == "True") {
                    $http({
                        url: "/Project/RePriceLsSave",
                        method: "POST",
                        data: JSON.stringify($scope.object)
                    }).success(function (response) {
                        startSign('/Price/SignOperation', id);
                    });
                } else {
                    customWarning("Требуются вложения:<br/> «Доверенность или копия» и «Сопроводительная письмо» ");
                }
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.sendAgree = function () {
        highlightNotValidTabs($scope.rePriceLsForm);
        if ($scope.rePriceLsForm.$valid) {
            $http({
                url: "/Project/RePriceLsSave",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function(response) {
                IsLoadedDoc($http, $scope, $uibModal);
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };

    $scope.SetObjectReg = function(isOrphan) {
        if (isOrphan) {
            cleanObject($scope);
            $scope.object.Project.NameRu = $scope.curentReg.Name;
        } else {
            $scope.object.ManufacturerOrganization.NameKz = $scope.curentReg.C_producer_name_kz;
            $scope.object.ManufacturerOrganization.NameRu = $scope.curentReg.C_producer_name;
            $scope.object.ManufacturerOrganization.NameEn = $scope.curentReg.C_producer_name_en;

            $scope.object.HolderOrganization.NameKz = $scope.curentReg.owner_name_kz;
            $scope.object.HolderOrganization.NameRu = $scope.curentReg.owner_name_ru;
            $scope.object.HolderOrganization.NameEn = $scope.curentReg.owner_name_en;
            $scope.hoNameKz = $scope.object.HolderOrganization.NameKz;
            $scope.hoNameRu = $scope.object.HolderOrganization.NameRu;
            $scope.hoNameEn = $scope.object.HolderOrganization.NameEn;


            $scope.object.Project.NameKz = $scope.curentReg.name_kz;
            $scope.object.Project.NameRu = $scope.curentReg.name;
            $scope.object.Project.RegNumber = $scope.curentReg.reg_number;
            // для даты регистрационного уд.
            $scope.object.Project.RegDate = new Date($scope.curentReg.reg_date.match(/\d+/)[0] * 1);
            $scope.object.Project.MnnRu = $scope.curentReg.C_int_name;
            $scope.object.Project.FormNameRu = $scope.curentReg.C_dosage_form_name;
            $scope.object.Project.FormNameKz = $scope.curentReg.C_dosage_form_name_kz;
            $scope.object.Project.Dosage = $scope.curentReg.dosage_value;
            $scope.object.Project.CountPackage = $scope.curentReg.box_count;
            $scope.object.Project.Concentration = $scope.curentReg.concentration;
            $scope.object.Project.CodeAtx = $scope.curentReg.C_atc_code;
            $scope.object.Project.Volume = $scope.curentReg.volume;
            $scope.object.ManufacturerOrganization.CountryDicId = $scope.curentReg.C_country_Id;
            $scope.object.HolderOrganization.CountryDicId = $scope.curentReg.owner_country_id;
            //$scope.object.ProxyOrganization.CountryDicId = $scope.curentReg.C_country_Id;
            $scope.object.Project.IntroducingMethodDicId = $scope.curentReg.umId;
            $scope.object.Project.ImnSecuryTypeDicId = $scope.curentReg.degree_risk_id;
        }
        customAlert("Данные заполнены");
    };
    $scope.curentReg = {};

    $scope.selectGridIntegration = function(data) {
        console.log(data);
        $scope.curentReg = data;
    };
    $scope.editPrice = function () {
        $("#priceCountyDiv").modal();
    };
    $scope.deletePrice = function () {
        $http({
            url: "/Price/PriceDelete",
            method: "POST",
            data: JSON.stringify($scope.object.Price)
        }).success(function (response) {
            $scope.reloadGridPrice();
            $scope.addPrice();
        });
        var cancel = function () {
        };
        showConfirmation("Подтверждение", "Вы уверены что хотите очистить данные?", success, cancel);
    };
    $scope.createPrice = function () {
        $scope.object.Price = angular.copy($scope.defaultPrice);
        $("#priceCountyDiv").modal();
    };
    $scope.addPrice = function() {
        $scope.object.Price = angular.copy($scope.defaultPrice);
    };
    $scope.selectGridPrice = function (data) {
        console.log(data);
        $scope.object.Price = data;
    };
    $scope.savePrice = function() {
        var isValid = true;
        if (!$scope.object.Price.IsManufacturerPrice) {
            isValid = $scope.object.Price.ManufacturerPrice > 0 &&
                $scope.object.Price.ManufacturerPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.ManufacturerPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsLimitPrice) {
            isValid = isValid &&
                $scope.object.Price.LimitPrice > 0 &&
                $scope.object.Price.LimitPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.LimitPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsAvgOptPrice) {
            isValid = isValid &&
                $scope.object.Price.AvgOptPrice > 0 &&
                $scope.object.Price.AvgOptPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.AvgOptPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsAvgRozPrice) {
            isValid = isValid &&
                $scope.object.Price.AvgRozPrice > 0 &&
                $scope.object.Price.AvgRozPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.AvgRozPriceNote.length > 0;
        }
        //        highlightNotValidTabs($scope.priceImnForm);
        if (isValid) {
            $http({
                url: "/Price/PriceSave",
                method: "POST",
                data: JSON.stringify($scope.object.Price)
            }).success(function (response) {
                $scope.addPrice();
                $scope.reloadGridPrice();
                $("#priceCountyDiv").modal('hide');
            });
        } else {
            // customWarning("Заполните поля!");
        }
    };


    var urlLoad = "";
    if (parentId == "") {
        urlLoad = "/Project/LoadRePriceLs?id=" + id;
    } else {
        urlLoad = "/Project/LoadRePriceLsParent?id=" + id + "&parentId=" + parentId;
    }

    $http({
        method: "GET",
        url: urlLoad,
        data: "JSON"
    }).success(function(response) {
        response.Project.DoverennostCreatedDate = new Date(response.StartDateYear,
            response.StartDateMonth - 1,
            response.StartDateDay);
        response.Project.DoverennostExpiryDate = new Date(response.EndDateYear,
            response.EndDateMonth - 1,
            response.EndDateDay);
        // для даты регистрационного уд.
        if (response.Project.RegDate != null) {
            response.Project.RegDate = new Date(response.Project.RegDate.match(/\d+/)[0] * 1);
        } else {
            response.Project.RegDate = new Date();
        }
        $scope.object = response;
        $scope.defaultPrice = response.Price;
        $scope.addPrice();

        $scope.editProject(false);
    });

    loadDictionary($scope, "Country", $http, true);
    loadDictionaryRePrice($scope, "RePrice", $http, parentId);
    //loadDictionary($scope, 'RePrice', $http);
    loadDictionaryByOrderYear($scope, "Currency", $http);
    loadDictionary($scope, "IntroducingMethod", $http);
    loadDictionary($scope, "LsType", $http);
    loadDictionary($scope, "RefPriceType", $http);
}

function rePriceImnGrid($scope, DTColumnBuilder, $http, $uibModal) {

    $(document).ready(function() {
        commonInit();
    });

    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("PartsName", "Наименование").withOption("name", "PartsName"),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя"),
        DTColumnBuilder.newColumn("CipPrice", "CIP цена"),
        DTColumnBuilder.newColumn("RefPriceTypeName", "Тип референтной цены"),
        DTColumnBuilder.newColumn("RefPrice", "Референтная цена"),
        DTColumnBuilder.newColumn("UnitPrice", "Цена заявителя")
    ];

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption("name", "CountryName"),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withOption("name", "ManufacturerPrice"),
        DTColumnBuilder.newColumn("ManufacturerPriceNote", "Ссылка на источник информации")
        .withOption("name", "ManufacturerPriceNote"),
    ];

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("name", "Торговое название"),
        DTColumnBuilder.newColumn("expiration_date", "Дата истечение рег.").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("C_producer_name", "Производитель"),
        DTColumnBuilder.newColumn("C_country_name", "Страна")
    ];

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption("name", "Number"),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption("name", "Number").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption("name", "Number"),
    ];

    var parentId = $("#parentId").val();

    var priceProjectType = $("#priceProjectType").val();
    
    var id = $("#projectId").val();
    $scope.object = {};

    $scope.editProject = function (withAlert) {
        if (!$scope.IsCompleteness) {
            $http({
                url: "/Project/RePriceImnSaveNotCompleteness",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function(response) {
                if (withAlert) {
                    customAlert("Ок");
                }
                $scope.isEnableDownload = true;
                $scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;
            });
        } else {
            $http({
                url: "/Project/RePriceImnSave",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function(response) {
                if (withAlert) {
                    customAlert("Ок");
                }
                $scope.isEnableDownload = true;
                $scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;
            });
        }
    };
    var dateNow = new Date();
    //dateNow.setMonth(dateNow.getMonth() + 6);
    dateNow.setDate(dateNow.getDate() + 1);
    $scope.testDate = new Date();
    //DatePicker
    $scope.datePicker = {
        dateStartStatus: { opened: false },
        dateStartOpen: function($event) {
            $scope.datePicker.dateStartStatus.opened = true;
        },
        dateStartOptions: {
            maxDate: new Date()
        },
        dateEndStatus: {
            opened: false
        },
        dateEndOpen: function($event) {
            $scope.datePicker.dateEndStatus.opened = true;
        },
        dateEndOptions: {
            minDate: dateNow
        }
    };

    $scope.BoolDic = [
        {
            Id: true,
            Name: "Да"
        }, {
            Id: false,
            Name: "Нет"
        }
    ];


    $scope.sendProject = function() {
        $http({
            url: "/Price/SendProjectPrice",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function(response) {
            customAlert("Ок");
        });
    };
    $scope.view = function(id) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Upload/FileView?id=" + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.signPrice = function (id) {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
            $http({
                method: "GET",
                url: "/Upload/CheckAttachList",
                data: "JSON",
                params: { id: $scope.object.Project.Id, type: "sysAttachPriceDict" }
            }).success(function (result) {
                if (result == "True") {
                    if (!$scope.IsCompleteness) {
                        $http({
                            url: "/Project/RePriceImnSaveNotCompleteness",
                            method: "POST",
                            data: JSON.stringify($scope.object)
                        }).success(function (response) {
                            startSign('/Price/SignOperation', id);
                        });
                    } else {
                        $http({
                            url: "/Project/RePriceImnSave",
                            method: "POST",
                            data: JSON.stringify($scope.object)
                        }).success(function (response) {
                            startSign('/Price/SignOperation', id);
                        });
                    }
                } else {
                    customWarning("Требуются вложения:<br/> «Доверенность или копия» и «Сопроводительная письмо» ");
                }
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.sendAgree = function () {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
            if (!$scope.IsCompleteness) {
                $http({
                    url: "/Project/RePriceImnSaveNotCompleteness",
                    method: "POST",
                    data: JSON.stringify($scope.object)
                }).success(function(response) {
                    IsLoadedDoc($http, $scope, $uibModal);
                });
            } else {
                $http({
                    url: "/Project/RePriceImnSave",
                    method: "POST",
                    data: JSON.stringify($scope.object)
                }).success(function(response) {
                    IsLoadedDoc($http, $scope, $uibModal);
                });
            }
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    
    $scope.cbNoDataChange = function (cbVal) {
        if (cbVal) {
            return "-";
        } else {
            return "";
        }
    };

    $scope.SetObjectReg = function() {

        $http({
            method: "GET",
            url: "/Dictionaries/GetParts",
            data: "JSON",
            params: { id: $scope.curentReg.id }
        }).success(function(result) {
            $scope.Parts = result;
        });

        $scope.object.ManufacturerOrganization.NameKz = $scope.curentReg.C_producer_name_kz;
        $scope.object.ManufacturerOrganization.NameRu = $scope.curentReg.C_producer_name;
        $scope.object.ManufacturerOrganization.NameEn = $scope.curentReg.C_producer_name_en;

        $scope.object.HolderOrganization.NameKz = $scope.curentReg.owner_name_kz;
        $scope.object.HolderOrganization.NameRu = $scope.curentReg.owner_name_ru;
        $scope.object.HolderOrganization.NameEn = $scope.curentReg.owner_name_en;
        $scope.hoNameKz = $scope.object.HolderOrganization.NameKz;
        $scope.hoNameRu = $scope.object.HolderOrganization.NameRu;
        $scope.hoNameEn = $scope.object.HolderOrganization.NameEn;

        $scope.object.Project.NameKz = $scope.curentReg.name_kz;
        $scope.object.Project.NameRu = $scope.curentReg.name;
        $scope.object.Project.RegNumber = $scope.curentReg.reg_number;
        // для даты регистрационного уд.
        $scope.object.Project.RegDate = new Date($scope.curentReg.reg_date.match(/\d+/)[0] * 1);
        $scope.object.Project.MnnRu = $scope.curentReg.C_int_name;
        $scope.object.Project.FormNameRu = $scope.curentReg.C_dosage_form_name;
        $scope.object.Project.FormNameKz = $scope.curentReg.C_dosage_form_name_kz;
        $scope.object.Project.Dosage = $scope.curentReg.dosage_value;
        $scope.object.Project.CountPackage = $scope.curentReg.box_count;
        $scope.object.Project.Concentration = $scope.curentReg.concentration;
        $scope.object.Project.CodeAtx = $scope.curentReg.C_atc_code;
        $scope.object.Project.Volume = $scope.curentReg.volume;
        $scope.object.ManufacturerOrganization.CountryDicId = $scope.curentReg.C_country_Id;
        $scope.object.HolderOrganization.CountryDicId = $scope.curentReg.owner_country_id;
        //$scope.object.ProxyOrganization.CountryDicId = $scope.curentReg.C_country_Id;

        alert("Данные заполнены");
    };

    $scope.curentReg = {};

    $scope.selectGridIntegration = function(data) {
        console.log(data);
        $scope.curentReg = data;
    };

    // методы идентичны priceImnGrid
    $scope.selectGridPrice = function(data) {
        console.log(data);
        $scope.object.Price = data;
    };
    $scope.selectGridCurrentPrice = function (data) {
        console.log(data);
        $scope.object.CurentPrice = data;
    };
    $scope.addPrice = function() {
        $scope.object.Price = angular.copy($scope.defaultPrice);
    };
    $scope.addCurrentPrice = function () {
        $scope.object.CurentPrice = angular.copy($scope.defaultCurrentPrice);
    };
    $scope.deletePrice = function() {
        $http({
            url: "/Price/PriceDelete",
            method: "POST",
            data: JSON.stringify($scope.object.Price)
        }).success(function(response) {
            $scope.reloadGridPrice();
            $scope.addPrice();
        });

    };
    $scope.deleteCurrentPrice = function () {
        $http({
            url: "/Price/PriceDelete",
            method: "POST",
            data: JSON.stringify($scope.object.CurentPrice)
        }).success(function(response) {
            $scope.reloadGridCurrentPrice();
            $scope.addCurrentPrice();
        });

    };
    $scope.savePrice = function () {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
            $scope.object.Price.Type = 5; // костыль до выяснения обстоятельств
            $http({
                url: "/Price/PriceSave",
                method: "POST",
                data: JSON.stringify($scope.object.Price)
            }).success(function(response) {
                $scope.addPrice();
                $scope.reloadGridPrice();
                customAlert("Ок");
            });
        } else {
            customWarning("Заполните поля!");
        }
    };
    $scope.saveCurrentPrice = function () {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
            $scope.object.CurentPrice.Type = 4; // костыль до выяснения обстоятельств
            $http({
                url: "/Price/DeleteNotCompletenessPrices",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                $http({
                    url: "/Price/PriceSave",
                    method: "POST",
                    data: JSON.stringify($scope.object.CurentPrice)
                }).success(function (response) {
                    $scope.addCurrentPrice();
                    $scope.reloadGridCurrentPrice();
                    customAlert("Ок");
                });
            });
        } else {
            customWarning("Заполните поля!");
        }
    };
    var urlLoad = "";
    if (parentId == "") {
        urlLoad = "/Project/LoadRePriceImn?id=" + id;
    } else {
        urlLoad = "/Project/LoadRePriceImnParent?id=" + id + "&parentId=" + parentId + "&priceProjectType=" + priceProjectType;
    }

    $http({
        method: "GET",
        url: urlLoad,
        data: "JSON"
    }).success(function(response) {
        response.Project.DoverennostCreatedDate = new Date(response.StartDateYear,
            response.StartDateMonth - 1,
            response.StartDateDay);
        response.Project.DoverennostExpiryDate = new Date(response.EndDateYear,
            response.EndDateMonth - 1,
            response.EndDateDay);
        // для даты регистрационного уд.
        if (response.Project.RegDate != null) {
            response.Project.RegDate = new Date(response.Project.RegDate.match(/\d+/)[0] * 1);
        } else {
            response.Project.RegDate = new Date();
        }
        $scope.object = response;
        $scope.editProject(false);

        // комлектность, список наименований
        loadDictionaryParts($scope, $http);
        loadDictionaryRePrice($scope, "RePrice", $http, $scope.object.Project.PriceProjectId);
        $scope.defaultPrice = response.Price;
        $scope.defaultCurrentPrice = response.CurentPrice;
        $scope.defaultCurrentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;
        $scope.addPrice();
        $scope.addCurrentPrice();
    });

    loadDictionary($scope, "Country", $http, true);
    loadDictionary($scope, "ImnSecuryType", $http);
    loadDictionaryByOrderYear($scope, "Currency", $http);
    loadDictionary($scope, "IntroducingMethod", $http);
    loadDictionary($scope, "LsType", $http);
    loadDictionary($scope, "RefPriceType", $http);
    //loadDictionary($scope, 'RePrice', $http);

}

function rePriceLsNewGrid($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.isProjectSaved = false;
    $scope.isShowFindTab = true;

    function actionsPriceHtmlAction(data, type, full, meta) {
        if (full.Status === 0) {
            return '<span class="glyphicon glyphicon-trash" title="Удалить" onclick="removePrice(this)" modelId="' + full.Id + '"></span>';
        }
        return "";
    }

    $(document).ready(function () {
        commonInit();
        $('#priceCountyDiv').on('hidden.bs.modal', function () {
            $scope.priceWindowOpened = false;
            $scope.$digest();
        });
        $('#priceCountyDiv').on('shown.bs.modal', function () {
            $scope.priceWindowOpened = true;
        });
    });

    $scope.emailFormat = "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$";
    $scope.decimalRegex = decimalRegex;

    $scope.cbNoDataChange = function (cbVal) {
        if (cbVal) {
            return "-";
        } else {
            return "";
        }
    };
    $scope.GetCountries = function (search) {
        var c = $scope.Country;
        if (search && c.indexOf(search) === -1) {
            c.unshift(search);
        }
        return c;
    };
    $scope.dtColumns1 = [
        DTColumnBuilder.newColumn("Type", "Тип").withOption("name", "Number").notVisible(),
        DTColumnBuilder.newColumn("CountryName", "Страна"),
        DTColumnBuilder.newColumn("ManufacturerPriceWithLink", "Зарегистрированная цена производителя (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("LimitPriceWithLink", "Предельная цена (указать год, ссылку на источник информации)"),
        DTColumnBuilder.newColumn("AvgOptPriceWithLink", "Средняя оптовая цена (за последний квартал) Указать ссылку на источник информации"),
        DTColumnBuilder.newColumn("AvgRozPriceWithLink", "Средняя розничная цена (за последний квартал) Указать ссылку на источник информации"),
        DTColumnBuilder.newColumn("IsIncluded", "Поставляет").renderWith(priceIsIncluded)
    ];

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения").withClass("dtc_reg_number"),
        DTColumnBuilder.newColumn("reg_date", "Дата регистрационного удостоверения").renderWith(dateformatHtml)
        .withClass("dtc_reg_date"),
        DTColumnBuilder.newColumn("C_int_name", "МНН").withClass("dtc_int_name"),
        DTColumnBuilder.newColumn("SubstanceName", "Состав").withClass("dtc_SubstanceName"),
        DTColumnBuilder.newColumn("name", "Торговое название").withClass("dtc_name"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма").withClass("dtc_dosage_form_name"),
        DTColumnBuilder.newColumn("concentration", "Концентрация").withClass("dtc_concentration"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка (мг)").withClass("dtc_dosage_value"),
        DTColumnBuilder.newColumn("um", "Способ введения").withClass("dtc_um"),
        DTColumnBuilder.newColumn("box_name1", "Первичная упаковка").withClass("dtc_box_name1"),
        DTColumnBuilder.newColumn("box_name2", "Вторичная упаковка").withClass("dtc_box_name2"),
        DTColumnBuilder.newColumn("box_count", "Количество во вторичной упаковке").withClass("dtc_box_count"),
        DTColumnBuilder.newColumn("volume", "Объем").withClass("dtc_volume"),
        DTColumnBuilder.newColumn("expiration_date", "Дата истечение рег.").renderWith(dateformatHtml)
        .withClass("dtc_expiration_date"),
        DTColumnBuilder.newColumn("C_producer_name", "Производитель").withClass("dtc_producer_name"),
        DTColumnBuilder.newColumn("C_country_name", "Страна").withClass("dtc_country_name"),
        DTColumnBuilder.newColumn("description", "Признак ЛС").withClass("dtc_description"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код").withClass("dtc_atc_code")
    ];

    $scope.dtColumns10Full = angular.copy($scope.dtColumns10);
    $scope.dtColumns10Full = $scope.dtColumns10Full.map(function (obj) {
        var newObj = obj;
        newObj["columnVisible"] = true;
        return newObj;
    });

    Array.prototype.getIndexOfObject = function (prop, value) {
        for (var i = 0; i < this.length ; i++) {
            if (this[i][prop] === value) {
                return i;
            }
        }
    }

    $scope.cbChange = function (val, cl) {
        if (val) {
            var insertedInd = $scope.dtColumns10Full.getIndexOfObject("sClass", cl);
            $scope.dtColumns10.splice(insertedInd, 0, $scope.dtColumns10Full[insertedInd]);
            return true;
        } else {
            if ($scope.dtColumns10.length > 1) {
                var ind = $scope.dtColumns10.getIndexOfObject("sClass", cl);
                $scope.dtColumns10.splice(ind, 1);
                return false;
            } else {
                return true;
            }
        }
    };
    $scope.dtColumns11 = [
        DTColumnBuilder.newColumn("Name", "Наименование ЛС"),
        DTColumnBuilder.newColumn("DiseaseOfICD",
            "Заболевание группа по международной классификаций болезней 10 го пересмотра (далее – МКБ-10)*"),
        DTColumnBuilder.newColumn("SysnonimAndRareDesease", "Синонимы и названия редких болезней"),
        DTColumnBuilder.newColumn("CodeICD", "Код по МКБ-10")
    ];

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption("name", "Number"),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption("name", "Number").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption("name", "Number"),
    ];

    var id = $("#projectId").val();
    $scope.object = {
        project: {},
        HolderOrganization: {},
        ProxyOrganization: {},
        CurentPrice: {},
        Price: {}
    };

    var dateNow = new Date();
    //dateNow.setMonth(dateNow.getMonth() + 6);
    dateNow.setDate(dateNow.getDate() + 1);
    $scope.testDate = new Date();
    //DatePicker
    $scope.datePicker = {
        dateStartStatus: { opened: false },
        dateStartOpen: function ($event) {
            $scope.datePicker.dateStartStatus.opened = true;
        },
        dateStartOptions: {
            // maxDate: new Date()
        },
        dateEndStatus: {
            opened: false
        },
        dateEndOpen: function ($event) {
            $scope.datePicker.dateEndStatus.opened = true;
        },
        dateEndOptions: {
            minDate: dateNow
        }
    };

    $scope.unitPriceCalc = function () {
        var OwnerPrice = $scope.object.CurentPrice.OwnerPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (OwnerPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.UnitPrice = $scope.object.CurentPrice.OwnerPrice /
                $scope.object.Project.CountPackage;
            $scope.object.Price.UnitPrice = $scope.object.CurentPrice.UnitPrice;
        }
    };
    $scope.ownerPriceCalc = function () {
        var UnitPrice = $scope.object.CurentPrice.UnitPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (UnitPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.OwnerPrice = $scope.object.CurentPrice.UnitPrice *
                $scope.object.Project.CountPackage;
            $scope.object.Price.OwnerPrice = $scope.object.CurentPrice.OwnerPrice;
        }
    };
    $scope.unitBritishPriceCalc = function () {
        var BritishPrice = $scope.object.CurentPrice.BritishPrice;
        var CountPackage = $scope.object.Project.CountPackage;
        if (BritishPrice != null && CountPackage != null) {
            $scope.object.CurentPrice.BritishCost = $scope.object.CurentPrice.BritishPrice /
                $scope.object.Project.CountPackage;
        }
    };
    $scope.ownerBritishPriceCalc = function () {
        var BritishCost = $scope.object.CurentPrice.BritishCost;
        var CountPackage = $scope.object.Project.CountPackage;
        if (BritishCost != null && CountPackage != null) {
            $scope.object.CurentPrice.BritishPrice = $scope.object.CurentPrice.BritishCost *
                $scope.object.Project.CountPackage;
        }
    };
    $scope.BoolDic = [
        {
            Id: true,
            Name: "Да"
        }, {
            Id: false,
            Name: "Нет"
        }
    ];

    $scope.sendProject = function () {
        if ($scope.rePriceLsForm.$valid) {
            $http({
                url: "/Price/SendProjectPrice",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                customAlert("Ок");
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Upload/FileView?id=" + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.signPrice = function (id) {
        highlightNotValidTabs($scope.rePriceLsForm);
        if ($scope.rePriceLsForm.$valid) {
            $http({
                method: "GET",
                url: "/Upload/CheckAttachList",
                data: "JSON",
                params: { id: $scope.object.Project.Id, type: "sysAttachPriceDict" }
            }).success(function (result) {
                if (result == "True") {
                    $http({
                        url: "/Project/PriceLsSave",
                        method: "POST",
                        data: JSON.stringify($scope.object)
                    }).success(function (response) {
                        startSign('/Price/SignOperation', id);
                    });
                } else {
                    customWarning("Требуются вложения:<br/> «Доверенность или копия» и «Сопроводительная письмо» ");
                }
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.sendAgree = function () {
        highlightNotValidTabs($scope.rePriceLsForm);
        if ($scope.rePriceLsForm.$valid) {
            $http({
                url: "/Project/PriceLsSave",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                IsLoadedDoc($http, $scope, $uibModal);
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };


    $scope.editProject = function (withAlert) {
        $http({
            url: "/Project/PriceLsSave",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            if (withAlert) {
                customAlert("Данные сохранены");
            }
            $scope.isEnableDownload = true;
        });
    };
    $scope.sendProject = function () {
        $http({
            url: "/Price/SendProjectPrice",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            customAlert("Ок");
        });
    };
    $scope.selectGridPrice = function (data) {
        console.log(data);
        $scope.object.Price = data;
    };
    $scope.SetObjectReg = function (isOrphan) {
        if (isOrphan) {
            cleanObject($scope);
            $scope.object.Project.NameRu = $scope.curentReg.Name;
            $scope.object.Project.Id = id;
            $scope.editProject();
        } else {
            $scope.object.CurentPrice.OwnerPrice = '';
            $scope.object.CurentPrice.UnitPrice = '';
            $scope.object.CurentPrice.RefPrice = '';
            $scope.object.ManufacturerOrganization.NameKz = $scope.curentReg.C_producer_name_kz;
            $scope.object.ManufacturerOrganization.NameRu = $scope.curentReg.C_producer_name;
            $scope.object.ManufacturerOrganization.NameEn = $scope.curentReg.C_producer_name_en;

            $scope.object.HolderOrganization.NameKz = $scope.curentReg.owner_name_kz;
            $scope.object.HolderOrganization.NameRu = $scope.curentReg.owner_name_ru;
            $scope.object.HolderOrganization.NameEn = $scope.curentReg.owner_name_en;
            $scope.hoNameKz = $scope.object.HolderOrganization.NameKz;
            $scope.hoNameRu = $scope.object.HolderOrganization.NameRu;
            $scope.hoNameEn = $scope.object.HolderOrganization.NameEn;

            $scope.object.Project.NameKz = $scope.curentReg.name_kz;
            $scope.object.Project.NameRu = $scope.curentReg.name;
            $scope.object.Project.RegNumber = $scope.curentReg.reg_number;
            // для даты регистрационного уд.
            $scope.object.Project.RegDate = new Date($scope.curentReg.reg_date.match(/\d+/)[0] * 1);
            $scope.object.Project.MnnRu = $scope.curentReg.C_int_name;
            $scope.object.Project.FormNameRu = $scope.curentReg.C_dosage_form_name;
            $scope.object.Project.FormNameKz = $scope.curentReg.C_dosage_form_name_kz;
            $scope.object.Project.Dosage = $scope.curentReg.dosage_value;
            $scope.object.Project.CountPackage = $scope.curentReg.box_count;
            $scope.object.Project.Concentration = $scope.curentReg.concentration;
            $scope.object.Project.CodeAtx = $scope.curentReg.C_atc_code;
            $scope.object.Project.Volume = $scope.curentReg.volume;
            $scope.object.ManufacturerOrganization.CountryDicId = $scope.curentReg.C_country_Id;
            $scope.object.HolderOrganization.CountryDicId = $scope.curentReg.owner_country_id;
            //$scope.object.ProxyOrganization.CountryDicId = $scope.curentReg.C_country_Id;
            $scope.object.Project.IntroducingMethodDicId = $scope.curentReg.umId;

            $scope.object.Project.RegisterId = $scope.curentReg.id;
            $scope.object.Project.RegisterDfId = $scope.curentReg.df_id;

            $scope.object.Project.Id = id;


            if ($scope.object.CurentPrice.ManufacturerPrice == "0") {
                $scope.object.CurentPrice.ManufacturerPrice = "";
            }
            if ($scope.object.CurentPrice.CipPrice == "0") {
                $scope.object.CurentPrice.CipPrice = "";
            }
            if ($scope.object.CurentPrice.RefPrice == "0") {
                $scope.object.CurentPrice.RefPrice = "";
            }
            if ($scope.object.CurentPrice.OwnerPrice == "0") {
                $scope.object.CurentPrice.OwnerPrice = "";
            }
            if ($scope.object.CurentPrice.UnitPrice == "0") {
                $scope.object.CurentPrice.UnitPrice = "";
            }
            if ($scope.object.CurentPrice.BritishPrice == "0") {
                $scope.object.CurentPrice.BritishPrice = "";
            }
            if ($scope.object.CurentPrice.BritishCost == "0") {
                $scope.object.CurentPrice.BritishCost = "";
            }
            $scope.object.CurentPrice.OwnerPriceCurrencyDicId = $scope.Currency[0].Id;
            $scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;

            $scope.editProject(false);
        }
        $scope.isProjectSaved = true;
        customAlert("Данные заполнены");
        $http({
            method: "GET",
            url: "/Dictionaries/GetDicIdByCode",
            data: "JSON",
            params: { dicType: "RePrice", code: "2" }
        }).success(function (result) {
            $scope.object.Project.RePriceDicId = result;
        });
    };
    $scope.curentReg = {};

    $scope.selectGridIntegration = function (data) {
        console.log(data);
        $scope.curentReg = data;

    };
    $scope.editPrice = function () {
        $("#priceCountyDiv").modal();

    };
    $scope.deletePrice = function () {
        var success = function () {
            $http({
                url: "/Price/PriceDelete",
                method: "POST",
                data: JSON.stringify($scope.object.Price)
            }).success(function (response) {
                $scope.reloadGridPrice();
                $scope.addPrice();
            });
        }
        var cancel = function () {
        };
        showConfirmation("Подтверждение", "Вы уверены что хотите очистить данные?", success, cancel);


    };
    $scope.createPrice = function () {
        $scope.object.Price = angular.copy($scope.defaultPrice);
        $("#priceCountyDiv").modal();
    };
    $scope.addPrice = function () {
        $scope.object.Price = angular.copy($scope.defaultPrice);
        //   $("#priceCountyDiv").modal();
    };
    $scope.savePrice = function () {
        var isValid = true;
        if (!$scope.object.Price.IsManufacturerPrice) {
            isValid = $scope.object.Price.ManufacturerPrice > 0 &&
                $scope.object.Price.ManufacturerPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.ManufacturerPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsLimitPrice) {
            isValid = isValid &&
                $scope.object.Price.LimitPrice > 0 &&
                $scope.object.Price.LimitPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.LimitPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsAvgOptPrice) {
            isValid = isValid &&
                $scope.object.Price.AvgOptPrice > 0 &&
                $scope.object.Price.AvgOptPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.AvgOptPriceNote.length > 0;
        }
        if (!$scope.object.Price.IsAvgRozPrice) {
            isValid = isValid &&
                $scope.object.Price.AvgRozPrice > 0 &&
                $scope.object.Price.AvgRozPriceCurrencyDicId.length > 0 &&
                $scope.object.Price.AvgRozPriceNote.length > 0;
        }
        //        highlightNotValidTabs($scope.priceImnForm);

        if (isValid) {
            $http({
                url: "/Price/PriceSave",
                method: "POST",
                data: JSON.stringify($scope.object.Price)
            }).success(function (response) {
                $scope.addPrice();
                $scope.reloadGridPrice();
                $("#priceCountyDiv").modal('hide');
            });
        } else {
            // customWarning("Заполните поля!");
        }
    };
    $http({
        method: "GET",
        url: "/Project/LoadRePriceLs?id=" + id,
        data: "JSON"
    }).success(function (response) {
        response.Project.DoverennostCreatedDate = new Date(response.StartDateYear,
            response.StartDateMonth - 1,
            response.StartDateDay);
        response.Project.DoverennostExpiryDate = new Date(response.EndDateYear,
            response.EndDateMonth - 1,
            response.EndDateDay);

        // для даты регистрационного уд.
        if (response.Project.RegDate != null) {
            response.Project.RegDate = new Date(response.Project.RegDate.match(/\d+/)[0] * 1);
        }


        $scope.object = response;
        $scope.defaultPrice = response.Price;
        $scope.addPrice();
        if (response.Project.RegNumber != null) {
            $scope.isEnableDownload = true;
            $scope.isProjectSaved = true;
            $scope.isShowFindTab = false;
            $("#producerTabLink").addClass("active");
            $("#tab-1").addClass("active");
            $("#findTabLink").removeClass("active");
            $("#tab-0").removeClass("active");
        }
    });

    loadDictionary($scope, "Country", $http, true);
    //loadDictionary($scope, 'RePrice', $http);
    loadDictionaryByOrderYear($scope, "Currency", $http);
    loadDictionary($scope, "IntroducingMethod", $http);
    loadDictionary($scope, "LsType", $http);
    loadDictionary($scope, "RefPriceType", $http);

}

function rePriceImnNewGrid($scope, DTColumnBuilder, $http, $uibModal) {
    $scope.isProjectSaved = false;
    $scope.isShowFindTab = true;

    $(document).ready(function () {
        commonInit();
    });

    $scope.dtColumns1 = [
         DTColumnBuilder.newColumn("PartsName", "Наименование").withOption('name', 'Name'),
         DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withClass("dtc_manufacturerPrice"),
         DTColumnBuilder.newColumn("CipPrice", "CIP цена").withClass("dtc_cipPrice"),
         DTColumnBuilder.newColumn("RefPriceTypeName", "Тип референтной цены"),
         DTColumnBuilder.newColumn("RefPrice", "Референтная цена").withClass("dtc_refPrice"),
         DTColumnBuilder.newColumn("UnitPrice", "Цена заявителя").withClass("dtc_unitPrice")
    ];

    $scope.dtColumns2 = [
        DTColumnBuilder.newColumn("CountryName", "Страна").withOption("name", "CountryName"),
        DTColumnBuilder.newColumn("ManufacturerPrice", "Цена производителя").withOption("name", "ManufacturerPrice"),
        DTColumnBuilder.newColumn("ManufacturerPriceNote", "Ссылка на источник информации")
        .withOption("name", "ManufacturerPriceNote"),
    ];
    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения"),
        DTColumnBuilder.newColumn("name", "Торговое название"),
        DTColumnBuilder.newColumn("expiration_date", "Дата истечение рег.").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("C_producer_name", "Производитель"),
        DTColumnBuilder.newColumn("C_country_name", "Страна")
    ];

    $scope.dtColumnsCor = [
        DTColumnBuilder.newColumn("Number", "Номер").withOption("name", "Number"),
        DTColumnBuilder.newColumn("DocumentDate", "Дата").withOption("name", "Number").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("Summary", "Краткое содержание").withOption("name", "Number"),
    ];

    $scope.emailFormat = emailFormat;

    var id = $("#projectId").val();
    $scope.object = {};

    $scope.editProject = function (withAlert) {
        if (!$scope.IsCompleteness && $scope.isEnableDownload) {
            $http({
                url: "/Project/PriceImnSaveNotCompleteness",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                if (withAlert) {
                    customAlert("Данные сохранены");
                }
                $scope.isEnableDownload = true;
            });
        } else {
            $http({
                url: "/Project/PriceImnSave",
                method: "POST",
                data: JSON.stringify($scope.object)
            }).success(function (response) {
                if (withAlert) {
                    customAlert("Данные сохранены");
                }
                $scope.isEnableDownload = true;
            });
        }
    };
    var dateNow = new Date();
    //dateNow.setMonth(dateNow.getMonth() + 6);
    dateNow.setDate(dateNow.getDate() + 1);
    $scope.testDate = new Date();
    //DatePicker
    $scope.datePicker = {
        dateStartStatus: { opened: false },
        dateStartOpen: function ($event) {
            $scope.datePicker.dateStartStatus.opened = true;
        },
        dateStartOptions: {
            maxDate: new Date()
        },
        dateEndStatus: {
            opened: false
        },
        dateEndOpen: function ($event) {
            $scope.datePicker.dateEndStatus.opened = true;
        },
        dateEndOptions: {
            minDate: dateNow
        }
    };

    $scope.BoolDic = [
        {
            Id: true,
            Name: "Да"
        }, {
            Id: false,
            Name: "Нет"
        }
    ];


    $scope.sendProject = function () {
        $http({
            url: "/Price/SendProjectPrice",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            customAlert("Ок");
        });
    };
    $scope.view = function (id) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Upload/FileView?id=" + id,
            controller: ModalregisterInstanceCtrl
        });
    };
    $scope.signPrice = function (id) {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
            $http({
                method: "GET",
                url: "/Upload/CheckAttachList",
                data: "JSON",
                params: { id: $scope.object.Project.Id, type: "sysAttachPriceDict" }
            }).success(function (result) {
                if (result == "True") {
                    if (!$scope.IsCompleteness) {
                        $http({
                            url: "/Project/PriceImnSaveNotCompleteness",
                            method: "POST",
                            data: JSON.stringify($scope.object)
                        }).success(function (response) {
                            startSign('/Price/SignOperation', id);
                        });
                    } else {
                        $http({
                            url: "/Project/PriceImnSave",
                            method: "POST",
                            data: JSON.stringify($scope.object)
                        }).success(function (response) {
                            startSign('/Price/SignOperation', id);
                        });
                    }
                } else {
                    customWarning("Требуются вложения:<br/> «Доверенность или копия» и «Сопроводительная письмо» ");
                }
            });
        } else {
            customWarning("Заполните обязательные поля");
        }
    };
    $scope.sendAgree = function () {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
            if (!$scope.IsCompleteness) {
                $http({
                    url: "/Project/PriceImnSaveNotCompleteness",
                    method: "POST",
                    data: JSON.stringify($scope.object)
                }).success(function (response) {
                    IsLoadedDoc($http, $scope, $uibModal);
                });
            } else {
                $http({
                    url: "/Project/PriceImnSave",
                    method: "POST",
                    data: JSON.stringify($scope.object)
                }).success(function (response) {
                    IsLoadedDoc($http, $scope, $uibModal);
                });
            }
        } else {
            customWarning("Заполните обязательные поля");
        }
    };

    $scope.cbNoDataChange = function (cbVal) {
        if (cbVal) {
            return "-";
        } else {
            return "";
        }
    };

    $scope.SetObjectReg = function () {
        $http({
            method: "GET",
            url: "/Dictionaries/GetParts",
            data: "JSON",
            params: { id: $scope.curentReg.id }
        }).success(function (result) {
            $scope.Parts = result;
        });

        $scope.object.ManufacturerOrganization.NameKz = $scope.curentReg.C_producer_name_kz;
        $scope.object.ManufacturerOrganization.NameRu = $scope.curentReg.C_producer_name;
        $scope.object.ManufacturerOrganization.NameEn = $scope.curentReg.C_producer_name_en;

        $scope.object.HolderOrganization.NameKz = $scope.curentReg.owner_name_kz;
        $scope.object.HolderOrganization.NameRu = $scope.curentReg.owner_name_ru;
        $scope.object.HolderOrganization.NameEn = $scope.curentReg.owner_name_en;
        $scope.hoNameKz = $scope.object.HolderOrganization.NameKz;
        $scope.hoNameRu = $scope.object.HolderOrganization.NameRu;
        $scope.hoNameEn = $scope.object.HolderOrganization.NameEn;


        $scope.object.Project.NameKz = $scope.curentReg.name_kz;
        $scope.object.Project.NameRu = $scope.curentReg.name;
        $scope.object.Project.RegNumber = $scope.curentReg.reg_number;
        // для даты регистрационного уд.
        $scope.object.Project.RegDate = new Date($scope.curentReg.reg_date.match(/\d+/)[0] * 1);
        $scope.object.Project.MnnRu = $scope.curentReg.C_int_name;
        $scope.object.Project.FormNameRu = $scope.curentReg.C_dosage_form_name;
        $scope.object.Project.FormNameKz = $scope.curentReg.C_dosage_form_name_kz;
        $scope.object.Project.Dosage = $scope.curentReg.dosage_value;
        $scope.object.Project.CountPackage = $scope.curentReg.box_count;
        $scope.object.Project.Concentration = $scope.curentReg.concentration;
        $scope.object.Project.CodeAtx = $scope.curentReg.C_atc_code;
        $scope.object.Project.Volume = $scope.curentReg.volume;
        $scope.object.ManufacturerOrganization.CountryDicId = $scope.curentReg.C_country_Id;
        $scope.object.HolderOrganization.CountryDicId = $scope.curentReg.owner_country_id;

        $scope.object.Project.RegisterId = $scope.curentReg.id;
        $scope.object.Project.RegisterDfId = $scope.curentReg.df_id;

        $scope.object.CurentPrice.UnitPriceCurrencyDicId = $scope.Currency[0].Id;

        $scope.object.Project.ImnSecuryTypeDicId = $scope.curentReg.degree_risk_id;

        $scope.object.Project.Id = id;

        $scope.isEnableDownload = false;
        $scope.editProject(false);
        $scope.isProjectSaved = true;
        $scope.IsCompleteness = false;
        customAlert('Данные заполнены');
    }
    $scope.curentReg = {};

    $scope.selectGridIntegration = function (data) {
        console.log(data);
        $scope.curentReg = data;

    };
    $scope.selectGridPrice = function (data) {
        console.log(data);
        $scope.object.Price = data;
    };
    $scope.selectGridCurrentPrice = function (data) {
        if (data.Id == '11111111-1111-1111-1111-111111111111') {
            return;
        }
        console.log(data);
        $scope.object.CurentPrice = data;
    };
    $scope.addPrice = function () {
        $scope.object.Price = angular.copy($scope.defaultPrice);
    };
    $scope.addCurrentPrice = function () {
        $scope.object.CurentPrice = angular.copy($scope.defaultCurrentPrice);
    };
    $scope.deletePrice = function () {
        $http({
            url: "/Price/PriceDelete",
            method: "POST",
            data: JSON.stringify($scope.object.Price)
        }).success(function (response) {
            $scope.reloadGridPrice();
            $scope.addPrice();
        });
    };
    $scope.deleteCurrentPrice = function () {
        $http({
            url: "/Price/PriceDelete",
            method: "POST",
            data: JSON.stringify($scope.object.CurentPrice)
        }).success(function (response) {
            $scope.reloadGridCurrentPrice();
            $scope.addCurrentPrice();
        });
    };
    $scope.savePrice = function () {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
            $http({
                url: "/Price/PriceSave",
                method: "POST",
                data: JSON.stringify($scope.object.Price)
            }).success(function (response) {
                $scope.addPrice();
                $scope.reloadGridPrice();
                customAlert("Ок");
            });
        } else {
            customWarning("Заполните поля!");
        }
    };
    $scope.saveCurrentPrice = function () {
        highlightNotValidTabs($scope.rePriceImnForm);
        if ($scope.rePriceImnForm.$valid) {
        $http({
            url: "/Price/DeleteNotCompletenessPrices",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (response) {
            $http({
                url: "/Price/PriceSave",
                method: "POST",
                data: JSON.stringify($scope.object.CurentPrice)
            }).success(function (response) {
                $scope.addCurrentPrice();
                $scope.reloadGridCurrentPrice();
                customAlert("Ок");
            });
        });
        } else {
            customWarning("Заполните поля!");
        }
    };
    $http({
        method: "GET",
        url: "/Project/LoadRePriceImn?id=" + id,
        data: "JSON"
    }).success(function (response) {
        response.Project.DoverennostCreatedDate = new Date(response.StartDateYear,
            response.StartDateMonth - 1,
            response.StartDateDay);
        response.Project.DoverennostExpiryDate = new Date(response.EndDateYear,
            response.EndDateMonth - 1,
            response.EndDateDay);
        // для даты регистрационного уд.
        if (response.Project.RegDate != null) {
            response.Project.RegDate = new Date(response.Project.RegDate.match(/\d+/)[0] * 1);
        }
        $scope.object = response;

        $scope.defaultPrice = response.Price;
        $scope.defaultCurrentPrice = response.CurentPrice;
        $scope.addPrice();
        $scope.addCurrentPrice();
        loadDictionaryParts($scope, $http);

        if (response.Project.RegNumber != null) {
            $scope.isEnableDownload = true;
            $scope.isProjectSaved = true;
            $scope.isShowFindTab = false;
            $("#producerTabLink").addClass("active");
            $("#tab-1").addClass("active");
            $("#findTabLink").removeClass("active");
            $("#tab-0").removeClass("active");
        }

        $http({
            method: "GET",
            url: "/Dictionaries/GetDicIdByCode",
            data: "JSON",
            params: { dicType: "RePrice", code: "2" }
        }).success(function (result) {
            $scope.object.Project.RePriceDicId = result;
        });
    });

    loadDictionary($scope, "Country", $http, true);
    loadDictionary($scope, "ImnSecuryType", $http);
    loadDictionaryByOrderYear($scope, "Currency", $http);
    loadDictionary($scope, "IntroducingMethod", $http);
    loadDictionary($scope, "LsType", $http);
    loadDictionary($scope, "RefPriceType", $http);

}

function priceReject($scope, DTColumnBuilder, $http, $uibModal) {
    loadDictionary($scope, "RejectReason", $http);


    $(document).ready(function () {
        commonInit();
    });

    $scope.cbNoDataChange = function (cbVal) {
        if (cbVal) {
            return "-";
        } else {
            return "";
        }
    };

    $scope.dtColumns10 = [
        DTColumnBuilder.newColumn("reg_number", "Номер регистрационного удостоверения").withClass("dtc_reg_number"),
        DTColumnBuilder.newColumn("reg_date", "Дата регистрационного удостоверения").renderWith(dateformatHtml).withClass("dtc_reg_date"),
        DTColumnBuilder.newColumn("C_int_name", "МНН").withClass("dtc_int_name"),
        DTColumnBuilder.newColumn("SubstanceName", "Состав").withClass("dtc_SubstanceName"),
        DTColumnBuilder.newColumn("name", "Торговое название").withClass("dtc_name"),
        DTColumnBuilder.newColumn("C_dosage_form_name", "Лекарственная форма").withClass("dtc_dosage_form_name"),
        DTColumnBuilder.newColumn("concentration", "Концентрация").withClass("dtc_concentration"),
        DTColumnBuilder.newColumn("dosage_value", "Дозировка (мг)").withClass("dtc_dosage_value"),
        DTColumnBuilder.newColumn("um", "Способ введения").withClass("dtc_um"),
        DTColumnBuilder.newColumn("box_name1", "Первичная упаковка").withClass("dtc_box_name1"),
        DTColumnBuilder.newColumn("box_name2", "Вторичная упаковка").withClass("dtc_box_name2"),
        DTColumnBuilder.newColumn("box_count", "Количество во вторичной упаковке").withClass("dtc_box_count"),
        DTColumnBuilder.newColumn("volume", "Объем").withClass("dtc_volume"),
        DTColumnBuilder.newColumn("expiration_date", "Дата истечение рег.").renderWith(dateformatHtml).withClass("dtc_expiration_date"),
        DTColumnBuilder.newColumn("C_producer_name", "Производитель").withClass("dtc_producer_name"),
        DTColumnBuilder.newColumn("C_country_name", "Страна").withClass("dtc_country_name"),
        DTColumnBuilder.newColumn("description", "Признак ЛС").withClass("dtc_description"),
        DTColumnBuilder.newColumn("C_atc_code", "АТХ код").withClass("dtc_atc_code")
    ];

    $scope.dtColumns10Full = angular.copy($scope.dtColumns10);
    $scope.dtColumns10Full = $scope.dtColumns10Full.map(function (obj) {
        var newObj = obj;
        newObj["columnVisible"] = true;
        return newObj;
    });

    Array.prototype.getIndexOfObject = function (prop, value) {
        for (var i = 0; i < this.length ; i++) {
            if (this[i][prop] === value) {
                return i;
            }
        }
    }

    $scope.cbChange = function (val, cl) {
        if (val) {
            var insertedInd = $scope.dtColumns10Full.getIndexOfObject("sClass", cl);
            $scope.dtColumns10.splice(insertedInd, 0, $scope.dtColumns10Full[insertedInd]);
            return true;
        } else {
            if ($scope.dtColumns10.length > 1) {
                var ind = $scope.dtColumns10.getIndexOfObject("sClass", cl);
                $scope.dtColumns10.splice(ind, 1);
                return false;
            } else {
                return true;
            }
        }
    };

    var id = $("#projectId").val();
    $scope.object = {
        project: {},
        HolderOrganization: {},
        ProxyOrganization: {},
        CurentPrice: {},
        Price: {}
    };

    $scope.view = function () {
        var modalInstance = $uibModal.open({
            templateUrl: "/Upload/DocumentAttachView?id=" + $scope.object.Project.PriceProjectId,
            controller: ModalregisterInstanceCtrl
        });
    };

    $scope.download = function () {
        window.location.href = "/Upload/DocumentAttachDownload?documentId=" + $scope.object.Project.PriceProjectId;
    };

    $scope.SetObjectReg = function () {

        if ($scope.object.rejectReason == null || $scope.object.rejectReason == '') {
            alert('Необходимо указать причину отказа');
            //ShowAlert('Внимание', 'Необходимо указать причину отказа', window.AlertType.Error, 1000);
            return;
        }

        if ($scope.curentReg == null || $scope.curentReg.id == null) {
            alert('Необходимо выбрать запись с таблицы');
            return;
        }

        $scope.object.Project = {};

        //Fields for Template
        $scope.object.Project.NameKz = $scope.curentReg.name_kz;
        $scope.object.Project.NameRu = $scope.curentReg.name;
        $scope.object.Project.RegNumber = $scope.curentReg.reg_number;
        $scope.object.Project.RegDate = $scope.curentReg.reg_date;
        $scope.object.Project.MnnRu = $scope.curentReg.C_int_name;
        $scope.object.Project.FormNameRu = $scope.curentReg.C_dosage_form_name;
        $scope.object.Project.FormNameKz = $scope.curentReg.C_dosage_form_name_kz;
        $scope.object.Project.Dosage = $scope.curentReg.dosage_value;
        $scope.object.Project.CountPackage = $scope.curentReg.box_count;
        $scope.object.Project.Concentration = $scope.curentReg.concentration;
        $scope.object.Project.CodeAtx = $scope.curentReg.C_atc_code;
        $scope.object.Project.Volume = $scope.curentReg.volume;
        $scope.object.Project.IntroducingMethodDicId = $scope.curentReg.umId;

        //data
        $scope.object.Project.RegisterId = $scope.curentReg.id;
        $scope.object.Project.RegisterDfId = $scope.curentReg.df_id;
        $scope.object.Project.ReasonDicId = $scope.object.rejectReason;
        $scope.object.Project.Type = 4;


        $http({
            url: "/Project/CreateRejectDocument",
            method: "POST",
            data: JSON.stringify($scope.object)
        }).success(function (rejectId) {
            $scope.object.Project.PriceProjectId = rejectId;
            alert("Документ сформирован");
            $scope.isEnableDownload = true;
        });

    };
  
    $scope.sendAgree = function () {
        var modalInstance = $uibModal.open({
            templateUrl: "/Project/Agreement",
            controller: ModalSendPrice,
            scope: $scope
        });
    };

    $scope.curentReg = {};

    $scope.selectGridIntegration = function (data) {
        console.log(data);
        $scope.curentReg = data;

    };

    //$http({
    //    method: "GET",
    //    url: "/Project/LoadPriceLs?id=" + id,
    //    data: "JSON"
    //}).success(function (response) {
    //    response.Project.DoverennostCreatedDate = new Date(response.StartDateYear,
    //        response.StartDateMonth - 1,
    //        response.StartDateDay);
    //    response.Project.DoverennostExpiryDate = new Date(response.EndDateYear,
    //        response.EndDateMonth - 1,
    //        response.EndDateDay);
    //    $scope.object = response;
    //});

}

function cleanObject($scope) {
    $scope.object.ManufacturerOrganization.NameKz = "";
    $scope.object.ManufacturerOrganization.NameRu = "";
    $scope.object.ManufacturerOrganization.NameEn = "";
    $scope.object.HolderOrganization.NameKz = "";
    $scope.object.HolderOrganization.NameRu = "";
    $scope.object.HolderOrganization.NameEn = "";
    $scope.object.Project.NameKz = "";
    $scope.object.Project.NameRu = "";
    $scope.object.Project.RegNumber = "";
    $scope.object.Project.RegDate = "";
    $scope.object.Project.MnnRu = "";
    $scope.object.Project.FormNameRu = "";
    $scope.object.Project.FormNameKz = "";
    $scope.object.Project.Dosage = "";
    $scope.object.Project.CountPackage = "";
    $scope.object.Project.Concentration = "";
    $scope.object.Project.CodeAtx = "";
    $scope.object.Project.Volume = "";
    $scope.object.ManufacturerOrganization.CountryDicId = "";
    $scope.object.HolderOrganization.CountryDicId = "";
    $scope.object.ProxyOrganization.CountryDicId = "";
    $scope.object.Project.IntroducingMethodDicId = "";
}
function actionsPriceHtmlAction(data, type, full, meta) {
    if (full.Status ===0) {
        return '<span class="glyphicon glyphicon-trash" title="Удалить" onclick="removePrice(this)" modelId="' + full.Id + '"></span>';
    }
    return "";
}
function projectGrid($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ заявления").withOption("name", "Number")
        .renderWith(actionsPriceLsHtmlAction),
        DTColumnBuilder.newColumn("TypeValue", "Тип").withOption("name", "TypeValue"),
        DTColumnBuilder.newColumn("NameRu", "Наименование ЛС/ИМН в заявление").withOption("name", "NameRu"),
        DTColumnBuilder.newColumn("info", "Доп. информация").withOption("name", "info"),
        DTColumnBuilder.newColumn("CreatedDate", "Дата начала").withOption("name", "CreatedDate").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("OutgoingDate", "Дата регистрации").withOption("name", "OutgoingDate").renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("StausValue", "Текущий статус").withOption("name", "StausValue"),
        DTColumnBuilder.newColumn("Id", "").withOption('name', 'Id').notSortable().renderWith(actionsPriceHtmlAction)
        //DTColumnBuilder.newColumn("IsRegisterProject", "IsRegisterProject").withOption('name', 'IsRegisterProject'),
        //DTColumnBuilder.newColumn("Type", "Type").withOption('name', 'Type'),
    ];
}

function priceReWorkGrid($scope, DTColumnBuilder) {
    $scope.dtColumns = [
        DTColumnBuilder.newColumn("Number", "№ заявления").withOption("name", "Number").renderWith(actionsPriceRework),
        DTColumnBuilder.newColumn("TypeValue", "Тип заявления").withOption("name", "TypeValue"),
        DTColumnBuilder.newColumn("NameRu", "Наименование заявления").withOption("name", "NameRu"),
        DTColumnBuilder.newColumn("CreatedDate", "Дата начала").withOption("name", "CreatedDate")
        .renderWith(dateformatHtml),
        DTColumnBuilder.newColumn("StausValue", "Текущий статус").withOption("name", "StausValue"),
        //DTColumnBuilder.newColumn("IsRegisterProject", "IsRegisterProject").withOption('name', 'IsRegisterProject'),
        //DTColumnBuilder.newColumn("Type", "Type").withOption('name', 'Type'),
    ];
}

angular
    .module("app")
    .controller("projectBaseCtrl", ["$scope", "DTColumnBuilder", projectBaseCtrl])
    .controller("priceLsGrid", ["$scope", "DTColumnBuilder", "$http", "$uibModal", priceLsGrid])
    .controller("priceReject", ["$scope", "DTColumnBuilder", "$http", "$uibModal", priceReject])
    .controller("priceImnGrid", ["$scope", "DTColumnBuilder", "$http", "$uibModal", "$timeout", priceImnGrid])
    .controller("rePriceLsGrid", ["$scope", "DTColumnBuilder", "$http", "$uibModal", rePriceLsGrid])
    .controller("rePriceImnGrid", ["$scope", "DTColumnBuilder", "$http", "$uibModal", rePriceImnGrid])
    .controller("rePriceLsNewGrid", ["$scope", "DTColumnBuilder", "$http", "$uibModal", rePriceLsNewGrid])
    .controller("rePriceImnNewGrid", ["$scope", "DTColumnBuilder", "$http", "$uibModal", rePriceImnNewGrid])
    .controller("projectGrid", ["$scope", "DTColumnBuilder", projectGrid])
    .controller("priceReWorkGrid", ["$scope", "DTColumnBuilder", priceReWorkGrid]);

