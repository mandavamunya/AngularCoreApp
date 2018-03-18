import { Component, OnInit } from '@angular/core';

import { PostService } from '../services/post.service';
import { UserService } from '../services/user.service';
import { RoleService } from '../services/role.service';
import { MessageService } from '../services/message.service';

import { User } from '../interfaces/user.interface';
import { Post } from '../interfaces/post.interface';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    private model: User = <User>{};
    private featuredNews: Post = <Post>{};
    private featuredArticle: Post = <Post>{}

    constructor(
        private postService: PostService,
        private roleService: RoleService,
        private userService: UserService,
        private alert: MessageService
    )
    {
    }

    ngOnInit(): void
    {
        this.getLoggedInUser();
        this.alert.hide();
    }

    private getLoggedInUser(): void
    {    
        this.userService.getProfile().subscribe(
            (data) => 
            { 
                //this.model = data;
                //this.userService.accessLevel = this.model.role;
               // this.roleService.setRole();                  
            },
            (error) => error
        );  
    } 

}