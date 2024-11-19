import {Component, OnDestroy, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {BlogPostService} from "../services/blog-post.service";
import {BlogPost} from "../models/blog-post";
import {CategoryService} from "../../category/services/category.service";
import {Category} from "../../category/models/category";
import {UpdateBlogPost} from "../models/update-blog-post";
import {ImageService} from "../../../shared/components/image-selector/image.service";

@Component({
  selector: 'app-edit-blogposts',
  templateUrl: './edit-blogposts.component.html',
  styleUrls: ['./edit-blogposts.component.css']
})
export class EditBlogpostsComponent implements OnInit, OnDestroy {

  id:string | null = null;
  routeSubscription?:Subscription;
  model?:BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories?:string[];
  updateBlogPostSubscription?:Subscription;
  getBlogPostSubscription?:Subscription;
  deleteBlogPostSubscription?:Subscription;
  isImageSelectorVisible : boolean = false;
  imageSelectSubscription?:Subscription;

  constructor(private route: ActivatedRoute,
  private blogPostService:BlogPostService,
  private categoryService: CategoryService,
  private router:Router,
  private imageService:ImageService) {}
  ngOnInit(): void {
    this.categories$=this.categoryService.getAllCategories();

    this.routeSubscription=this.route.paramMap.subscribe({
      next: (params)=>{
        this.id=params.get('id');

        if(this.id){
         this.getBlogPostSubscription= this.blogPostService.getBlogPostById(this.id).subscribe({
            next:(response)=>{
              this.model=response;
              this.selectedCategories=response.categories.map(x=>x.id);
            }
          });
        }

        this.imageSelectSubscription=this.imageService.onSelectImage()
          .subscribe({
            next:(response)=>{
              if (this.model){
                this.model.featuredImageUrl=response.url;
                this.isImageSelectorVisible=false;
              }
        }
          });

      }
    });
    }

    onFormSubmit():void{
      if(this.model && this.id){
        var updateBlogPost:UpdateBlogPost = {
          author:this.model.author,
          content:this.model.content,
          shortDescription:this.model.shortDescription,
          featuredImageUrl:this.model.featuredImageUrl,
          isVisible:this.model.isVisible,
          publishedDate:this.model.publishedDate,
          title:this.model.title,
          urlHandle:this.model.urlHandle,
          categories:this.selectedCategories??[]
        };
        this.updateBlogPostSubscription=this.blogPostService.updateBlogPost(this.id, updateBlogPost)
          .subscribe({
            next:(response)=>{
              this.router.navigateByUrl('/admin/blogposts');
            }
          });
      }
    }

    onDelete():void{
      if(this.id){
        this.deleteBlogPostSubscription=this.blogPostService.deleteBlogPost(this.id)
          .subscribe({
            next:(response)=>{
              this.router.navigateByUrl('/admin/blogposts');}
          });
      }
    }

  openImageSelector():void{
    this.isImageSelectorVisible=true;
  }

  closeImageSelector():void{
    this.isImageSelectorVisible=false;
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }

}
