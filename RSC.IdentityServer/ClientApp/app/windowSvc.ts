import { Injectable } from '@angular/core';

function _window(): any {
    // return the native window obj
    return window;
}

@Injectable()
export class WindowSvc {

    get nativeWindow(): any {
        return _window();
    }

}