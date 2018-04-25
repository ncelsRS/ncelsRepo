import {Injectable} from "@angular/core";
import {IdentityRes} from "./dtos/IdentityRes";
import {HttpHeaders, HttpParams, HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {IdentityDto} from "./dtos/IdentityDto";
import {IdentityUpdateTokenDto} from "./dtos/IdentityUpdateTokenDto";

@Injectable()

export class IdentityProviderSvc {

  constructor(private http: HttpClient) {
    console.log("IdentityProvider created");
  }

  private _auth: IdentityRes = null;

  public getAuth(): IdentityRes {
    if (!this._auth) {
      let authStr = localStorage.getItem("auth");
      if (authStr)
        this._auth = JSON.parse(authStr);
    }
    return this._auth;
  }

  public setAuth(auth: IdentityRes): void {
    if (auth)
      localStorage.setItem("auth", JSON.stringify(auth));
  }

  private getJwtExp(jwt: string): number {
    let jwtArr = jwt.split('.');
    let jwtEnc = atob(jwtArr[1]);
    let data = JSON.parse(jwtEnc);
    return data.exp;
  }

  private isJwtExp(jwt: string): boolean {
    let exp = this.getJwtExp(jwt);
    return exp < new Date().getTime();
  }

  private objToForm(obj: any): HttpParams {
    let params = new HttpParams();
    Object.keys(obj).forEach(key => {
      params = params.set(key, obj[key]);
    });

    return params;
  }

  public checkAuthWithRefresh(): Promise<boolean> {
    return new Promise(resolve => {
      if (!this.getAuth()) return false;
      if (!this.isJwtExp(this._auth.access_token)) return true;
      if (this.isJwtExp(this._auth.refresh_token)) return false;

      return this.postIdentity(new IdentityUpdateTokenDto(this._auth.refresh_token));
    });
  }

  public postIdentity(login: IdentityDto): Promise<boolean> {
    let params = this.objToForm(login);
    return this.http.post<IdentityRes>(environment.urls.identity + '/oauth/token', params,
      {headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')})
      .toPromise()
      .then(res => {
        this.setAuth(res);
        return true;
      });
  }

}
