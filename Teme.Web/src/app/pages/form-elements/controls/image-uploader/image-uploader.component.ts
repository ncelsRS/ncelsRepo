import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-image-uploader',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './image-uploader.component.html',
  styleUrls: ['./image-uploader.component.scss']
})
export class ImageUploaderComponent {
    public image:any;
  
    fileChange(input){
        const reader = new FileReader();
        if (input.files.length) {
            const file = input.files[0];
            reader.onload = () => {
                this.image = reader.result;
            }
            reader.readAsDataURL(file);           
        }
    }

    removeImage():void{
        this.image = '';
    }

}

