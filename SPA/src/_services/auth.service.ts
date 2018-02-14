import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from './../environments/environment';
import { Injectable } from '@angular/core';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError, retry, map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthService {
    private baseUrl = environment.apiUrl;
    private userToken: any;
    public decodedToken: any;
    // jwtHelper: JwtHelper = new JwtHelper();

    //public httpOptions: any;
    constructor(private http: HttpClient, public jwtHelperService: JwtHelperService ) {
        // super();                      
    }    

    login(data: any): Observable<any> {        
        return this.http.post(this.baseUrl+'auth/login', data )
        .pipe(
            map( res => {
                const user = JSON.stringify(res);
                if (user) {
                  localStorage.setItem('token', user);
                  this.decodedToken = this.jwtHelperService.decodeToken(user);
                  this.userToken = user;
                //   return this.decodedToken.unique_name;                  
                }
            }),
            catchError(this.handleError)
        );     
    }

    register(model: any) {
        return this.http.post(this.baseUrl + 'register', model)
        .pipe(
            map( res => {
                // const user = res;
                // if (user) {
                //   localStorage.setItem('token', user.tokenString);
                //   this.decodedToken = this.jwtHelper.decodeToken(user.tokenString);
                //   console.log(this.decodedToken);
                //   this.userToken = user.tokenString;
                // }
            }),
            catchError(this.handleError)
        );
    }

    // public requestOptions(){
    //     return { 
    //         headers: new HttpHeaders({
    //           'Content-Type':  'application/json'
    //         })
    //     }; 
    // }

    private handleError(error: HttpErrorResponse){
        if (error.error instanceof ErrorEvent) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          console.error(
            `Backend returned code ${error.status}, ` +
            `body was: ${error.error}`);
        }
        // return an ErrorObservable with a user-facing error message
        return new ErrorObservable(error.error);
    };

    loggedIn(){
        // return this.jwtHelper.isTokenExpired('token');
        const token: string = this.jwtHelperService.tokenGetter()

        if (!token) {
            return false
        }

        const tokenExpired: boolean = this.jwtHelperService.isTokenExpired(token)

        return !tokenExpired
    }

    logOut(){
        return localStorage.removeItem('token');
    }

}