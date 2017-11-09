var MaterialBase = {
    currentRowId: null,
    actionsMaterialListHtmlAction : function(data, type, full, meta) {
        return '<a  class="pw-task-link" href="/Material/ViewDetailMaterial?id=' + full.Id + '" >' + data + '</a>';
    },
    materialGridController: function ($scope, DTColumnBuilder, DTOptionsBuilder, $http) {
        $scope.dtColumns = [
            DTColumnBuilder.newColumn("IsAdditionalStr", "Донос").withOption('name', 'IsAdditionalStr'),
            DTColumnBuilder.newColumn("RegistrationDate", "Дата").withOption('name', 'RegistrationDate')
            .renderWith(dateformatHtml),
            DTColumnBuilder.newColumn("TypeName", "Тип").withOption('name', 'TypeName'),
            DTColumnBuilder.newColumn("Name", "Наименование").withOption('name', 'Name').renderWith(MaterialBase.actionsMaterialListHtmlAction),
            DTColumnBuilder.newColumn("Quantity", "Кол-во").withOption('name', 'Quantity'),
            DTColumnBuilder.newColumn("UnitName", "Единица измерения").withOption('name', 'UnitName'),
            DTColumnBuilder.newColumn("Batch", "Серия/Партия").withOption('name', 'Batch'),
            DTColumnBuilder.newColumn("DateOfManufacture", "Дата изготовления").withOption('name', 'DateOfManufacture').renderWith(dateformatHtml),
            DTColumnBuilder.newColumn("IsCertificatePassportStr", "Сертификат/ паспорт").withOption('name', 'IsCertificatePassportStr'),
            DTColumnBuilder.newColumn("StorageConditionName", "Условия хранения").withOption('name', 'StorageConditionName'),
            DTColumnBuilder.newColumn("StorageTemperatureStr", "Температура с ... по ...").withOption('name', 'StorageTemperatureStr'),
            DTColumnBuilder.newColumn("Id", "Id").withOption('name', 'Id').withOption('class', 'ng-hide')
        ];

       // var ddId = $("#modelId").val();

       // debugger;

       // $scope.dtOptions = DTOptionsBuilder.newOptions()
       //     .withOption('fnServerParams',
       //         function (aoData) {
       //             debugger;
       //             aoData.push({ "name": "DrugDeclaratioinId", "value": ddId });
       //         });

//        DTOptionsBuilder.fromSource()
//            .withOption('fnServerParams',
//                function(aoData) {
//                    aoData.push({ "name": "DrugDeclaratioinId", "value": ddId });
//                });
        
        $scope.localCurrentRowId = null;
        
        $('#entry-grid').on('click', 'tr',
           function () {
               if (this.childNodes.length > 1) {
                   MaterialBase.currentRowId = this.childNodes[this.childNodes.length - 1].textContent;
                   $scope.$apply(function () {
                       $scope.localCurrentRowId = MaterialBase.currentRowId;
                   });
               }
           });
        
        // edit
        $scope.edit = function () {
            location.href = '/Material/EditMaterial?id=' + MaterialBase.currentRowId;
        }

        // remove
        $scope.remove = function () {
            $http({
                url: '/Material/RemoveMaterial?id=' + MaterialBase.currentRowId,
                method: 'POST'
            }).success(function (response) {
              
            });
        }
    },
    materialFormController: function ($scope, $http, $uibModal) {
        // patterns

        $scope.decimal2Pattern = "-?[0-9]+(\.[0-9]{1,2})?";

        // variables
        $scope.object = {
            Material: {
                MaterialType: null, //{
                //Code: "mpsample",
                //Id: "005d6a89-7a46-40ed-bc82-4fdbac31be7d",
                //Name: "Образец ЛС",
                //NameKz: "Образец ЛС"
                //},
                Name: null,
                DrugFormId: null,
                Dosage: null,
                DosageUnitId: null,
                IsCertificatePassport: false,
                IsAdditional: false,
                IsContainNPP: false,
                Country: null,
                Producer: null,
                ProducerId: null,
                DrugDeclarationId: $('#drugdeclarationId').val()
            }
        };
        
        $scope.manufacturesSource = {
            selected: null,
            page: 1,
            items: [],
            lastPage: 2,
            loading: false
        };
        

        //DatePicker
        $scope.datePicker = {
            dateOfManufactureStatus: { opened: false },
            dateOfManufactureOpen: function ($event) {
                $scope.datePicker.dateOfManufactureStatus.opened = true;
            },
            dateOfManufactureOptions: {
                maxDate: new Date()
            },
            expirationDateStatus: { opened: false },
            expirationDateOpen: function ($event) {
                $scope.datePicker.expirationDateStatus.opened = true;
            },
            expirationDateOptions: {
                // minDate: dateNow
            },
            retestDateStatus: { opened: false },
            retestDateStatusOpen: function ($event) {
                $scope.datePicker.retestDateStatus.opened = true;
            },
            retestDateStatusOptions: {
                // minDate: dateNow
            }
        };


        // funtions
        $scope.setSelectedCode = function (list, name, id) {
            angular.forEach($scope[list], function (value, key) {
                if (value.Id === id) {
                    $scope[name] = value.Code;
                }
            });
        }

        $scope.setContryByManufacture = function (id) {
            angular.forEach($scope['Country'], function (value, key) {
                if (value.Id === id) {
                    $scope.object.Material.Country = value;
                }
            });
        }

        $scope.fetchOrganizations = function ($select, sourceName, $event) {
            if (!$event) {
                $scope[sourceName].page = 1;
                $scope[sourceName].items = [];
                $scope[sourceName].lastPage = 1;
            } else {
                $event.stopPropagation();
                $event.preventDefault();
                $scope[sourceName].page++;
            }
            if ($select.search === "") return;
            $scope[sourceName].loading = true;

            $http({
                method: 'GET',
                url: '/Dictionaries/Organizations',
                params: {
                    search: $select.search,
                    page: $scope[sourceName].page
                }
            }).then(function (resp) {
                $scope[sourceName].items = $scope[sourceName].items.concat(resp.data.items);
                $scope[sourceName].lastPage = resp.data.lastPage;
            })['finally'](function () {
                $scope[sourceName].loading = false;
            });
        }

        $scope.fetchOrganization = function (selectedItem) {

            $scope.object.Material.ProducerId = selectedItem.Id;
            $scope.object.Material.Producer = selectedItem;

            $http({
                method: 'GET',
                url: '/Dictionaries/GetOrganization',
                params: {
                    id: selectedItem.Id
                }
            }).then(function (resp) {
                if (resp.data) {
                    $scope.setContryByManufacture(resp.data.CountryDicId);
                }
            });
        }
        
        $scope.saveMaterial = function () {
            debugger;
            if ($scope.materialForm.$valid) {
                $http({
                    url: '/Material/SaveMaterial',
                    method: 'POST',
                    data: JSON.stringify($scope.object)
                }).success(function (response) {
                    location.href = '/DrugDeclaration/Edit?id=' + $('#drugdeclarationId').val();
                });
            } else {
                angular.forEach($scope.materialForm.$error.required, function (field) {
                    field.$setDirty();
                    field.$setTouched();
                });
            }
        }
        

        // load Dictionaries
        loadDictionary($scope, 'MaterialRdType', $http);
        loadDictionary($scope, 'MeasureType', $http);
        loadDictionary($scope, 'Country', $http);
        loadDictionary($scope, 'StorageCondition', $http);

        var modelId = $("#modelId").val();
        if (modelId) {
            $http({
                url: '/Material/LoadMaterial?id=' + modelId,
                method: 'POST'
            }).success(function(response) {
                $scope.object.Material = response;
                
                if ($scope.object.Material.DateOfManufacture)
                    $scope.object.Material.DateOfManufacture = new Date($scope.object.Material.DateOfManufacture);
                if ($scope.object.Material.ExpirationDate)
                    $scope.object.Material.ExpirationDate = new Date($scope.object.Material.ExpirationDate);
                if ($scope.object.Material.RetestDate)
                    $scope.object.Material.RetestDate = new Date($scope.object.Material.RetestDate);
                $scope.manufacturesSource.selected = response.Producer;
            });
        } else {
            $http({
                method: 'GET',
                url: '/Material/GetMaterialType?code=' + 'mpsample',
                data: 'JSON'
            }).success(function (response) {
                $scope.object.Material.MaterialType = response;
            });
        }
    }
}

angular
    .module('app')
    .controller('MaterialBase.materialGridController', ['$scope', 'DTColumnBuilder', 'DTOptionsBuilder', '$http', MaterialBase.materialGridController])
    .controller('MaterialBase.materialFormController', ['$scope', '$http', '$uibModal', MaterialBase.materialFormController]);