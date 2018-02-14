import { ModulesComponent } from './../modules/modules.component';
import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from '../_utils/header/header.component';
import { AuthService } from './../../_services/auth.service';
import { UserService } from './../../_services/user.service';
import { AlertifyService } from './../../_services/alertify.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  public src: string;
  public submitButton: boolean = false;
  passwordForm: FormGroup;
  public firstname: any;
  public error: string;
  public passwordReset: boolean = false;  
  public mod: any;
  // public pattern = /^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)|(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?=.*\d)|(?=.*[a-z])(?=.*\W)(?=.*[A-Z])(?=.*\d)){8}$/;
  

  constructor(private userService: UserService, private authService: AuthService, private alert: AlertifyService, private router: Router) { 
    this.passwordForm = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirm_password: new FormControl('', [Validators.required ] ),
      id: new FormControl('')
    },{ validators: [this.confirmPassword, this.passwordStrength] })
  }

  ngOnInit() {
    this.userService.getUserInfo(this.authService.decodedToken.nameid).subscribe(
      (res) => {
        localStorage.setItem('user', JSON.stringify(res));
        this.firstname = res.person.firstName;
        this.mod = JSON.parse(res.modules);
        this.src = res.profilePic;
        this.alert.success("Welcome back " +res.person.firstName+" "+res.person.lastName );
        this.passwordReset = (res.expiry == 1) ? false : true;
      },
      (err) => {
        this.alert.error(err);
      }
    )
  }

  private confirmPassword(group: FormGroup) {
    return group.get('password').value === group.get('confirm_password').value ? null : { mismatch: true };
  }

  private passwordStrength(group: FormGroup) {
    
    // if(/[^a-z]/.test(group.get('password').value) && /[^A-Z]/.test(group.get('password').value) && /[^\d]/.test(group.get('password').value) && /[^\W]/.test(group.get('password').value))
    // {
    //   return null;
    // }

    // return { strong: true };
    var pwString = group.get('password').value;
    var pwStringLength = group.get('password').value.length;
    var strength = 0;

    strength += /[A-Z]+/.test(pwString) ? 1 : 0;
    strength += /[a-z]+/.test(pwString) ? 1 : 0;
    strength += /[0-9]+/.test(pwString) ? 1 : 0;
    strength += /[\W]+/.test(pwString) ? 1 : 0;
    // strength += /[\s]+/.test(pwString) ? 1 : 0;

    switch(strength) {

        case 1:
            return { strength: false, message: 'Very weak'};

        case 2:
            return { strength: false, message: 'Fairly weak'};        
        
        case 3:
            return { strength: false, message: 'Weak'};
            // break;
        case 4:
            return null;
            // break;
        default:
            break;
    }
  }


  changePassword(){
    this.submitButton = true;
    if(this.passwordForm.valid){
      // console.log(this.passwordForm.value);
      var id: number = this.authService.decodedToken.nameid;
      this.passwordForm.controls['id'].patchValue(id);
      this.userService.updatePassword(this.passwordForm.value).subscribe(
        (res) =>{
          if(res){
            this.submitButton = false;
            this.passwordReset = false;
          }
        },
        (err) => {
          this.submitButton = false;
          this.alert.error(err);
        }
      )
    }
  }

  logOut(){
    this.authService.logOut();
    this.authService.decodedToken = null;
    this.alert.message("You logged out!");
    this.router.navigate(['/login']); 
  }

}
