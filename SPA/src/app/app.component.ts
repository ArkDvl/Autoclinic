import { Component } from '@angular/core';
import {JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  /**
   *
   */
  // jwtHelper: JwtHelper = new JwtHelper();

  constructor(private authService: AuthService, public jwtHelperService: JwtHelperService) { 

  }

  ngOnInit(){
    // const token = localStorage.getItem('token');
    const token: string = this.jwtHelperService.tokenGetter()
    if(token){
      this.authService.decodedToken = this.jwtHelperService.decodeToken(token);
      console.log(this.authService.decodedToken);
    }
  }
}
