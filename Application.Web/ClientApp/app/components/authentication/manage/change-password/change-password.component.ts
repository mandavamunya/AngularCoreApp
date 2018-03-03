import { Component, Input } from '@angular/core';
import { FormArray, FormGroup, FormBuilder } from '@angular/forms';

import { ManageService } from '../../../services/manage.service';
import { MessageService } from '../../../services/message.service';
import { ChangePassword } from './change-password.interface';

@Component({
    selector: 'change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
   @Input() model: ChangePassword = <ChangePassword>{};
    private changePasswordForm: FormGroup =  <FormGroup>{};

    constructor(
        private formBuilder: FormBuilder,
        private alert: MessageService,
        private manageService: ManageService
    )
    {
        this.createForm();
    }
    
    createForm()
    {
        this.changePasswordForm = this.formBuilder.group({
            oldPassword: "",
            newPassword: "",
            confirmPassword: ""          
        });
    }

    prepareSaveChangePassword(): ChangePassword
    {
        const formModel = this.changePasswordForm.value;
        
        const saveChangePassword: ChangePassword = {
            oldPassword: formModel.oldPassword as string,
            newPassword: formModel.newPassword as string,
            confirmPassword: formModel.confirmPassword as string
        };

        return saveChangePassword;
    }

    onsubmit(): void
    {
        this.model = this.prepareSaveChangePassword();
        
        console.log(this.model);
        this.manageService.ChangePassword(this.model)
            .subscribe(
                data => this.alert.setMessage(data, "succcess"),
                error => this.alert.setMessage(error, "danger"));            
    }
}
