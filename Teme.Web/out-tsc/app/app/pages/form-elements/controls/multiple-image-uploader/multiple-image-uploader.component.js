var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewEncapsulation, ChangeDetectorRef } from '@angular/core';
var MultipleImageUploaderComponent = (function () {
    function MultipleImageUploaderComponent(changeDetectorRef) {
        this.changeDetectorRef = changeDetectorRef;
        this.images = [];
    }
    MultipleImageUploaderComponent.prototype.fileChange = function (input) {
        this.readFiles(input.files);
    };
    MultipleImageUploaderComponent.prototype.readFile = function (file, reader, callback) {
        reader.onload = function () {
            callback(reader.result);
        };
        reader.readAsDataURL(file);
    };
    MultipleImageUploaderComponent.prototype.readFiles = function (files, index) {
        var _this = this;
        if (index === void 0) { index = 0; }
        var reader = new FileReader();
        if (index in files) {
            this.readFile(files[index], reader, function (result) {
                _this.images.push(result);
                _this.readFiles(files, index + 1);
            });
        }
        else {
            this.changeDetectorRef.detectChanges();
        }
    };
    MultipleImageUploaderComponent.prototype.removeImage = function (index) {
        this.images.splice(index, 1);
    };
    MultipleImageUploaderComponent = __decorate([
        Component({
            selector: 'app-multiple-image-uploader',
            encapsulation: ViewEncapsulation.None,
            templateUrl: './multiple-image-uploader.component.html',
            styleUrls: ['./multiple-image-uploader.component.scss']
        }),
        __metadata("design:paramtypes", [ChangeDetectorRef])
    ], MultipleImageUploaderComponent);
    return MultipleImageUploaderComponent;
}());
export { MultipleImageUploaderComponent };
//# sourceMappingURL=multiple-image-uploader.component.js.map