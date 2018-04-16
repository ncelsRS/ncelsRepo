var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewEncapsulation, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
var NgxComponent = (function () {
    function NgxComponent() {
        var _this = this;
        this.editing = {};
        this.rows = [];
        this.temp = [];
        this.selected = [];
        this.loadingIndicator = true;
        this.reorderable = true;
        this.columns = [
            { prop: 'name' },
            { name: 'Gender' },
            { name: 'Company' }
        ];
        this.fetch(function (data) {
            _this.temp = data.slice();
            _this.rows = data;
            setTimeout(function () { _this.loadingIndicator = false; }, 1500);
        });
    }
    NgxComponent.prototype.fetch = function (data) {
        var req = new XMLHttpRequest();
        req.open('GET', 'assets/data/company.json');
        req.onload = function () {
            data(JSON.parse(req.response));
        };
        req.send();
    };
    NgxComponent.prototype.updateFilter = function (event) {
        var val = event.target.value.toLowerCase();
        var temp = this.temp.filter(function (d) {
            return d.name.toLowerCase().indexOf(val) !== -1 || !val;
        });
        this.rows = temp;
        this.table.offset = 0;
    };
    NgxComponent.prototype.updateValue = function (event, cell, cellValue, row) {
        this.editing[row.$$index + '-' + cell] = false;
        this.rows[row.$$index][cell] = event.target.value;
    };
    NgxComponent.prototype.onSelect = function (_a) {
        var selected = _a.selected;
        console.log('Select Event', selected, this.selected);
        this.selected.splice(0, this.selected.length);
        (_b = this.selected).push.apply(_b, selected);
        var _b;
    };
    NgxComponent.prototype.onActivate = function (event) {
        console.log('Activate Event', event);
    };
    __decorate([
        ViewChild(DatatableComponent),
        __metadata("design:type", typeof (_a = typeof DatatableComponent !== "undefined" && DatatableComponent) === "function" && _a || Object)
    ], NgxComponent.prototype, "table", void 0);
    NgxComponent = __decorate([
        Component({
            selector: 'app-ngx',
            templateUrl: './ngx.component.html',
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [])
    ], NgxComponent);
    return NgxComponent;
    var _a;
}());
export { NgxComponent };
//# sourceMappingURL=ngx.component.js.map