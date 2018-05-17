import {OnInit, ElementRef, Injectable} from '@angular/core';
import {IconService} from './icon.service';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Icon} from './Icon';
import {IconRecord} from './IconRecord';

@Injectable()
export class IconIntModal {
  iconUrl:string = environment.urls.icon;
  iconData:Icon;
  constructor(private elementRefIconModal: ElementRef, private http: HttpClient) {

  }

  showIconModal() {
    var ModuleType = "1", ObjectId = "1", FieldName="test.field";
    jQuery('#iconIntModal').remove();
    this.http.get( this.iconUrl + 'GetIconRecords',{ params: {ModuleType, ObjectId, FieldName} })
      .subscribe((data:Icon)=> {
        let comment = this.generateComent(data.iconRecords);

        this.appendModal(comment);
      })
  }

  appendModal(comment:string){
    this.elementRefIconModal.nativeElement.insertAdjacentHTML('beforeend', `
<div class="modal fade" id="iconIntModal" tabindex="-1" role="dialog" aria-labelledby="iconModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title grow" id="iconModalLabel">Описание1</h5>
        <button type="button" class="close" data-dismiss="modal" (click)="removeIcon()" aria-label="Close">
          <span aria-hidden="true" (click)="removeIcon()">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <div id="content" style="height: 400px;overflow-y: auto;">
          <ul class="nav nav-tabs">
            <li class="nav-item">
              <a class="nav-link active" data-target="#f1tab" data-toggle="tab">Комментарии</a>
            </li>
          </ul>
          <div class="tab-content">
            <div class="tab-pane active" id="f1tab">
              <textarea cols="20" id="NoteComment" name="NoteComment" rows="2" placeholder="Описание"
                        style="width: 100%; height: 100px;"></textarea>
` + comment + `
            </div>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-success">Save changes</button>
      </div>
    </div>
  </div>
</div>
`);
    jQuery('#iconIntModal').modal();
  }

  generateComent(iconRecords: Array<IconRecord>) {
    let result = "";
    iconRecords.forEach(iconRecord =>
      result += `
               <div class="aleralert alert-info" role="alert" style="text-align: left">
                <div class="row">
                  <div class="col-lg-12">` + iconRecord.note + `</p></div>
                </div>
                <p><b>Значение поля:</b>` + iconRecord.displayField + `</p>
                <p style="text-align: right;font-size: 12px"><b>Дата регистрации:</b>` + iconRecord.dateCreate +
        `<b>Автор:</b>` + iconRecord.userName + `</p>

              </div>
    `)
    return result;
  };


}
