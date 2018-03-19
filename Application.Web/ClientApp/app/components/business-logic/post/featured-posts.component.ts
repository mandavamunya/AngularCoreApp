import { Component, OnInit } from '@angular/core';

import { Time } from '../../services/time.service';
import { PostService } from '../../services/post.service';
import { UserService } from '../../services/user.service';
import { RoleService } from '../../services/role.service';
import { MessageService } from '../../services/message.service';

import { User } from '../../interfaces/user.interface';
import { Post } from '../../interfaces/post.interface';

@Component({
    selector: 'featured-posts',
    templateUrl: './featured-posts.component.html',
    styleUrls: ['./post.component.css']
})
export class FeaturedPostsComponent implements OnInit {
    private model: User = <User>{};
    private featuredNews: Post = <Post>{};
    private featuredArticle: Post = <Post>{}

    constructor(
        private time: Time,
        private alert: MessageService,
        private postService: PostService,
        private roleService: RoleService,
        private userService: UserService
    )
    {
    }

    ngOnInit(): void
    {
        this.alert.hide();
        this.getFeaturedNews();
        this.getFeaturedArticle();
    }


    private getFeaturedNews(): void
    {
        this.postService.getFeaturedNews().subscribe(
            (data) => 
            {
                this.featuredNews = data as Post;
            },
            (error) => 
            {
                this.alert.setMessage(error, "warning")
            }
        );
    }

    private getFeaturedArticle(): void
    {
        this.postService.getFeaturedArticle().subscribe(
            (data) => 
            {
                this.featuredArticle = data as Post;
            },
            (error) => 
            {
                this.alert.setMessage(error, "warning")                
            }
        );
    }

}