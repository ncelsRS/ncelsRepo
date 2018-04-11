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
var PrimeNgInputComponent = (function () {
    function PrimeNgInputComponent() {
        this.selectedTypes = ['Apartment', 'Studio'];
        this.selectedCar2 = 'BMW';
        this.selectedCars = [];
        this.color2 = '#1976D2';
        this.types = [];
        this.types.push({ label: 'Apartment', value: 'Apartment' });
        this.types.push({ label: 'House', value: 'House' });
        this.types.push({ label: 'Studio', value: 'Studio' });
        this.cities = [];
        this.cities.push({ label: 'New York', value: { id: 1, name: 'New York', code: 'NY' } });
        this.cities.push({ label: 'Rome', value: { id: 2, name: 'Rome', code: 'RM' } });
        this.cities.push({ label: 'London', value: { id: 3, name: 'London', code: 'LDN' } });
        this.cities.push({ label: 'Istanbul', value: { id: 4, name: 'Istanbul', code: 'IST' } });
        this.cities.push({ label: 'Paris', value: { id: 5, name: 'Paris', code: 'PRS' } });
        this.cars = [];
        this.cars.push({ label: 'Audi', value: 'Audi' });
        this.cars.push({ label: 'BMW', value: 'BMW' });
        this.cars.push({ label: 'Fiat', value: 'Fiat' });
        this.cars.push({ label: 'Ford', value: 'Ford' });
        this.cars.push({ label: 'Honda', value: 'Honda' });
        this.cars.push({ label: 'Jaguar', value: 'Jaguar' });
        this.cars.push({ label: 'Mercedes', value: 'Mercedes' });
        this.cars.push({ label: 'Renault', value: 'Renault' });
        this.cars.push({ label: 'VW', value: 'VW' });
        this.cars.push({ label: 'Volvo', value: 'Volvo' });
    }
    PrimeNgInputComponent.prototype.clear = function () {
        this.selectedType = null;
        this.selectedTypes = [];
    };
    PrimeNgInputComponent.prototype.ngOnInit = function () {
    };
    PrimeNgInputComponent = __decorate([
        Component({
            selector: "app-primeng-input",
            templateUrl: "./primeng-input.component.html",
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [])
    ], PrimeNgInputComponent);
    return PrimeNgInputComponent;
}());
export { PrimeNgInputComponent };
//# sourceMappingURL=primeng-input.component.js.map