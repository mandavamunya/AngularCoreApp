import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { MessageService } from '../../services/message.service';

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
      private router: Router,
      private alert: MessageService,
      private userService: UserService,
      @Inject("BASE_URL") private baseUrl: string
  ) {}

  ngOnInit(): void 
  {
    this.getUsers();
  }

  private getUsers(): void
  {
      this.userService.getUsers().subscribe(
          (data) => 
          {
            this.allData = data as UserViewModel[];
            this.users = this.Users().splice(0, this.records);              
          },
          (error) => 
          {
              this.alert.setMessage(error, "warning")                
          }
      );        
  }  

  private Users(): UserViewModel[]
  {
      return Object.assign([], this.allData);
  }

  private Next(): void
  {
    this.index = this.index + this.records;
    this.users = this.Users().splice(this.index, this.records);  
    this.pagination(); 
  }

  private Previous(): void
  {
    this.index = this.index - this.records;
    this.users = this.Users().splice(this.index, this.records);   
    this.pagination();
  }

  private pagination()
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

  private onSelect(user: UserViewModel): void
  {
    this.selectedUser = user;
    this.userService.selectedUser = this.selectedUser;
  }

  private gotoUser(): void
  {
    this.router.navigate(['/user']);
  }
}