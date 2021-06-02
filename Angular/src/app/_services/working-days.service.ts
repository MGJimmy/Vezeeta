import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IWorkingDay } from '../_models/_interfaces/IWorkingDay';

@Injectable({
  providedIn: 'root'
})
export class WorkingDaysService {

  constructor(private _http:HttpClient) { }
  addWorkingDays(workingDays:IWorkingDay[]):Observable<any>{
    let url = `${environment.apiUrl}/api/WorkingDay`;
    return this._http.post<IWorkingDay[]>(url, workingDays)
      .pipe(catchError((err)=>{
        return throwError(err.message ||"Internal Server error contact site adminstarator");
      }
    ));
  }
}
