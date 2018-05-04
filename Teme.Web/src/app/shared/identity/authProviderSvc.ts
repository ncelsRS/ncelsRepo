import {Injectable} from "@angular/core";
import {Auth} from "./Auth";

@Injectable()

export class AuthProviderSvc {
  private static _auth: Auth = null;

  public static getAuth(): Auth {
    if (!this._auth) {
      let authStr = localStorage.getItem("auth");
      if (authStr)
        this._auth = JSON.parse(authStr);
    }
    return this._auth;
  }

  public static setAuth(auth: Auth): void {
    if (auth)
      localStorage.setItem("auth", JSON.stringify(auth));
  }

  public static checkAuth(): boolean {
    if (!this.getAuth()) return false;
    let jwtArr = this._auth.access_token.split('.');
    let jwtEnc = atob(jwtArr[1]);
    let jwt = JSON.parse(jwtEnc);
    return jwt.exp > new Date().getTime();
  }

}
