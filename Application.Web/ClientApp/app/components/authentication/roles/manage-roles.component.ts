import { UserComponent } from '../user/user.component';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { UserService } from './../../services/user.service';
import { MessageService } from './../../services/message.service';

import { UserRole, Role } from './../../interfaces/roles.interface';

@Component({
  selector: "manage-roles",
  templateUrl: "./manage-roles.component.html",
  styleUrls: ["./manage-roles.component.css"]
})
export class ManageRolesComponent implements OnInit {
  user: UserRole = <UserRole>{};
  roles: string[] = [];
  selectedRole: string = <string>{};
  
  email: string;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private alert: MessageService
  ) {
    this.email = route.snapshot.params.email;
  }

  ngOnInit() {
    if (this.email == null || this.email == "") { 
      this.getUserRoles()
      this.getOtherRoles();
    } else {
      this.getUserRolesByUsername();
      this.getOtherRolesByUsername();
    }
  }

  onChange(role: string)
  {
    this.selectedRole = role;
  }

  assignRole()
  {
    this.userService.AssignRole(this.user)
    .subscribe(
      (data) => { 
        this.alert.setMessage(data, "success");
        this.user.role = this.selectedRole;  
      },
      (error) => this.alert.setMessage(error,"danger"));     
  }

  assignRoles()
  {
    this.user.role = this.selectedRole;
    this.userService.AssignRole(this.user)
    .subscribe(
      (data) => { 
        this.alert.setMessage(data, "success");
      },
      (error) => this.alert.setMessage(error, "danger"))    
  }

  removeRole(role: string)
  {
    this.userService.DeleteRole(this.toUserWithSingleRole(this.user.username, role))
    .subscribe(
      (data) => { 
        this.alert.setMessage(data, "success")
        this.user.role = ""; 
        this.getUserRolesByUsername();
      },
      (error) => { this.alert.setMessage(error, "danger") }
    );
  }

  getUserRoles() {
    this.userService.UserRoles().subscribe(
      data => this.user = data,
      error => this.alert.setMessage(error, "danger") 
    );
  }

  getUserRolesByUsername() {
    this.userService.UserRolesByUsername(this.email).subscribe(
      data => this.user = data,
      error => this.alert.setMessage(error,"danger")
    );
  }

  getOtherRolesByUsername() {
    this.userService.OtherRolesByUsername(this.email).subscribe(
      data => this.roles = data,
      error => this.alert.setMessage(error, "danger") 
    );
  }

  getOtherRoles() {
    this.userService.OtherRoles().subscribe(
      data => this.roles = data,
      error => this.alert.setMessage(error, "danger")
    );
  }
  
  toUserWithSingleRole(username: string, role: string)
  {
    const user: UserRole = {  
      username: username,
      role: role
    }
    return user;
  }

}