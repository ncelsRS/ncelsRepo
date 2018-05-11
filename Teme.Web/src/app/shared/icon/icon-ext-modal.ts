import {OnInit, ElementRef, Injectable } from '@angular/core';
import {TemplateValidation} from './TemplateValidation';

@Injectable()
export class IconModal {

  constructor(private elementRefIconModal: ElementRef) {

  }

  showIconModal() {
    jQuery('#iconModal').remove()
    this.elementRefIconModal.nativeElement.insertAdjacentHTML('beforeend', `
<div class="modal fade" id="iconModal" tabindex="-1" role="dialog" aria-labelledby="iconModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="iconModalLabel">Описание</h5>
        <button type="button" class="close" data-dismiss="modal" (click)="removeIcon()" aria-label="Close">
          <span aria-hidden="true" (click)="removeIcon()">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div id="content" style="height: 200px">
          <ul class="nav nav-tabs">
            <li class="nav-item">
              <a class="nav-link active" data-target="#f1tab" data-toggle="tab">Комментарии</a>
            </li>
          </ul>
          <div class="tab-content">
            <div class="tab-pane active" id="f1tab">
              <div class="aleralert alert-info" role="alert" style="text-align:left; color:#0c5460; background-color:#d1ecf1; border-color:#bee5eb;">
                <p>вфаыаыаыаыаы </p>
                <p><b>Значение поля:</b> ТОО Витаминка</p>
                <p style="text-align: right;font-size: 12px"><b>Дата регистрации:</b> 20.04.2018 11:49 </p>

              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
`);
    jQuery('#iconModal').modal();
  }
}
