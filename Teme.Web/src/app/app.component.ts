import {Component, ViewEncapsulation} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AppSettings} from './app.settings';
import {Settings} from './app.settings.model';
import {IdentityProviderSvc} from './shared/identity/IdentityProviderSvc';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {
  public settings: Settings;

  constructor(
    public appSettings: AppSettings,
    private route: ActivatedRoute,
    private identitySvc: IdentityProviderSvc
  ) {
    this.settings = this.appSettings.settings;

    let url = window.location.href;
    let index = url.indexOf('onetime');
    if (index > 0) {
      let onetime = url.substr(index, url.length - 1)
        .split('&')[0]
        .split('=')[1];
      this.identitySvc.setAuthFromOneTime(onetime)
        .subscribe();
    }
    else
      this.identitySvc.checkAuthWithRefresh()
        .subscribe();
  }


  /* These following methods used for theme preview, you can remove this methods */

  // ngOnInit() {
  //     var demo = this.getParameterByName('demo');
  //     this.setLayout(demo);
  // }

  // private getParameterByName(name) {
  //     var url = window.location.href;
  //     name = name.replace(/[\[\]]/g, "\\$&");
  //     var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
  //         results = regex.exec(url);
  //     if (!results) return null;
  //     if (!results[2]) return '';
  //     return decodeURIComponent(results[2].replace(/\+/g, " "));
  // }

  // private setLayout(demo){
  //      switch (demo) {
  //         case "vertical-default":
  //             this.settings.theme.menu = 'vertical';
  //             this.settings.theme.menuType = 'default';
  //             break;
  //         case "vertical-compact":
  //             this.settings.theme.menu = 'vertical';
  //             this.settings.theme.menuType = 'compact';
  //             break;
  //         case "vertical-mini":
  //             this.settings.theme.menu = 'vertical';
  //             this.settings.theme.menuType = 'mini';
  //             break;
  //         case "horizontal-default":
  //             this.settings.theme.menu = 'horizontal';
  //             this.settings.theme.menuType = 'default';
  //             break;
  //         case "horizontal-compact":
  //             this.settings.theme.menu = 'horizontal';
  //             this.settings.theme.menuType = 'compact';
  //             break;
  //         case "horizontal-mini":
  //             this.settings.theme.menu = 'horizontal';
  //             this.settings.theme.menuType = 'mini';
  //             break;
  //         default:
  //             this.settings.theme.menu = 'vertical';
  //             this.settings.theme.menuType = 'default';
  //     }
  //     this.router.navigate(['/']);
  // }

}
