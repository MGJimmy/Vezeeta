import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import {catchError} from 'rxjs/operators';
import { ICategory } from '../_models/_interfaces/ICategory';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private _http:HttpClient) { }

  getAllCategories():Observable<ICategory[]> {
    let url = `${environment.apiUrl}/api/category`;
    return this._http.get<ICategory[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCategoryById(id:number):Observable<ICategory>{
    let url = `${environment.apiUrl}/api/category/${id}`;
    return this._http.get<ICategory>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  addNewCategory(newCategory:ICategory):Observable<ICategory>{
    let url = `${environment.apiUrl}/api/category`;
    return this._http.post<ICategory>(url, newCategory)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  updateCategory(id:number, categoryToUpdate:ICategory):Observable<ICategory>{
    let url = `${environment.apiUrl}/api/category/${id}`;
    return this._http.put<ICategory>(url, categoryToUpdate)
            .pipe(catchError((err)=>{
              return throwError(err.message ||"Internal Server error contact site adminstarator");
                }
              ));
  }
  deleteCategory(id:number):Observable<any>{
    let url = `${environment.apiUrl}/api/category/${id}`;
    return this._http.delete<any>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCategoriesCount():Observable<number>{
    let url = `${environment.apiUrl}/api/category/count`;
    return this._http.get<number>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
  getCategoriesByPage(pageSize:number, pageNumber:number):Observable<ICategory[]>{
    let url = `${environment.apiUrl}/api/category/${pageSize}/${pageNumber}`;
    return this._http.get<ICategory[]>(url).pipe(catchError((err)=>
    {
      return throwError(err.message ||"Internal Server error contact site adminstarator");
    }));
  }
}