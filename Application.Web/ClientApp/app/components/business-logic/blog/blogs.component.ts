import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { BlogService } from '../../services/blog.service';
import { MessageService } from '../../services/message.service';

import { Blog } from '../../interfaces/blog.interface';

@Component({
  selector: "blogs",
  templateUrl: "./blogs.component.html",
  styleUrls: ['./blogs.component.css']
})
export class BlogsComponent implements OnInit {
  public blogs: Blog[] = [];
  private allData: Blog[] = [];
  private selectedBlog: Blog = <Blog>{};


  private index = 0;
  private records = 5;

  private next = false;
  private previous = true;

  constructor(
      private http: Http, 
      private router: Router,
      private blogService: BlogService,
      private alert: MessageService,
      @Inject("BASE_URL") private baseUrl: string
  ) {}

  ngOnInit(): void 
  {
    this.getBlogs();
  }

  getBlogs(): void
  {
      this.blogService.getBlogs().subscribe(
          (data) => 
          {
            this.allData = data as Blog[];
            this.blogs = this.Blogs().splice(0, this.records);
          },
          (error) => 
          {
              this.alert.setMessage(error, "warning")                
          }
      );        
  }

  Blogs(): Blog[]
  {
      return Object.assign([], this.allData);
  }

  Next(): void
  {
    this.index = this.index + this.records;
    this.blogs = this.Blogs().splice(this.index, this.records);  
    this.pagination(); 
  }

  Previous(): void
  {
    this.index = this.index - this.records;
    this.blogs = this.Blogs().splice(this.index, this.records);   
    this.pagination();
  }

  pagination()
  {
    var check = this.index + this.records;
    if (check >= this.allData.length)
        this.next = true;
    else 
        this.next = false;

    if (this.index <= 0)
        this.previous = true;
    else 
        this.previous = false;
  }

  onSelect(blog: Blog): void
  {
    this.selectedBlog = blog;
  }

  gotoBlog(): void
  {
    this.router.navigate(['/blog']);
  }
}