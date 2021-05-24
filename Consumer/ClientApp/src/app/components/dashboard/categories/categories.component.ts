import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ICategory } from 'src/app/_models/_interfaces/ICategory';
import { CategoryService } from 'src/app/_services/category.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  // @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  // @ViewChild(ConfirmModalComponent) confirmModal:ConfirmModalComponent;
  hasCategories:boolean = false;
  private _categoryToUpdate:ICategory;
  allCategories:ICategory[]; 
  errorMsg:string;
  categoryForm : FormGroup;
  loading = false;
  submitted = false;
  actionName:string;
  categoriesCount:number;
  pageSize:number = 8;
  currentPageNumber:number = 1;
  numberOfPages:number; // categoriesCount / pageSize
  public response: {dbPath: ''};

  // convenience getter for easy access to form fields
  get formFields() { return this.categoryForm.controls; }
  constructor(private _categoryService:CategoryService,
    private _formBuilder: FormBuilder,
    private _router:Router) { }

  ngOnInit(): void {
    this.getCategoriesCount();
    this.categoryForm = this._formBuilder.group({
      name:['', Validators.required]
    });
    this.getSelectedPage(1);
  }

  private getCategoriesCount(){
    this._categoryService.getCategoriesCount().subscribe(
      data => {
        this.categoriesCount = data
        this.numberOfPages = Math.ceil(this.categoriesCount / this.pageSize)
        
      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }
  private onAddCategorySubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.categoryForm.invalid) {
        return;
      }

    this.loading = true;
    let newCategory:ICategory = 
    {
      id:0 ,
      name : this.formFields.name.value,
      image : this.response.dbPath,
    };
    this._categoryService.addNewCategory(newCategory)
        .pipe(first())
        .subscribe(
            data => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
               // this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }

  private onUpdateCategorySubmit(){
    this.submitted = true;

    // stop here if form is invalid
    if (this.categoryForm.invalid) {
        return;
      }

    this.loading = true;
    this._categoryToUpdate.name = this.formFields.name.value;
    if(this.response.dbPath != ''){ // if the user doesn't change the image 
      this._categoryToUpdate.image = this.response.dbPath;
    }
    console.log(this._categoryToUpdate);
    this._categoryService.updateCategory(this._categoryToUpdate.id, this._categoryToUpdate)
        .pipe(first())
        .subscribe(
            data => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
               // this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }

  onAddOrUpdateSubmit(){
    if(this.actionName == "Add"){
      this.onAddCategorySubmit();
    }else{
      this.onUpdateCategorySubmit()
    }
  }
 
  openAddCategoryModal(){
    this.formFields.name.setValue("");
    this.actionName = "Add";
  }

  openUpdateCategoryModal(categoryId){
    this.actionName = "Update";
    this._categoryService.getCategoryById(categoryId)
        .pipe(first())
        .subscribe(
            data => {
                this.categoryForm.setValue({
                  name: data.name
                }); 
                this._categoryToUpdate = data;
            },
            error => {
                this.errorMsg = error;
                this.loading = false;
            });
  }
  openDeleteCategoryModal(categoryId){
    //this._categoryToDeleteId = categoryId;
    // this.confirmModal.pointerToFunction = this._categoryService.deleteCategory
    // this.confirmModal.title = "Delete Category";
    // this.confirmModal.itemId = categoryId;
    // this.confirmModal.message = "Are you sure to delete this category";
    // this.confirmModal.pageUrl = this._router.url;
    //this.confirmModal.entityName ="category";
  }

// pagination
  counter(i: number) {
    return new Array(i);
  }
  getSelectedPage(currentPageNumber:number){
    this._categoryService.getCategoriesByPage(this.pageSize,currentPageNumber).subscribe(
      data => {
        this.allCategories = data
        this.currentPageNumber = currentPageNumber;
        if(data.length != 0)
          this.hasCategories = true;
        else
          this.hasCategories = false;

      },
      error=>
      {
       this.errorMsg = error;
      }
    ) 
  }

  // upload image
  public uploadFinished = (event) => { 
    this.response = event;
  }
  public createImgPath = (serverPath: string) => {
    return `${environment.apiUrl}/${serverPath}`;
  }
}

