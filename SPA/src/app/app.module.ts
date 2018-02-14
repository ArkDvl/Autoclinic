import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './../_services/auth.service';
import { UserService } from './../_services/user.service';
import { AlertifyService } from './../_services/alertify.service';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { RouterModule, Routes } from '@angular/router';
import { appRoutes } from './routes';
import { AuthGuard } from '../_guard/auth.guard';


import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { PasswordComponent } from './password/password.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './_utils/header/header.component';
import { BsDropdownModule, ModalModule } from 'ngx-bootstrap';
import { ModulesComponent } from './modules/modules.component';

//apps
import { InventoryComponent } from './_apps/inventory/inventory.component';
import { UserManagementComponent } from './_apps/user-management/user-management.component';
import { ReplacespacePipe } from './pipes/replacespace.pipe';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PasswordComponent,
    HomeComponent,
    HeaderComponent,
    ModulesComponent,
    InventoryComponent,
    UserManagementComponent,
    ReplacespacePipe
],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('token');
        },
        whitelistedDomains: ['http://localhost:5000'],
        skipWhenExpired: true
      }
    })
  ],
  providers: [
    AuthService, UserService, AlertifyService, AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
