import { AuthService } from './../../_services/auth.service';
import { AlertifyService } from './../../_services/alertify.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public error: string = "";
  public hasError: boolean = false;
  public submitButton: boolean = false;
  loginForm: FormGroup;
  constructor(private authService: AuthService, private alert: AlertifyService, private router: Router) { 
    this.loginForm = new FormGroup({
      Username: new FormControl('', [Validators.required,Validators.email]),
      Password: new FormControl('', [Validators.required] )
    })
  }

  ngOnInit() {
    if(this.authService.loggedIn()){
      this.router.navigate(['']);  
    }
  }

  login(loginValues){
    this.submitButton = true;
    this.hasError = false; 
    this.authService.login(loginValues).subscribe(
      (res) => {
        console.log(res);
        this.submitButton = false;        
        this.router.navigate(['']); 
      },
      (err) => {
        this.submitButton = false;
        this.hasError = true;    
        this.error = err;
        this.alert.error(err);
      }
    )
    // console.log(loginValues);
  }

  logOut(){
    this.authService.logOut();
    this.authService.decodedToken = null;
    this.alert.message("You logged out!");
  }


}
