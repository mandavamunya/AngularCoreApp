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
    private blog: Blog = <Blog>{}
    private blogs: Blog[] = [];
    private blogCategories: Blog[] = [];

    private activeTab: Blog = <Blog>{};
    private tabs: Blog [] = [];

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
        this.alert.hide();
    }

    private getBlogs(): void
    {
        this.blogService.getBlogs().subscribe(
            (data) => 
            {
                this.blogs = data as Blog[];
                this.activeTab = data[0] as Blog;
                this.tabs = this.categories().splice(1, this.categories().length);                 
            },
            (error) => 
            {
                this.alert.setMessage(error, "warning")                
            }
        );        
    }

    private categories(): Blog[]
    {
        return Object.assign([], this.blogs);
    }

    private selectTab(blogCategory: Blog)
    {
        this.activeTab = blogCategory;
        this.tabs = this.categories().filter(item => item !== blogCategory);
    }

    private gotoPosts(): void
    {
        this.router.navigate(['/blog']);
    }
}
