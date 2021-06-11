import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IDayShift } from '../_models/_interfaces/IDayShift';

@Injectable({
  providedIn: 'root'
})
export class DayshiftService {

  constructor(private _http:HttpClient) { }
  url=`${environment.apiUrl}/api/DayShift`;

  GetById(id:number):Observable<IDayShift>{
    return this._http.get<IDayShift>(this.url+"/"+id).pipe(catchError(error=>{
      return throwError(error.message|| "an error occur");
    }));
  }


}
