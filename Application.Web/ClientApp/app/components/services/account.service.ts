import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { UserService } from './user.service';

import { Login } from '../authentication/account/login/login.interface';
import { Register } from '../authentication/account/register/register.interface';

@Injectable()
export class AccountService {
    constructor(
        private http: Http,
        private router: Router,
        private userService: UserService,
        @Inject('BASE_URL') private baseUrl: string) 
    { 
    }

    login(model: Login) 
    {
        return this.http.post(this.baseUrl + 'api/Account/Login',  model)
            .map((response: Response) => response.json())
            .catch((error) => {
                return Observable.throw(error);
            });
    }

    Register(model: Register) 
    {
        return this.http.post(this.baseUrl + 'api/Account/Register',  model)
            .map((response: Response) => response.json())
            .catch((error) => {
                return Observable.throw(error);
            });
    }

    logout() 
    {
        return this.http.post(this.baseUrl + 'api/Account/Logout', null)
            .map((response: Response) => response.json())
            .catch((error) => {
                return Observable.throw(error);
            });
    }
    
}