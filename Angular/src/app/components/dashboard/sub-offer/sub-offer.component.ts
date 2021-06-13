import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IOffer } from 'src/app/_models/_interfaces/IOffer';
import { ISubOffer } from 'src/app/_models/_interfaces/ISubOffer';
import { OfferService } from 'src/app/_services/offer.service';
import { SubOfferService } from 'src/app/_services/sub-offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-sub-offer',
  templateUrl: './sub-offer.component.html',
  styleUrls: ['./sub-offer.component.scss']
})
export class SubOfferComponent implements OnInit {

  constructor(private _subOfferService:SubOfferService,private _fb:FormBuilder,private _router:Router
    ,private _offerServic:OfferService) { }

  @ViewChild('modalCloseBtn')modalCloseBtn;
  @ViewChild('modalConfirmCloseBtn')modalConfirmCloseBtn;

  allOffer:IOffer[];
  allSubOffer; 
  
  currentPage:number=1;
  pageSize:number=5;
  countOfSubOffer:number;
  numberOfPage:number;

  modelTitle:string;
  modelActionType:string;
  deleteSubOfferId:number;

  
  ngOnInit(): void {
    this.GetNumberOfPageOfSubOffer();
    this.GetPageingOfSubOffer(this.currentPage);
    this._offerServic.getAll().subscribe(data=>{this.allOffer=data});
  }

  subOfferForm = this._fb.group({
    id:[],
    name:['', Validators.required],
    offerId:['',Validators.required]
  });

  GetNumberOfPageOfSubOffer(){
    this._subOfferService.getSubOfferCount().subscribe(data=>{
      this.countOfSubOffer=data,
      this.numberOfPage=Math.ceil(this.countOfSubOffer / this.pageSize)
    });
  }
  GetPageingOfSubOffer(currentPage:number){
    this._subOfferService.getByPage(this.pageSize,currentPage).subscribe(data=>{
      this.allSubOffer=data;
      this.currentPage=currentPage;
    })
  }


  //Model 
  openAddSubOfferModal(){
    this.modelTitle="Create";
    this.modelActionType="Add";
    this.subOfferForm.setValue({
      id:"",
      name:"",
      offerId:""
    })
  }
  openUpdateSubOfferModal(id:number){
    this.modelTitle="Update";
    this.modelActionType="Update"

    this._subOfferService.getById(id).subscribe(data=>{
      console.log(data)
      this.subOfferForm.setValue({
        id:data.id,
        name:data.name,
        offerId:data.offerId
      })
    })
  }

  //add / update 
  addOrUpdateSubOffer(){
    if(this.modelActionType=="Add"){
      const newSubOffer:ISubOffer={
        name:this.name.value,
        offerId:this.offerId.value
      }
      this._subOfferService.addNewSubOffer(newSubOffer).subscribe(()=>{
          if(this.countOfSubOffer++ % this.pageSize ==0){
            this.GetNumberOfPageOfSubOffer();
            if(this.numberOfPage == this.currentPage)
              this.currentPage++;
          }
        
        this.pageChange(this.currentPage);
        this.modalCloseBtn.nativeElement.click();
      })
    }
    else if(this.modelActionType=="Update"){
      const newSubOffer:ISubOffer={
        id:this.subOfferId.value,
        name:this.name.value,
        offerId:this.offerId.value
      }
      this._subOfferService.updateSubOffer(newSubOffer).subscribe(()=>{
          // if(--this.countOfOffer %this.pageSize ==0){
          //   if(this.numberOfPage == this.currentPage)
          //     this.currentPage--;
          // }
        this.GetNumberOfPageOfSubOffer();
        this.pageChange(this.currentPage);
        this.modalCloseBtn.nativeElement.click();
      })
    }
  }


  //DeleteOffer
  openConfirmDelete(id){
    this.deleteSubOfferId=id;
  }
  closeConfirmDelete(){
    this.deleteSubOfferId=null;
  }
  confirmDelete(){  
    this._subOfferService.deleteSubOffer(this.deleteSubOfferId).subscribe(()=>{
      if(--this.countOfSubOffer %this.pageSize ==0){
        if(this.numberOfPage == this.currentPage)
          this.currentPage--;
        this.numberOfPage=Math.ceil(this.countOfSubOffer / this.pageSize)
      }
      this.pageChange(this.currentPage);
      this.modalConfirmCloseBtn.nativeElement.click();
    })
  }













  //paganation
  counter(i:number){
    return new Array(i);
  }
  pageChange(pageNumber){
    this.GetPageingOfSubOffer(pageNumber);
  }
  //
  get subOfferId(){  return this.subOfferForm.get("id")   }  
  get name(){  return this.subOfferForm.get("name")   }  
  get offerId(){ return this.subOfferForm.get("offerId")  }

}
