import { Component, Inject, OnInit } from '@angular/core';
import { Route, Router,  ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { Location } from '@angular/common';

import { AccountService } from './../services/account.service';
import { RoleService } from './../services/role.service';
import { UserService } from './../services/user.service';
import { MessageService } from './../services/message.service';

import { User } from '../interfaces/user.interface';

@Component({
    selector: 'nav-bar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.css']
})
export class NavBarComponent implements OnInit {
    user: User = <User>{};
    resetUser: User = <User>{};

    constructor(
        private router: Router,
        private roleService: RoleService,
        private userService: UserService,
        private alert: MessageService,
        private accountService: AccountService
    )
    {}

    ngOnInit(): void
    {
        this.getLoggedInUser();
    }

    logout(): void
    {
        this.accountService.logout()
        .subscribe(
            (data) => {
                this.userService.user = this.resetUser;
                this.router.navigate(["/home"]);
                this.roleService.reset(); 
            },
            (error) => this.alert.setMessage(error, "danger")
            );       
    }

    getLoggedInUser()
    {
        this.userService.getProfile()
        .subscribe(
            (data) => { 
                this.user = data;
                this.userService.accessLevel = this.user.role;
                this.roleService.setRole();                
            },
            (error) => error
        );
    }

}

