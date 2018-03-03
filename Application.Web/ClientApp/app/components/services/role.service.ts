import { Injectable, Inject } from "@angular/core";
import { Http, Headers, Response } from "@angular/http";
import { Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

import { UserService } from './user.service';
import { Role } from '../interfaces/roles.interface';

@Injectable()
export class RoleService {
    
    constructor(
        private userService: UserService
    )
    {

    }
    public developer: boolean = <boolean>{};
    public journalist: boolean = <boolean>{};
    public administrator: boolean = <boolean>{};
    public editor: boolean = <boolean>{};
    public manager: boolean = <boolean>{};

    reset()
    {
        this.userService.accessLevel = "";
        this.developer = false;
        this.journalist = false;
        this.administrator = false;
        this.editor = false;
        this.manager = false;
    }

    setRole()
    {
        if (this.userService.accessLevel == "Developer")
            this.developer = true;
        else if (this.userService.accessLevel == "Journalist")
            this.journalist = true;
        else if (this.userService.accessLevel == "Administrator")
            this.administrator = true;
        else if (this.userService.accessLevel == "Editor")
            this.editor = true;
        else
            this.manager = true;
    }
}