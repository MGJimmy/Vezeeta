import { HttpClient, HttpEventType } from '@angular/common/http';

import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { environment } from 'src/environments/environment';

import { FormBuilder, Validators } from '@angular/forms';




@Component({
  selector: 'app-upload-and-show',
  templateUrl: './upload-and-show.component.html',
  styleUrls: ['./upload-and-show.component.scss']
})
export class UploadAndShowComponent implements OnInit {
  public progress: number;
  public message: string;
  photo:string="/assets/Image/uploadLogo.jpg"

  @Output() public onUploadFinished = new EventEmitter();
  constructor(private http: HttpClient,private _fb:FormBuilder) {
   }

  ngOnInit(): void {
    this.form=this._fb.group({
      image:["",Validators.required]
    })
  }  
  
  
  @Input('formGroupFromParent') form;
  


  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let uploadUrl = `${environment.apiUrl}/api/upload`
    let fileToUpload = <File>files[0];
    
    var reader = new FileReader();  
    reader.onload = (event: any) => {  
        this.photo = event.target.result;          
    }
    reader.readAsDataURL(fileToUpload);

    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post(uploadUrl, formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}
