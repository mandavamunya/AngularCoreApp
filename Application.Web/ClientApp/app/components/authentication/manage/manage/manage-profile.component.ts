import { Component, Inject, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Rx';

import { UserService } from './../../../services/user.service';
import { RoleService } from './../../../services/role.service';
import { MessageService } from './../../../services/message.service';

import { User, UserViewModel } from '../../../interfaces/user.interface';

@Component({
  selector: "manage-profile",
  templateUrl: "./manage-profile.component.html",
  styleUrls: ["./manage-profile.component.css"]
})
export class ManageProfileComponent implements OnInit {
  
  indexViewModel: User = <User>{};
  viewModel: UserViewModel = <UserViewModel>{};
  model: User = <User>{};

  private email: string = "";

  constructor(
    private http: Http,
    private router: Router,
    private route: ActivatedRoute,
    private roleService: RoleService,
    private userService: UserService,
    private alert: MessageService, 
    @Inject("BASE_URL") private baseUrl: string
  ) 
  {
    this.email = route.snapshot.params.email;
  }

  ngOnInit(): void {
    if (this.email == null || this.email == "")
      this.getLoggedInUser();
    else 
      this.getUser();
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

  getUser(): void
  {
      this.userService.getUser(this.email).subscribe(
        (data) => 
        {
          this.viewModel = data as UserViewModel;
          this.model = this.toUserModel(this.viewModel);          
        },
        (error) => this.alert.setMessage(error, "danger") 
      );
  }

  onSave(viewModel: User):void 
  {   
    this.userService.UpdateUser(this.toUserViewModel(viewModel))
      .subscribe(
        data => this.alert.setMessage(data, "success"),
        error => this.alert.setMessage(error, "danger")
    );                
  }
  
  onDelete(user: User): void
  {    
    this.userService.DeleteUser(user.username)
    .subscribe(
      (data) => { 
        this.alert.setMessage(data, "success");
        this.router.navigate(['/users']); 
      },
      (error)=> this.alert.setMessage(error, "danger")
    );             
  }

  toUserModel(viewModel: UserViewModel): User
  {
    const model: User = {
      firstName: viewModel.firstName as string,
      lastName: viewModel.lastName as string,
      username: viewModel.username as string,
      email: viewModel.email as string,
      phoneNumber: viewModel.phoneNumber as string,
      isEmailConfirmed: viewModel.emailConfirmed as boolean,
      statusMessage: "",
      role: ""
    };    
    return model;
  }

  toUserViewModel(model: User)
  {
     const viewModel: UserViewModel = {
      firstName: model.firstName as string,
      lastName: model.lastName as string,
      username: model.username as string,
      email: model.email as string,
      phoneNumber: model.phoneNumber as string,
      emailConfirmed: model.isEmailConfirmed as boolean
    };  
    return viewModel;
  }  
}