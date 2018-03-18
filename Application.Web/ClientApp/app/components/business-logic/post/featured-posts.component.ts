import { Component, OnInit } from '@angular/core';

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
        private postService: PostService,
        private roleService: RoleService,
        private userService: UserService,
        private alert: MessageService
    )
    {
    }

    ngOnInit(): void
    {
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

    private hasFeaturedNews(): boolean
    {
        if (this.featuredNews.title == "" && this.featuredNews.content == "")
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }

    private hasFeaturedArticle(): boolean
    {
        if (this.featuredArticle.title == "" && this.featuredArticle.content == "")
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }

}