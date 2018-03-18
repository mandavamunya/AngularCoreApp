import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { Blog } from '../../interfaces/blog.interface';

import { BlogService } from '../../services/blog.service';
import { MessageService } from '../../services/message.service';

@Component({
    selector: 'blog',
    templateUrl: './blog.component.html',
    styleUrls: ['./blogs.component.css']
})
export class BlogComponent implements OnInit {
    private blog: Blog = <Blog>{};

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
        this.getBlog();
    }

    getBlog(): void
    {
        this.http.get(this.baseUrl + 'api/Blog/' + this.blogService.selectedBlog.id).subscribe(result => {
            this.blog = result.json();
            console.log(this.blog);
        }, error => console.error(error));
    }

    gotoPosts(): void
    {
        this.router.navigate(['/blog']);
    }
}
