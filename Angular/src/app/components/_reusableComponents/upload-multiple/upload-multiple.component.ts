import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-upload-multiple',
  templateUrl: './upload-multiple.component.html',
  styleUrls: ['./upload-multiple.component.scss']
})
export class UploadMultipleComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }


  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();


  photo: any = "/assets/Image/uploadLogo.jpg";
  photo2: any ;
  Images:any=[];
  hasImages:boolean=false;
  public uploadFile = (files) => {
    if (files.length === 0) {
      this.hasImages=false;
      return;
    }
this.hasImages=true;
    let filesToUpload: File[] = files;
    
    const formData = new FormData();

    Array.from(filesToUpload).map((file, index) => {
      return formData.append('file' + index, file, file.name);
    });

    for (let i = 0; i < files.length; i++)  //for multiple files
    {
      let f = files[i];
      let reader = new FileReader();
      reader.readAsDataURL(f);
      reader.onload = (_event) => {
        this.Images[i]=reader.result;
        this.photo2=reader.result;
      }
    }
    console.log(this.Images);

    
    let uploadUrl = `${environment.apiUrl}/api/UploadMultipleFiles`

    this.http.post(uploadUrl, formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress){
          console.log("if");
          this.progress = Math.round(100 * event.loaded / event.total);
        }
          
        else if (event.type === HttpEventType.Response) {
          console.log("Startelse");
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }

}
