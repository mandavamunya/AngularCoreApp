import { Component, OnInit } from '@angular/core';

import { UserService } from '../../services/user.service';
import { MessageService } from '../../services/message.service';

@Component({
    selector: 'blog',
    templateUrl: './blog.component.html',
    styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
    
    constructor(
        private userService: UserService,
        private alert: MessageService
    )
    {}

    ngOnInit()
    {
    }
}
