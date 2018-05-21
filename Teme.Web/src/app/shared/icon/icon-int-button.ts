import {Component, ElementRef, forwardRef, Injectable, Input, OnInit} from '@angular/core';
import {environment} from '../../../environments/environment';
import {Icon} from './Icon';
import {HttpClient} from '@angular/common/http';
import {IconRecord} from './IconRecord';




@Component({
  selector: 'app-icon-int-button',
  template: `
    <div class="input-group-append" (click)="showIconModal()" data-toggle="modal" data-target="#iconIntModal" style="height: 100%;">
      <span class="input-group-text"><i class="fa fa-info-circle fa-lg"></i></span>
    </div>
    <div class="modal fade" id="iconIntModal" tabindex="-1" role="dialog" aria-labelledby="iconModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title grow" id="iconModalLabel">Описание1</h5>
            <button type="button" class="close" data-dismiss="modal" (click)="removeIcon()" aria-label="Close">
              <span aria-hidden="true">&times;</span>
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
                <div class="tab-pane active" id="f1tab"  style="min-height: 350px;">
                  <textarea cols="20" id="NoteComment" [(ngModel)]="note" name="NoteComment" rows="2" placeholder="Описание"
                        style="width: 100%; height: 100px;" ></textarea>

                  <div *ngFor="let iconRecord of iconRecords" class="aleralert alert-info" role="alert" style="text-align: left"
                       style="text-align:left; color:#0c5460; background-color:#d1ecf1; border-color:#bee5eb;">
                    <div class="row">
                      <div class="col-lg-12"><p>{{iconRecord.note}}</p></div>
                  </div>
                  <p><b>Значение поля: </b>{{iconRecord.displayField}}</p>
                  <p style="text-align: right;font-size: 12px"><b>Дата регистрации: </b>{{iconRecord.dateCreate}}
                    <b>Автор: </b>{{iconRecord.userName}}</p>
                  </div>
                  
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-success" (click)="saveComment()">Сохранить</button>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [],
  providers: [
  ]
})
@Injectable()
export class IconIntButton implements OnInit  {
  iconUrl:string = environment.urls.common + '/Icon/';
  dataIcon:Icon;
  iconRecords: Array<IconRecord> = [];

  @Input() moduleType;
  @Input() objectId;
  @Input() fieldName;
  @Input() valueField;
  @Input() displayField;
  note: string = "";




  constructor(private elementRefIconModal: ElementRef, private http: HttpClient) {

  }

  ngOnInit(){

  }
  showIconModal(){
    let params = {moduleType: this.moduleType, objectId: this.objectId, fieldName: this.fieldName}
    this.http.get( this.iconUrl + 'GetIconRecords',{ params: params })
      .subscribe((data:Icon)=> {this.iconRecords = data==null?[]:data.iconRecords});
  }

  saveComment(){

    let body = {moduleType: this.moduleType, objectId: this.objectId, fieldName: this.fieldName,
      valueField: this.valueField, displayField: this.displayField}
    console.log("body", body)

    body['note'] = this.note;
    this.http.post(this.iconUrl + 'CreateIcon', body).subscribe(data => {
      console.log(data);
      this.ngOnInit();
    });
    this.note = "";
  }

}
