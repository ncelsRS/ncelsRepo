var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewEncapsulation } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "../_services/authentication.service";
import { Helpers } from "../../helpers";
var LogoutComponent = (function () {
    function LogoutComponent(_router, _authService) {
        this._router = _router;
        this._authService = _authService;
    }
    LogoutComponent.prototype.ngOnInit = function () {
        Helpers.setLoading(true);
        // reset login status
        this._authService.logout();
        this._router.navigate(['/login']);
    };
    LogoutComponent = __decorate([
        Component({
            selector: 'app-logout',
            templateUrl: './logout.component.html',
            encapsulation: ViewEncapsulation.None,
        }),
        __metadata("design:paramtypes", [Router,
            AuthenticationService])
    ], LogoutComponent);
    return LogoutComponent;
}());
export { LogoutComponent };
//# sourceMappingURL=logout.component.js.map