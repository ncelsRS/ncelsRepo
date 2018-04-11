import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-file-uploader',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.scss']
})
export class FileUploaderComponent {
    public file:any;
  
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
