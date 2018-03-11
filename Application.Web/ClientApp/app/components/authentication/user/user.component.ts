import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { UserService } from '../../services/user.service';

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
        private userService: UserService,
        private route: ActivatedRoute,
        @Inject('BASE_URL') private baseUrl: string
    ) 
    {
    }

    ngOnInit(): void
    {
        this.getUser();
    }

    getUser(): void
    {
        this.http.get(this.baseUrl + 'api/User/' + this.userService.selectedUser.username).subscribe(result => {
            this.user = result.json() as UserViewModel;
            this.userService.selectedUser = this.user;
        }, error => console.error(error));
    }

    gotoUsers(): void
    {
        this.router.navigate(['/users']);
    }
}