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
  url=environment.apiUrl+"/api/ClinicImage";
 

  AddImages(clinicImages)
  {
    return this._http.post<IClinicImage[]>(this.url,clinicImages).pipe(catchError((err)=>
    {
      console.log("error Is:"+err);
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  // /api/ClinicImage/GetAllImages
  GetClinicImages()
  {
    return this._http.get<IClinicImage[]>(this.url+"/GetAllImages").pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }

  DeleteImage(id)
  {
    return this._http.delete<any>(this.url+"/"+id).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));

}
}
