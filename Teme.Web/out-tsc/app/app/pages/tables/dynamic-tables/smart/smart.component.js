var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewEncapsulation } from '@angular/core';
var SmartComponent = (function () {
    function SmartComponent() {
        var _this = this;
        this.data = [];
        this.settings = {
            selectMode: 'single',
            hideHeader: false,
            hideSubHeader: false,
            actions: {
                columnTitle: 'Actions',
                add: true,
                edit: true,
                delete: true,
                custom: [],
                position: 'right' // left|right
            },
            add: {
                addButtonContent: '<h4 class="mb-1"><i class="fa fa-plus ml-3 text-success"></i></h4>',
                createButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
                cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
            },
            edit: {
                editButtonContent: '<i class="fa fa-pencil mr-3 text-primary"></i>',
                saveButtonContent: '<i class="fa fa-check mr-3 text-success"></i>',
                cancelButtonContent: '<i class="fa fa-times text-danger"></i>'
            },
            delete: {
                deleteButtonContent: '<i class="fa fa-trash-o text-danger"></i>',
                confirmDelete: true
            },
            noDataMessage: 'No data found',
            columns: {
                id: {
                    title: 'ID',
                    editable: false,
                    width: '60px',
                    type: 'html',
                    valuePrepareFunction: function (value) { return '<div class="text-center">' + value + '</div>'; }
                },
                firstName: {
                    title: 'First Name',
                    type: 'string',
                    filter: true
                },
                lastName: {
                    title: 'Last Name',
                    type: 'string'
                },
                username: {
                    title: 'Username',
                    type: 'string'
                },
                email: {
                    title: 'E-mail',
                    type: 'string'
                },
                age: {
                    title: 'Age',
                    type: 'number'
                }
            },
            pager: {
                display: true,
                perPage: 10
            }
        };
        this.getData(function (data) {
            _this.data = data;
        });
    }
    SmartComponent.prototype.getData = function (data) {
        var req = new XMLHttpRequest();
        req.open('GET', 'assets/data/users.json');
        req.onload = function () {
            data(JSON.parse(req.response));
        };
        req.send();
    };
    SmartComponent.prototype.onDeleteConfirm = function (event) {
        if (window.confirm('Are you sure you want to delete?')) {
            event.confirm.resolve();
        }
        else {
            event.confirm.reject();
        }
    };
    SmartComponent.prototype.onRowSelect = function (event) {
        // console.log(event);
    };
    SmartComponent.prototype.onUserRowSelect = function (event) {
        //console.log(event);   //this select return only one page rows
    };
    SmartComponent.prototype.onRowHover = function (event) {
        //console.log(event);
    };
    SmartComponent = __decorate([
        Component({
            selector: 'app-smart',
            templateUrl: './smart.component.html',
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [])
    ], SmartComponent);
    return SmartComponent;
}());
export { SmartComponent };
//# sourceMappingURL=smart.component.js.map