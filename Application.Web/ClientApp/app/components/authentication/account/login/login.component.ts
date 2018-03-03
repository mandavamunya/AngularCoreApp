import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { Login } from './login.interface';

import { UserService } from './../../../services/user.service';
import { RoleService } from './../../../services/role.service';
import { AccountService } from './../../../services/account.service';
import { MessageService } from './../../../services/message.service';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    @Input() model: Login = <Login>{}; 
    private loginForm: FormGroup = <FormGroup>{};

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private roleService: RoleService,
        private alert: MessageService,
        private accountService: AccountService
    )
    {
        this.createForm();
    }

    createForm()
    {
        this.loginForm = this.formBuilder.group({
          email: '',
          password: '',
          rememberMe: false
        });
    }

    ngOnInit()
    {
    }
    
    prepareSaveLogin(): Login
    {
        const formModel = this.loginForm.value;
        const saveLogin: Login = {
            email: formModel.email as string,
            password: formModel.password as string,
            rememberMe: formModel.rememberMe as boolean
        };
        return saveLogin;
    }

    onsubmit(): void
    {
        this.model = this.prepareSaveLogin();        
        this.accountService.login(this.model)
            .subscribe(
                (data) => { 
                    this.alert.setMessage(data, "success");
                    //this.userService.accessLevel = this.user.role;
                    this.roleService.setRole();
                    this.router.navigate(['/Application']);                   
                },
                (error) => this.alert.setMessage(error, "danger"));        
    }   

}
