import { Component, OnInit } from '@angular/core';

import { UserService } from './../services/user.service';
import { MessageService } from './../services/message.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    
    constructor(
        private userService: UserService,
        private alert: MessageService
    )
    {}

    ngOnInit()
    {
    }
}
