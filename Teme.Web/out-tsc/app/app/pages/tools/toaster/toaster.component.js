var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, VERSION, ViewEncapsulation } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
var quotes = [
    {
        title: 'Title',
        message: 'Message'
    },
    {
        title: '😃',
        message: 'Supports Emoji'
    },
    {
        title: null,
        message: 'My name is Inigo Montoya. You killed my father. Prepare to die!',
    },
    {
        title: null,
        message: 'Titles are not always needed'
    },
    {
        title: 'Title only 👊',
        message: null,
    },
    {
        title: '',
        message: "Supports Angular " + VERSION.full,
    },
];
var types = ['success', 'error', 'info', 'warning'];
var ToasterComponent = (function () {
    function ToasterComponent(toastrService) {
        this.toastrService = toastrService;
        this.title = '';
        this.type = types[0];
        this.message = '';
        this.version = VERSION;
        this.lastInserted = [];
        this.options = this.toastrService.toastrConfig;
    }
    ToasterComponent.prototype.ngOnInit = function () {
        var _this = this;
        setTimeout(function () {
            _this.toastrService.success('Welcome to toaster page!', 'Toastr fun!');
        });
    };
    ToasterComponent.prototype.openToast = function () {
        var m = this.message;
        var t = this.title;
        if (!this.title.length && !this.message.length) {
            var randomMessage = quotes[Math.floor(Math.random() * quotes.length)];
            m = randomMessage.message;
            t = randomMessage.title;
        }
        var opt = JSON.parse(JSON.stringify(this.options));
        var inserted = this.toastrService[this.type](m, t, opt);
        if (inserted) {
            this.lastInserted.push(inserted.toastId);
        }
        return inserted;
    };
    ToasterComponent.prototype.clearToasts = function () {
        this.toastrService.clear();
    };
    ToasterComponent.prototype.clearLastToast = function () {
        this.toastrService.clear(this.lastInserted.pop());
    };
    ToasterComponent = __decorate([
        Component({
            selector: 'app-toaster',
            templateUrl: './toaster.component.html',
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof ToastrService !== "undefined" && ToastrService) === "function" && _a || Object])
    ], ToasterComponent);
    return ToasterComponent;
    var _a;
}());
export { ToasterComponent };
//# sourceMappingURL=toaster.component.js.map