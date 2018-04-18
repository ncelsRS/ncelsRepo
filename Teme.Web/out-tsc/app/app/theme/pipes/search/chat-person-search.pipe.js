var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Pipe } from '@angular/core';
var ChatPersonSearchPipe = (function () {
    function ChatPersonSearchPipe() {
    }
    ChatPersonSearchPipe.prototype.transform = function (value, args) {
        var searchText = new RegExp(args, 'ig');
        if (value) {
            return value.filter(function (message) {
                if (message.author) {
                    return message.author.search(searchText) !== -1;
                }
            });
        }
    };
    ChatPersonSearchPipe = __decorate([
        Pipe({ name: 'ChatPersonSearchPipe' })
    ], ChatPersonSearchPipe);
    return ChatPersonSearchPipe;
}());
export { ChatPersonSearchPipe };
//# sourceMappingURL=chat-person-search.pipe.js.map