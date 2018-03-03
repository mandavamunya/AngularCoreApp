import { ReactiveErrors } from '@angular/forms/src/directives/reactive_errors';
import { Observable } from 'rxjs/Rx';
import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

import { User, UserViewModel } from '../interfaces/user.interface';
import { Role, UserRole } from '../interfaces/roles.interface';

@Injectable()
export class UserService {
    public selectedUser: UserViewModel = <UserViewModel>{};
    //public selectedJournalist: Journalist;
    public user: User = <User>{};
    
    public accessLevel: string = <string>{};
    private model: User = <User>{};

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string
    ) 
    { }

    getProfile()
    {
        return this.http.get(this.baseUrl + 'api/Manage/',  "")
        .map((response: Response) => this.user = response.json())
        .catch((error) => {
            return Observable.throw(error);
        });                     
    }

    getUser(email: string)
    {
        return this.http.get(this.baseUrl + 'api/User/' + email)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    getUsers()
    {
        return this.http.get(this.baseUrl + 'api/User/')
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    getJournalists()
    {
        return this.http.get(this.baseUrl + 'api/User/Journalists')
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    UpdateUser(model: UserViewModel) 
    {
        return this.http.put(this.baseUrl + 'api/User/UpdateUser',  model)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }
    
    DeleteUser(username: string) 
    {     
        return this.http.delete(this.baseUrl + "api/User/" + username)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    AssignRole(user: UserRole)
    {
        return this.http.put(this.baseUrl +  "api/Role/AssignRole/", user)
        .map((response: Response) =>  response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    UserRoles()
    {
         return this.http.get(this.baseUrl + "api/Role/UserRoles/")
         .map((response: Response) => response.json())
         .catch((error) => {
            return Observable.throw(error);
         });
    }

    UserRolesByUsername(username: string)
    {
         return this.http.get(this.baseUrl + "api/Role/UserRolesByUsername/" + username)
         .map((response: Response) => response.json())
         .catch((error) => {
            return Observable.throw(error);
         });
    }

    getJournalistByUsername(username: string)
    {
         return this.http.get(this.baseUrl + "api/User/JournalistByUsername/" + username)
         .map((response: Response) => response.json())
         .catch((error) => {
            return Observable.throw(error);
         });
    }    

    OtherRolesByUsername(username: string)
    {
        return this.http.get(this.baseUrl + "api/Role/OtherRolesByUsername/" + username)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }    

    OtherRoles()
    {
        return this.http.get(this.baseUrl + "api/Role/OtherRoles")
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    DeleteRole(user: UserRole)
    {
        return this.http.post(this.baseUrl + "api/Role/DeleteRole", user)
        .map((response: Response) => response.json())
        .catch((error) => {
            console.log(error);
            return Observable.throw(error);
        });
    }

}