import { ReactiveErrors } from '@angular/forms/src/directives/reactive_errors';
import { Observable } from 'rxjs/Rx';
import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

import { ChangePassword } from '../authentication/manage/change-password/change-password.interface';

@Injectable()
export class ManageService {

    constructor(
        private http: Http,
         @Inject('BASE_URL') private baseUrl: string) 
    {}

    ChangePassword(model: ChangePassword) 
    {
        return this.http.post(this.baseUrl + 'api/Manage/ChangePassword',  model)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

}