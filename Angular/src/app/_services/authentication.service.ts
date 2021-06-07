import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { User } from '../_models/_classes/user';
import { IRegisterDoctor } from '../_models/_interfaces/IRegisterDoctor';
import { IRegisterUser } from '../_models/_interfaces/IRegisterUser';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;
    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User {
        return this.userSubject.value;
    }
    registerUser(registerUser:IRegisterUser ){
        return this.http.post<IRegisterUser>(`${environment.apiUrl}/api/Account/Register`,registerUser)
        .pipe(catchError((err)=>{
            return throwError(err.message ||"Internal Server error contact site adminstarator");
            }
        ));
    }
    register(registerDoctor:IRegisterDoctor ){
        return this.http.post<IRegisterDoctor>(`${environment.apiUrl}/api/doctor`,registerDoctor)
        .pipe(catchError((err)=>{
            return throwError(err ||"Internal Server error contact site adminstarator");
            }
        ));
    }
    

    login(username: string, PasswordHash: string) {
        return this.http.post<any>(`${environment.apiUrl}/Login`, { username, PasswordHash })
            .pipe(map(res => {
                this.setSession(res);
            }));
    }

   
    private setSession(authResult) {
        //const expiresAt = moment().add(authResult.expiresIn,'second');
        const expiresAt = authResult.expiration;
        localStorage.setItem('token', authResult.token);
        localStorage.setItem("expires_at", JSON.stringify(expiresAt));//.valueOf()) );
    }  

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem("token");
        localStorage.removeItem("expires_at");
        this.router.navigate(['/login']);
    }
    
    public isLoggedIn() {
        if(localStorage.getItem('token')){
            let token = localStorage.getItem('token');

            let jwtData = token.split('.')[1]

            let decodedJwtJsonData = window.atob(jwtData)

            let decodedJwtData = JSON.parse(decodedJwtJsonData)

            let expirationDateInMills = decodedJwtData.exp * 1000;

            let todayDateInMills = new Date().getTime();

            if (expirationDateInMills >= todayDateInMills)
                return true;
            
        }
        return false;
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }
    getRole():string {
        if(localStorage.getItem('token')){
            let token = localStorage.getItem('token');

            let jwtData = token.split('.')[1]

            let decodedJwtJsonData = window.atob(jwtData)

            let decodedJwtData = JSON.parse(decodedJwtJsonData)
            return decodedJwtData.role;
        }
        return "No Role";
      }
    getUserId(){
        if(localStorage.getItem('token')){
            let token = localStorage.getItem('token');

            let jwtData = token.split('.')[1]

            let decodedJwtJsonData = window.atob(jwtData)

            let decodedJwtData = JSON.parse(decodedJwtJsonData)
            let userID=decodedJwtData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
            return userID;
        }
        return null;
    }
    /*          
    public isLoggedIn() {
        return moment().isBefore(this.getExpiration());
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }

    getExpiration() {
        const expiration = localStorage.getItem("expires_at");
        const expiresAt = JSON.parse(expiration);
        return moment(expiresAt);
    }  
    */

    
}