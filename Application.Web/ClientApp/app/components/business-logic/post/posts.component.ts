import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRouteSnapshot, ActivatedRoute, Router } from '@angular/router';

import { PostService } from '../../services/post.service';
import { MessageService } from '../../services/message.service';

import { Post } from '../../interfaces/post.interface';

@Component({
  selector: "posts",
  templateUrl: "./posts.component.html",
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  public posts: Post[] = [];
  private allData: Post[] = [];
  private selectedPost: Post = <Post>{};


  private index = 0;
  private records = 5;

  private next = false;
  private previous = true;

  constructor(
      private http: Http, 
      private router: Router,
      private postService: PostService,
      private alert: MessageService,
      @Inject("BASE_URL") private baseUrl: string
  ) {}

  ngOnInit(): void 
  {
    this.getPosts();
  }

  private getPosts(): void
  {
      this.postService.getPosts().subscribe(
          (data) => 
          {
            this.allData = data as Post[];
            this.posts = this.Posts().splice(0, this.records);               
          },
          (error) => 
          {
              this.alert.setMessage(error, "warning")                
          }
      );        
  }

  private Posts(): Post[]
  {
      return Object.assign([], this.allData);
  }

  private Next(): void
  {
    this.index = this.index + this.records;
    this.posts = this.Posts().splice(this.index, this.records);  
    this.pagination(); 
  }

  private Previous(): void
  {
    this.index = this.index - this.records;
    this.posts = this.Posts().splice(this.index, this.records);   
    this.pagination();
  }

  private pagination():void
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

  private onSelect(post: Post): void
  {
    this.selectedPost = post;
  }

  private gotoPost(): void
  {
    this.router.navigate(['/post']);
  }
}