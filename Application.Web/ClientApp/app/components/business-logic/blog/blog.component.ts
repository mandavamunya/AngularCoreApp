import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { Blog } from '../../interfaces/blog.interface';
import { BlogCategory } from '../../interfaces/blog-category.interface';

import { BlogService } from '../../services/blog.service';
import { MessageService } from '../../services/message.service';

@Component({
    selector: 'blog',
    templateUrl: './blog.component.html',
    styleUrls: ['./blogs.component.css']
})
export class BlogComponent implements OnInit {
    private blogs: Blog[] = [];
    private blogCategories: BlogCategory[] = [];

    constructor(
        private http: Http,
        private router: Router, 
        private route: ActivatedRoute,
        private blogService: BlogService,
        private alert: MessageService,        
        @Inject('BASE_URL') private baseUrl: string        
    )
    {}

    ngOnInit(): void
    {
        this.getBlogs();
        this.getBlogCategories();
    }

    getBlogs(): void
    {
        this.blogService.getBlogs().subscribe(
            (data) => 
            {
                this.blogs = data as Blog[];
            },
            (error) => 
            {
                this.alert.setMessage(error, "warning")                
            }
        );        
    }

    getBlogCategories(): void
    {
        this.blogService.getBlogCategory().subscribe(
            (data) => 
            {
                this.blogCategories = data as BlogCategory[];
                console.log(this.blogCategories);
            },
            (error) => 
            {
                this.alert.setMessage(error, "warning")                
            }
        );        
    }

    gotoPosts(): void
    {
        this.router.navigate(['/blog']);
    }
}
