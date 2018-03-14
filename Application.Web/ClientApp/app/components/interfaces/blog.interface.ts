import { Post } from './post.interface';

export interface Blog {
    id: number;
    name: string;
    isPublished: boolean;
    createdate: Date;
    publishDate: Date;  
    posts?: Post[]; 
}