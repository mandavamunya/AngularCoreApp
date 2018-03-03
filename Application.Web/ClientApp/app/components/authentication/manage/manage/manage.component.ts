import { Component,  OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { RoleService } from '../../../services/role.service';
import { UserService } from '../../../services/user.service';

import { Option } from '../../../interfaces/option.interface';
import { User } from '../../../interfaces/user.interface';

@Component({
  selector: "manage",
  templateUrl: "./manage.component.html",
  styleUrls: ["./manage.component.css"]
})
export class ManageComponent implements OnInit {
  selectedOption: Option = <Option>{};
  model: User = <User>{};

  private options: Option[] = [
    { selection: "Profile" },
    { selection: "Password" },
    { selection: "Two-factor authentication" },
    { selection: "Roles" }
  ];

  constructor(
    private http: Http,
    private userService: UserService,
    private roleService: RoleService
  ) {
    this.onSelect(this.options[0]);
  }

  ngOnInit(): void { 
  }

  onSelect(option: Option): void {
    this.selectedOption = option;
  }
 
  hasAccess(option: string)
  {
    if (option == "Two-factor authentication" 
      && (this.roleService.manager || this.roleService.editor || this.roleService.journalist))
      return false;
    else if (option == "Roles" 
      && (this.roleService.manager || this.roleService.editor || this.roleService.journalist)) 
    return false;
    else return true;
  }
  
}