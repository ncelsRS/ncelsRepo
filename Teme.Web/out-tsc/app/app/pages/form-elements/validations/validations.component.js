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
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
var ValidationsComponent = (function () {
    function ValidationsComponent(formBuilder) {
        this.formBuilder = formBuilder;
    }
    ValidationsComponent.prototype.ngOnInit = function () {
        var password = new FormControl('', Validators.required);
        var certainPassword = new FormControl('', CustomValidators.equalTo(password));
        var first = new FormControl('', Validators.required);
        var second = new FormControl('', CustomValidators.notEqualTo(first));
        this.form = this.formBuilder.group({
            required: ['', Validators.required],
            minLength: ['', Validators.compose([Validators.required, CustomValidators.min(3)])],
            maxLength: ['', Validators.compose([Validators.required, Validators.maxLength(10)])],
            base64: ['', CustomValidators.base64],
            creditCard: ['', CustomValidators.creditCard],
            date: ['', CustomValidators.date],
            dateISO: ['', CustomValidators.dateISO],
            maxDate: ['', CustomValidators.maxDate('2017-09-09')],
            minDate: ['', CustomValidators.minDate('2017-09-09')],
            digits: ['', CustomValidators.digits],
            email: ['', CustomValidators.email],
            equal: ['', CustomValidators.equal('5')],
            notEqual: ['', CustomValidators.notEqual('10')],
            password: password,
            certainPassword: certainPassword,
            first: first,
            second: second,
            greaterThan: ['', CustomValidators.gt(10)],
            greaterThanEqual: ['', CustomValidators.gte(15)],
            lessThan: ['', CustomValidators.lt(10)],
            lessThanEqual: ['', CustomValidators.lte(10)],
            max: ['', CustomValidators.max(10)],
            min: ['', CustomValidators.min(10)],
            number: ['', CustomValidators.number],
            phone: ['', CustomValidators.phone('US')],
            range: ['', CustomValidators.range([10, 20])],
            rangeLength: ['', CustomValidators.rangeLength([5, 9])],
            url: ['', CustomValidators.url]
        });
    };
    ValidationsComponent.prototype.submitForm = function (value) {
        console.log(value);
    };
    ValidationsComponent = __decorate([
        Component({
            selector: 'app-validations',
            templateUrl: './validations.component.html',
            encapsulation: ViewEncapsulation.None
        }),
        __metadata("design:paramtypes", [FormBuilder])
    ], ValidationsComponent);
    return ValidationsComponent;
}());
export { ValidationsComponent };
//# sourceMappingURL=validations.component.js.map