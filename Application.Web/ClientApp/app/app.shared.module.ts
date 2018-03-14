import { combineAll } from 'rxjs/operator/combineAll';
import { NgModule } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { Time } from './components/services/time.service';
import { PostService } from './components/services/post.service';
import { BlogService } from './components/services/blog.service';
import { ManageService } from './components/services/manage.service';
import { AccountService } from './components/services/account.service';
import { UserService } from './components/services/user.service';
import { MessageService } from './components/services/message.service';
import { RoleService } from './components/services/role.service';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { NavBarComponent } from './components/navbar/navbar.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { BlogComponent } from './components/business-logic/blog/blog.component';
import { BlogsComponent } from './components/business-logic/blog/blogs.component';

import { ExternalLoginComponent } from './components/authentication/account/external-login/external-login.component';
import { ForgotPasswordComponent } from './components/authentication/account/forgot-password/forgot-password.component';
import { LoginComponent } from './components/authentication/account/login/login.component';
import { LoginWith2faComponent } from './components/authentication/account/login-with-2fa/login-with-2fa.component';
import { LoginWithRecoveryComponent } from './components/authentication/account/login-with-recovery/login-with-recovery.component';
import { RegisterComponent } from './components/authentication/account/register/register.component';
import { ResetPasswordComponent } from './components/authentication/account/reset-password/reset-password.component';

import { ChangePasswordComponent } from './components/authentication/manage/change-password/change-password.component';
import { EnableAuthenticatorComponent } from './components/authentication/manage/enable-authenticator/enable-authenticator.component';
import { ExternalLoginsComponent } from './components/authentication/manage/external-logins/external-logins.component';
import { GenerateRecoveryCodesComponent } from './components/authentication/manage/generate-recovery-codes/generate-recovery-codes.component';
import { ManageComponent } from './components/authentication/manage/manage/manage.component';
import { ManageAccountComponent } from "./components/authentication/manage/manage-account.component";
import { ManageProfileComponent } from './components/authentication/manage/manage/manage-profile.component';
import { RemoveLoginComponent } from './components/authentication/manage/remove-login/remove-login.component';
import { SetPasswordComponent } from './components/authentication/manage/set-password/set-password.component';
import { TwoFactorAuthenticationComponent } from './components/authentication/manage/two-factor-authentication/two-factor-authentication.component';

import { UsersComponent } from './components/authentication/user/users.component';
import { UserComponent } from './components/authentication/user/user.component';
import { RolesComponent } from './components/authentication/roles/roles.component';
import { ManageRolesComponent } from './components/authentication/roles/manage-roles.component';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'blog', component: BlogComponent },
    { path: 'blogs', component: BlogsComponent},

    { path: 'manage-account',  component: ManageAccountComponent },
    { path: 'external-login', component: ExternalLoginComponent },
    { path: 'forgot-password', component: ForgotPasswordComponent },
    { path: 'login', component: LoginComponent },
    { path: 'login-with-2fa', component: LoginWith2faComponent },
    { path: 'login-with-recovery', component: LoginWithRecoveryComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'reset-password', component: ResetPasswordComponent },
    
    { path: 'change-password', component: ChangePasswordComponent },
    { path: 'enable-authenticator', component: EnableAuthenticatorComponent },
    { path: 'external-logins', component: ExternalLoginsComponent },
    { path: 'generate-recovery-codes', component: GenerateRecoveryCodesComponent },
    { path: 'manage', component: ManageComponent },
    { path: 'remove-login', component: RemoveLoginComponent },
    { path: 'set-password', component: SetPasswordComponent },
    { path: 'two-factor-authentication', component: TwoFactorAuthenticationComponent },
    
    { path: 'users', component: UsersComponent },
    { path: 'user', component: UserComponent },

    { path: '**', redirectTo: 'home' }   
];

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        NavBarComponent,
        NavMenuComponent,
        BlogComponent,
        BlogsComponent,

        ExternalLoginComponent,
        ForgotPasswordComponent,
        LoginComponent,
        LoginWith2faComponent,
        LoginWithRecoveryComponent,
        RegisterComponent,
        ResetPasswordComponent,

        ChangePasswordComponent,
        EnableAuthenticatorComponent,
        ExternalLoginsComponent,
        GenerateRecoveryCodesComponent,
        ManageComponent,
        ManageAccountComponent,
        ManageProfileComponent,
        RemoveLoginComponent,
        SetPasswordComponent,
        TwoFactorAuthenticationComponent,
        
        UsersComponent,
        UserComponent,
        RolesComponent,
        ManageRolesComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        BrowserModule, 
        ReactiveFormsModule,
        RouterModule.forRoot(routes)  
    ],
    providers: [ 
        Time,
        UserService,
        RoleService,
        BlogService,
        PostService,
        ManageService,
        MessageService,
        AccountService
    ]
})
export class AppModuleShared {
}
