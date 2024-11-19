import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AddBlogPost} from "../models/add-blog-post";
import {Observable} from "rxjs";
import {environment} from "../../../../environments/environment";
import { BlogPost } from '../models/blog-post';
import {UpdateBlogPost} from "../models/update-blog-post";

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http:HttpClient) { }

  createBlogPost(data: AddBlogPost):Observable<BlogPost>{
    return this.http.post<BlogPost> (`${environment.apiBaseUrl}/api/blogposts?addAuth=true`, data);
  }

  getAllBlogPosts():Observable<BlogPost[]>{
    return this.http.get<BlogPost[]>(`${environment.apiBaseUrl}/api/blogposts`);
  }
  getBlogPostById(id: string):Observable<BlogPost>{
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${id}`);
  }

  getBlogPostByUrlHandle(urlHandle: string):Observable<BlogPost>{
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${urlHandle}`);
  }

  updateBlogPost(id:string, updatedBlogPost:UpdateBlogPost):Observable<BlogPost>{
    return this.http.put<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${id}?addAuth=true`, updatedBlogPost);
  }

  deleteBlogPost(id:string):Observable<BlogPost>{
    return this.http.delete<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${id}?addAuth=true`);
  }

}
