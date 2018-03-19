import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { Time } from '../../services/time.service';
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
      private time: Time,
      private http: Http, 
      private router: Router,
      private alert: MessageService,
      private blogService: BlogService,
      @Inject("BASE_URL") private baseUrl: string
  ) {}

  ngOnInit(): void 
  {
    this.alert.hide();
    this.getBlogs(); 
  }

  private getBlogs(): void
  {
      this.blogService.getBlogs().subscribe(
          (data) => 
          {
            this.allData = data as Blog[];
            this.blogs = this.Blogs().splice(0, this.records);
            var dataLength = this.allData.length;
            if (this.records == dataLength)
            {
              this.next = false;
            }             
          },
          (error) => 
          {
              this.alert.setMessage(error, "warning")                
          }
      );        
  }

  private Blogs(): Blog[]
  {
      return Object.assign([], this.allData);
  }

  private Next(): void
  {
    this.index = this.index + this.records;
    this.blogs = this.Blogs().splice(this.index, this.records);  
    this.pagination(); 
  }

  private Previous(): void
  {
    this.index = this.index - this.records;
    this.blogs = this.Blogs().splice(this.index, this.records);   
    this.pagination();
  }

  private pagination()
  {
    var check = this.index + this.records;
    var dataLength = this.allData.length;
    if (check >= dataLength)
        this.next = true;
    else 
        this.next = false;
    if (this.index <= 0)
        this.previous = true;
    else 
        this.previous = false;
  }

  private onSelect(blog: Blog): void
  {
    this.selectedBlog = blog;
    this.blogService.selectedBlog = blog;
  }

  private gotoBlog(): void
  {
    this.router.navigate(['/blog']);
  }
}