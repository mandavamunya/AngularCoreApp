import { Component, OnInit } from '@angular/core';

import { UserService } from './../services/user.service';
import { RoleService } from './../services/role.service';
import { MessageService } from './../services/message.service';

import { User } from '../interfaces/user.interface';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    model: User = <User>{};
    
    constructor(
        private roleService: RoleService,
        private userService: UserService,
        private alert: MessageService
    )
    {
    }

    ngOnInit()
    {
        this.getLoggedInUser();
        this.alert.timeOut();
    }

    getLoggedInUser() 
    {    
        this.userService.getProfile().subscribe(
            (data) => { 
                this.model = data;
                this.userService.accessLevel = this.model.role;
                this.roleService.setRole();                  
            },
            (error) => error
        );  
    } 

}