import { Component, Input } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

import { Register } from './register.interface';
import { AccountService } from "../../../services/account.service";
import { MessageService } from '../../../services/message.service';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent  {
    
    @Input() model: Register = <Register>{};
    private registerForm: FormGroup = <FormGroup>{};

    constructor(
        private formBuilder: FormBuilder,
        private alert: MessageService,  
        private accountService: AccountService      
    )
    {
        this.createForm();
    }

    createForm()
    {
        this.registerForm = this.formBuilder.group({
          email: '',
          password: '',
          confirmPassword: ''
        });
    }

    prepareSaveRegister(): Register
    {
        const formModel = this.registerForm.value;
        
        const saveRegister: Register = {
            email: formModel.email as string,
            password: formModel.password as string,
            confirmPassword: formModel.confirmPassword as string
        };

        return saveRegister;
    }

    onsubmit(): void
    {
        this.model = this.prepareSaveRegister();   
        this.accountService.Register(this.model)
            .subscribe(
                data => this.alert.setMessage(data, "success"),
                error => this.alert.setMessage(error, "danger"));            
    }
}
