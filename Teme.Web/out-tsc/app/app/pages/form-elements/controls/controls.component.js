var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component, ViewEncapsulation } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
var ControlsComponent = (function () {
    function ControlsComponent() {
        this.firstControlOptions = [
            { id: 1, name: 'Option 1' },
            { id: 2, name: 'Option 2' },
            { id: 3, name: 'Option 3' }
        ];
        this.secondControlSettings = {
            checkedStyle: 'fontawesome',
            buttonClasses: 'btn btn-secondary btn-block',
            dynamicTitleMaxItems: 3,
            displayAllSelectedText: true,
            showCheckAll: true,
            showUncheckAll: true
        };
        this.secondControlTexts = {
            checkAll: 'Select all',
            uncheckAll: 'Unselect all',
            checked: 'item selected',
            checkedPlural: 'items selected',
            searchPlaceholder: 'Find',
            defaultTitle: 'Select countries',
            allSelected: 'All selected',
        };
        this.secondControlOptions = [
            { id: 1, name: 'Sweden' },
            { id: 2, name: 'Norway' },
            { id: 3, name: 'Canada' },
            { id: 4, name: 'USA' }
        ];
        this.thirdControlSettings = {
            enableSearch: true,
            checkedStyle: 'checkboxes',
            buttonClasses: 'btn btn-secondary btn-block',
            dynamicTitleMaxItems: 3,
            displayAllSelectedText: true
        };
        this.thirdControlTexts = {
            checkAll: 'Select all',
            uncheckAll: 'Unselect all',
            checked: 'item selected',
            checkedPlural: 'items selected',
            searchPlaceholder: 'Find...',
            defaultTitle: 'Select countries using search filter',
            allSelected: 'All selected',
        };
        this.thirdControlOptions = [
            { id: 1, name: 'Sweden' },
            { id: 2, name: 'Norway' },
            { id: 3, name: 'Canada' },
            { id: 4, name: 'USA' }
        ];
        //Multiple months
        this.displayMonths = 2;
        this.navigation = 'select';
        //Datepicker in a popup
        this.modelPopup = { year: new Date().getFullYear(), month: new Date().getMonth() + 1, day: new Date().getDate() - 3 };
        //Disabled datepicker
        this.modelDisabled = { year: new Date().getFullYear(), month: new Date().getMonth() + 1, day: new Date().getDate() };
        this.disabled = true;
        //Basic timepicker
        this.time = { hour: 13, minute: 30, second: 20 };
        //Meridian
        this.timeMeridian = { hour: 15, minute: 20, second: 25 };
        this.meridian = true;
        //Seconds
        this.timeSeconds = { hour: 16, minute: 40, second: 30 };
        this.seconds = true;
        //Spinners
        this.timeSpinners = { hour: 13, minute: 30 };
        this.spinners = true;
        //Custom steps
        this.timeCustomSteps = { hour: 13, minute: 30, second: 0 };
        this.hourStep = 1;
        this.minuteStep = 15;
        this.secondStep = 30;
        //Custom validation
        this.timeValidation = { hour: 11, minute: 30 };
        this.ctrl = new FormControl('', function (control) {
            var value = control.value;
            if (!value) {
                return null;
            }
            if (value.hour < 12) {
                return { tooEarly: true };
            }
            if (value.hour > 13) {
                return { tooLate: true };
            }
            return null;
        });
        //Rating
        this.currentRate = 8;
        //Events and readonly ratings
        this.selected = 0;
        this.hovered = 0;
        this.readonly = false;
        //Custom star template
        this.currentRateCustom = 6;
        //Custom decimal rating
        this.currentRateDecimal = 3.14;
        //Form integration
        this.ctrlFormIntegration = new FormControl(null, Validators.required);
    }
    ControlsComponent.prototype.onChange = function () {
        console.log(this.firstControlModel);
    };
    ControlsComponent.prototype.selectToday = function () {
        this.model = { year: new Date().getFullYear(), month: new Date().getMonth() + 1, day: new Date().getDate() };
    };
    ControlsComponent.prototype.isWeekend = function (date) {
        var d = new Date(date.year, date.month - 1, date.day);
        return d.getDay() === 0 || d.getDay() === 6;
    };
    ControlsComponent.prototype.isDisabled = function (date, current) {
        return date.month !== current.month;
    };
    ControlsComponent.prototype.toggle = function () {
        if (this.ctrlFormIntegration.disabled) {
            this.ctrlFormIntegration.enable();
        }
        else {
            this.ctrlFormIntegration.disable();
        }
    };
    ControlsComponent = __decorate([
        Component({
            selector: 'app-controls',
            templateUrl: './controls.component.html',
            styleUrls: ['./controls.component.scss'],
            encapsulation: ViewEncapsulation.None
        })
    ], ControlsComponent);
    return ControlsComponent;
}());
export { ControlsComponent };
//# sourceMappingURL=controls.component.js.map