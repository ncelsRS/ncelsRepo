var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component, ViewEncapsulation } from '@angular/core';
var ImageUploaderComponent = (function () {
    function ImageUploaderComponent() {
    }
    ImageUploaderComponent.prototype.fileChange = function (input) {
        var _this = this;
        var reader = new FileReader();
        if (input.files.length) {
            var file = input.files[0];
            reader.onload = function () {
                _this.image = reader.result;
            };
            reader.readAsDataURL(file);
        }
    };
    ImageUploaderComponent.prototype.removeImage = function () {
        this.image = '';
    };
    ImageUploaderComponent = __decorate([
        Component({
            selector: 'app-image-uploader',
            encapsulation: ViewEncapsulation.None,
            templateUrl: './image-uploader.component.html',
            styleUrls: ['./image-uploader.component.scss']
        })
    ], ImageUploaderComponent);
    return ImageUploaderComponent;
}());
export { ImageUploaderComponent };
//# sourceMappingURL=image-uploader.component.js.map