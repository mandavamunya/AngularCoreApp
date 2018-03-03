import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { UserService } from '../../services/user.service';

import { UserViewModel } from '../../interfaces/user.interface';

@Component({
  selector: "users",
  templateUrl: "./users.component.html",
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  public users: UserViewModel[] = [];
  private allData: UserViewModel[] = [];
  private selectedUser: UserViewModel = <UserViewModel>{};


  private index = 0;
  private records = 5;

  private next = false;
  private previous = true;

  constructor(
      private http: Http, 
      private userService: UserService,
      private router: Router,
      @Inject("BASE_URL") private baseUrl: string
  ) {}

  ngOnInit(): void 
  {
    this.getUsers();
  }

  getUsers(): void 
  {
    this.http.get(this.baseUrl + "api/User").subscribe(
      result => 
      {
        this.allData = result.json() as UserViewModel[];
        this.users = this.Users().splice(0, this.records);
      },
      error => console.error(error));
  }

  Users(): UserViewModel[]
  {
      return Object.assign([], this.allData);
  }

  Next(): void
  {
    this.index = this.index + this.records;
    this.users = this.Users().splice(this.index, this.records);  
    this.pagination(); 
  }

  Previous(): void
  {
    this.index = this.index - this.records;
    this.users = this.Users().splice(this.index, this.records);   
    this.pagination();
  }

  pagination()
  {
    var check = this.index + this.records;
    if (check >= this.allData.length)
        this.next = true;
    else 
        this.next = false;

    if (this.index <= 0)
        this.previous = true;
    else 
        this.previous = false;
  }

  onSelect(user: UserViewModel): void
  {
    this.selectedUser = user;
    this.userService.selectedUser = this.selectedUser;
  }

  gotoUser(): void
  {
    this.router.navigate(['/user']);
  }
}