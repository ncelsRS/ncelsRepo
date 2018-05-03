import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-ext-certificate',
  templateUrl: './ext-certificate.component.html',
  styleUrls: ['./ext-certificate.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ExtCertificateComponent implements OnInit {

  public file:any;

  fileUpload=false;
  constructor() { }

  ngOnInit() {
  }

  changeHiddenFileUpload()
  {
    this.fileUpload=true;
  }

  changeVisibleFileUpload()
  {
    this.fileUpload=false;
  }

  fileChange(input){
    const reader = new FileReader();
    if (input.files.length) {
      this.file = input.files[0].name;
    }
  }

  removeFile():void{
    this.file = '';
  }

}
