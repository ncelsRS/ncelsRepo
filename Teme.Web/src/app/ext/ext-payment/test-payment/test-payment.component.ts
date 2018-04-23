import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import {selector} from 'rxjs/operator/publish';

@Component({
  selector: 'app-test-payment',
  templateUrl: './test-payment.component.html',
  styleUrls: ['./test-payment.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class TestPaymentComponent implements OnInit {

  constructor(private elRef: ElementRef) {  }

  ngOnInit() {
  }
  addIcon() {
    this.elRef.nativeElement.insertAdjacentHTML('beforeend', `
<div class="modal fade" id="iconModal" tabindex="-1" role="dialog" aria-labelledby="iconModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="iconModalLabel">Описание</h5>
        <button type="button" class="close" data-dismiss="modal" (click)="removeIcon()" aria-label="Close">
          <span aria-hidden="true" (click)="removeIcon()">&times;</span>
        </button>
      </div>
      <button type="button" class="btn btn-primary" (click)="removeIcon()">removeIcon</button>
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
  }

  showIcon() {
    jQuery('#iconModal').modal();
  }
  removeIcon() {
    jQuery('#iconModal').remove()
  }

  getid(){

    // var div = document.createElement("div");
    // var att  = document.createAttribute("id");
    // att.value = "democlass";
    // div.setAttributeNode(att);


    //var div = document.documentElement.innerHTML(`<div id="elRef">divtest</div>`);



    var divbekbol = document.getElementById('bekbol');
    //divbekbol.appendChild(div);

    //divbekbol.innerHTML='<div id="ccc">divtest</div>';
    //var tag = this.elRef.nativeElement;
    this.elRef.nativeElement.insertAdjacentHTML( 'beforeend', '<div id="ccc">divtest</div>');
    //console.log(tag);
  }
}
