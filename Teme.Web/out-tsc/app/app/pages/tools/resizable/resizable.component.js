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
var ResizableComponent = (function () {
    function ResizableComponent() {
        this.style = {};
    }
    ResizableComponent.prototype.ngOnInit = function () {
    };
    ResizableComponent.prototype.validate = function (event) {
        var MIN_DIMENSIONS_PX = 50;
        if (event.rectangle.width < MIN_DIMENSIONS_PX || event.rectangle.height < MIN_DIMENSIONS_PX) {
            return false;
        }
        return true;
    };
    ResizableComponent.prototype.onResizeEnd = function (event) {
        this.style = {
            position: 'fixed',
            left: event.rectangle.left + "px",
            top: event.rectangle.top + "px",
            width: event.rectangle.width + "px",
            height: event.rectangle.height + "px"
        };
    };
    ResizableComponent = __decorate([
        Component({
            selector: 'app-resizable',
            templateUrl: './resizable.component.html',
            styleUrls: ['./resizable.component.scss'],
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [])
    ], ResizableComponent);
    return ResizableComponent;
}());
export { ResizableComponent };
//# sourceMappingURL=resizable.component.js.map