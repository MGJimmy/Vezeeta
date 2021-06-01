import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IClinicImage } from '../_models/_interfaces/IClinicImage';

@Injectable({
  providedIn: 'root'
})
export class ClinicImagesService {

  constructor(private _http:HttpClient) { }
  url=environment.apiUrl+"api/ClinicImage/";

  AddImages(clinicImages,clinidID)
  {
    return this._http.post<IClinicImage>(this.url+clinidID,clinicImages).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}
