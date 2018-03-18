import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { MessageService } from '../../services/message.service';

import { User, UserViewModel } from '../../interfaces/user.interface';

@Component({
  selector: 'user',
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {
    user: UserViewModel = <UserViewModel>{};
    model: User = <User>{};

    constructor(
        private http: Http,
        private router: Router,
        private alert: MessageService,         
        private userService: UserService,
        @Inject('BASE_URL') private baseUrl: string
    ) 
    {
    }

    ngOnInit(): void
    {
        this.getUser();
    }

    private getUser(): void
    {
        this.userService.getUser(this.userService.selectedUser.username).subscribe(
            (data) => 
            {
                this.user = data as UserViewModel;
                this.userService.selectedUser = this.user;              
            },
            (error) => 
            {
                this.alert.setMessage(error, "warning")                
            }
        );      
    }

    private gotoUsers(): void
    {
        this.router.navigate(['/users']);
    }
}