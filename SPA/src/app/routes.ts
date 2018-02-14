import { InventoryComponent } from './_apps/inventory/inventory.component';
import { UserManagementComponent } from './_apps/user-management/user-management.component';
import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PasswordComponent } from './password/password.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from '../_guard/auth.guard';

export const appRoutes: Routes = [
    { path: 'login', component: LoginComponent},
    { path: 'password', component: PasswordComponent },
    { path: '', canActivate: [AuthGuard], component: HomeComponent },
    { path: 'app', canActivate: [AuthGuard], component: HomeComponent },
    {
        path: 'app',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'inventory', component: InventoryComponent },
            { path: 'user-management', component: UserManagementComponent }
        ]
    },
    { path: '**', redirectTo: 'login', pathMatch: 'full'},
];
