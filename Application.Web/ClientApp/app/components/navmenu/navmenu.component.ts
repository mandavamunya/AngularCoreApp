import { forEach } from '@angular/router/src/utils/collection';
import { Component, OnInit } from '@angular/core';

import { UserService } from './../services/user.service';
import { RoleService } from './../services/role.service';

import { User } from '../interfaces/user.interface';
import { Role, UserRoles } from '../interfaces/roles.interface';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
    user: User = <User>{};
    userRole: UserRoles = <UserRoles>{};

    role: Role = <Role>{};
    administrator: string = <string>{};

    thisValue = true; 

    constructor(
        private roleService: RoleService,
        private userService: UserService
    )
    {}

    ngOnInit()
    {
        this.getLoggedInUser();
    }

    getLoggedInUser()
    {
        this.userService.getProfile()
        .subscribe(
            (data) =>  { 
                this.user = data;
                this.userService.accessLevel = this.user.role;
                this.roleService.setRole(); 
            },
            (error) => error 
        );                
    }

}
