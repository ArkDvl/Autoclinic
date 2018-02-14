import { Router } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { UserService } from './../../_services/user.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-modules',
  templateUrl: './modules.component.html',
  styleUrls: ['./modules.component.css']
})
export class ModulesComponent implements OnInit {
  @Input() module: any;
  
  public name: string;
  public id: number;
  public code: string;

  constructor(public userService: UserService, public alertService: AlertifyService, public router: Router) {
    
  }

  ngOnInit() {
    console.log(this.module);
    this.fetchModuleInfo(this.module);
  }

  fetchModuleInfo(id: any){
    return this.userService.getModuleInfo(id).subscribe(
      res => {
        this.name = res.name;
        this.id = res.modulesID;
        this.code = res.key;
      },
      err => {
        this.alertService.error(err);
      }
    )
  }


  goToSingleModule(id,name,code)
  {
    this.router.navigateByUrl("apps/"+name+"/"+code);
  }

}
