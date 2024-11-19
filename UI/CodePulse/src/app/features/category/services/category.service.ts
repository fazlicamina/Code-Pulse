import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AddCategoryRequest} from "../models/add-category-request";
import {Observable} from "rxjs";
import {environment} from "../../../../environments/environment";
import {Category} from "../models/category";
import {UpdateCategoryRequest} from "../models/update-category-request";
import {CookieService} from "ngx-cookie-service";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient, private cookieService:CookieService) { }

  addCategory(model: AddCategoryRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories?addAuth=true`, model);
  }

  getAllCategories() : Observable<Category[]> {
    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/categories`);
  }

  getCategoryById(id:string): Observable<Category>{
    return this.http.get<Category>(`${environment.apiBaseUrl}/api/categories/${id}`);
  }

  updateCategory(id:string, updateCategoryRequest:UpdateCategoryRequest):Observable<Category>{
    return this.http.put<Category>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`, updateCategoryRequest)
  }

  deleteCategory(id:string):Observable<Category>{
    return this.http.delete<Category>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`);
  }

}
