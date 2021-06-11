import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IOffer } from 'src/app/_models/_interfaces/IOffer';
import { OfferService } from 'src/app/_services/offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-offer',
  templateUrl: './offer.component.html',
  styleUrls: ['./offer.component.scss']
})
export class OfferComponent implements OnInit {

  constructor(private _offerService:OfferService,private _fb:FormBuilder,private _router:Router) { }


  @ViewChild('modalCloseBtn')modalCloseBtn;
  @ViewChild('modalConfirmCloseBtn')modalConfirmCloseBtn;

  serverUrl=environment.apiUrl;

  allOffer:IOffer[]; 
  
  currentPage:number=1;
  pageSize:number=5;
  countOfOffer:number;
  numberOfPage:number;

  modelTitle:string;
  modelActionType:string;
  deleteOfferId:number;

  ngOnInit(): void {
    this.GetNumberOfPageOfAcceptedOffer();
    this.GetPageingOfOffer(this.currentPage);
  }

  offerForm = this._fb.group({
    id:[],
    name:['', Validators.required],
    image:['',Validators.required]
  });


  GetNumberOfPageOfAcceptedOffer(){
    this._offerService.getOfferCount().subscribe(data=>{
      this.countOfOffer=data,
      this.numberOfPage=Math.ceil(this.countOfOffer / this.pageSize)
    });
  }
  GetPageingOfOffer(currentPage:number){
    this._offerService.getByPage(this.pageSize,currentPage).subscribe(data=>{
      this.allOffer=data;
      this.currentPage=currentPage;
    })
  }


  //Model 
  openAddOfferModal(){
    this.modelTitle="Create";
    this.modelActionType="Add";
    this.offerForm.setValue({
      id:"",
      name:"",
      image:""
    })
  }
  openUpdateOfferModal(id:number){
    this.modelTitle="Update";
    this.modelActionType="Update"

    this._offerService.getById(id).subscribe(data=>{
      console.log(data)
      this.offerForm.setValue({
        id:data.id,
        name:data.name,
        image:data.image
      })
    })
  }

  //add / update 
  addOrUpdateOffer(){
    if(this.modelActionType=="Add"){
      const newOffer:IOffer={
        name:this.name.value,
        image:this.image.value
      }
      this._offerService.addNewOffer(newOffer).subscribe(()=>{
          if(this.countOfOffer++ % this.pageSize ==0){
            this.GetNumberOfPageOfAcceptedOffer();
            if(this.numberOfPage == this.currentPage)
              this.currentPage++;
          }
        
        this.pageChange(this.currentPage);
        this.modalCloseBtn.nativeElement.click();
      })
    }
    
    else if(this.modelActionType=="Update"){
      const newOffer:IOffer={
        id:this.idOffer.value,
        name:this.name.value,
        image:this.image.value
      }
      this._offerService.updateOffer(newOffer).subscribe(()=>{
          // if(--this.countOfOffer %this.pageSize ==0){
          //   if(this.numberOfPage == this.currentPage)
          //     this.currentPage--;
          // }
        this.GetNumberOfPageOfAcceptedOffer();
        this.pageChange(this.currentPage);
        this.modalCloseBtn.nativeElement.click();
      })
    }

  }
  

  //DeleteOffer
  openConfirmDelete(id){
    this.deleteOfferId=id;
    console.log(id)
  }
  closeConfirmDelete(){
    this.deleteOfferId=null;
  }
  confirmDelete(){  
    this._offerService.deleteOffer(this.deleteOfferId).subscribe(()=>{
      if(--this.countOfOffer %this.pageSize ==0){
        if(this.numberOfPage == this.currentPage)
          this.currentPage--;
        this.numberOfPage=Math.ceil(this.countOfOffer / this.pageSize)
      }
      this.pageChange(this.currentPage);
      this.modalConfirmCloseBtn.nativeElement.click();
    })
  }



  uploadImage=(event)=>{
    this.offerForm.get("image").setValue(event.dbPath)
    // console.log(this.image.value)
    
  }
  






  //paganation
  counter(i:number){
    return new Array(i);
  }
  pageChange(pageNumber){
    this.GetPageingOfOffer(pageNumber);
  }
  //
  get idOffer(){  return this.offerForm.get("id")   }  
  get name(){  return this.offerForm.get("name")   }  
  get image(){ return this.offerForm.get("image")  }


}
