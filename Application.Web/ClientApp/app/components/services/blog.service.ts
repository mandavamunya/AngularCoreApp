import { ReactiveErrors } from '@angular/forms/src/directives/reactive_errors';
import { Observable } from 'rxjs/Rx';
import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

import { Blog } from '../interfaces/blog.interface';

@Injectable()
export class BlogService {
    
    public selectedBlog: Blog = <Blog>{};

    constructor(
        private http: Http,
         @Inject('BASE_URL') private baseUrl: string) 
    {}

    getBlog()
    {
        return this.http.get(this.baseUrl + 'api/Blog/')
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    deleteBlog(id: number) 
    {     
        return this.http.delete(this.baseUrl + "api/Blog/" + id)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    } 
    
    addBlog(blog: Blog) 
    {
        return this.http.post(this.baseUrl + 'api/Blog/AddBlog',  blog)
            .map((response: Response) => response.json())
            .catch((error) => {
                return Observable.throw(error);
            });
    }  
    
    UpdateBlog(blog: Blog) 
    {
        return this.http.put(this.baseUrl + 'api/Blog/UpdateBlog',  blog)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }   
}