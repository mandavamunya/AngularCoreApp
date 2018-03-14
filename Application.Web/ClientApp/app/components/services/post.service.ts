import { ReactiveErrors } from '@angular/forms/src/directives/reactive_errors';
import { Observable } from 'rxjs/Rx';
import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

import { Post } from '../interfaces/post.interface';

@Injectable()
export class PostService {

    constructor(
        private http: Http,
         @Inject('BASE_URL') private baseUrl: string) 
    {}

    getPost()
    {
        return this.http.get(this.baseUrl + 'api/Post/')
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }

    deletePost(id: number) 
    {     
        return this.http.delete(this.baseUrl + "api/Post/" + id)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    } 
    
    addPost(post: Post) 
    {
        return this.http.post(this.baseUrl + 'api/Post/AddPost',  post)
            .map((response: Response) => response.json())
            .catch((error) => {
                return Observable.throw(error);
            });
    }  
    
    UpdatePost(post: Post) 
    {
        return this.http.put(this.baseUrl + 'api/Post/UpdatePost',  post)
        .map((response: Response) => response.json())
        .catch((error) => {
            return Observable.throw(error);
        });
    }   
}