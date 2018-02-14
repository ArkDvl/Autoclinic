import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from './../environments/environment';
import { Injectable } from '@angular/core';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError, retry, map } from 'rxjs/operators';
// import { tokenNotExpired, JwtHelper, AuthHttp } from 'angular2-jwt';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class UserService {
    private baseUrl = environment.apiUrl;
    // private userToken: any;
    // public decodedToken: any;
    // jwtHelper: JwtHelper = new JwtHelper();

    //public httpOptions: any;
    public token = localStorage.getItem('token');

    constructor(private http: HttpClient, public jwtHelperService: JwtHelperService ) {
        // super();                      
    }    

    getUserInfo(data: any): Observable<any> {        
        return this.http.get(this.baseUrl+'user/'+data,this.requestOptions())
        .pipe(
            map( res => {                
                return res;
            }),
            catchError(this.handleError)
        );     
    }

    updatePassword(data: any): Observable<any>{
        return this.http.put(this.baseUrl+'user/'+data.id, { password: data.password }, this.requestOptions())
        .pipe(
            map(
                res => {
                    return true;
                }
            ),
            catchError(this.handleError)
        )
    }

    // register(model: any) {
    //     return this.http.post(this.baseUrl + 'register', model, this.requestOptions())
    //     .pipe(
    //         map( res => {
    //             // const user = res;
    //             // if (user) {
    //             //   localStorage.setItem('token', user.tokenString);
    //             //   this.decodedToken = this.jwtHelper.decodeToken(user.tokenString);
    //             //   console.log(this.decodedToken);
    //             //   this.userToken = user.tokenString;
    //             // }
    //         }),
    //         catchError(this.handleError)
    //     );
    // }

    public requestOptions(){
        return { 
            headers: new HttpHeaders({
              'Content-Type':  'application/json',
              'Authorization': 'Bearer '+ JSON.parse(this.token),

            })
        }; 
    }

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

    //get modules authorization status
    getModuleInfo(data: any): Observable<any>{
        return this.http.get(this.baseUrl+'module/'+data, this.requestOptions())
        .pipe(
            map( res => {
                return res;
            }),
            catchError(this.handleError)
        )
    }

    getModuleAuthStatus(data: any): Observable<any>{
        return this.http.post(this.baseUrl+'user/moduleauth', data, this.requestOptions())
        .pipe(
            map(
                res => {
                    return res;
                }
            ),
            catchError(this.handleError)
        )
    }

}