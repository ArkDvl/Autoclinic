import { UserService } from './../../../_services/user.service';
import { HeaderComponent } from './../../_utils/header/header.component';
import { Component, OnInit } from '@angular/core';



@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  public profilepic: string;

  constructor(private userService: UserService ) { }

  ngOnInit() {

  }

  authorizationCheck(){
    this.userService.getModuleAuthStatus()
  }

  initialize(){

  }

}
