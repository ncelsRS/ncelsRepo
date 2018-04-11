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
import { MessagesService } from './messages.service';
var MessagesComponent = (function () {
    function MessagesComponent(messagesService) {
        this.messagesService = messagesService;
        this.messages = messagesService.getMessages();
        this.files = messagesService.getFiles();
        this.meetings = messagesService.getMeetings();
    }
    MessagesComponent.prototype.ngOnInit = function () {
        jQuery('#messagesTabs').on('click', '.nav-item a', function () {
            var _this = this;
            setTimeout(function () { return jQuery(_this).closest('.dropdown').addClass('show'); });
        });
    };
    MessagesComponent = __decorate([
        Component({
            selector: 'app-messages',
            templateUrl: './messages.component.html',
            styleUrls: ['./messages.component.scss'],
            encapsulation: ViewEncapsulation.None,
            providers: [MessagesService]
        }),
        __metadata("design:paramtypes", [MessagesService])
    ], MessagesComponent);
    return MessagesComponent;
}());
export { MessagesComponent };
//# sourceMappingURL=messages.component.js.map