import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IArea, IAreaWithArea } from 'src/app/_models/_interfaces/IArea';
import { AreaService } from 'src/app/_services/area.service';
import { ConfirmModalComponent } from '../../_reusableComponents/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.scss']
})
export class AreaComponent implements OnInit {

  @ViewChild('modalCloseBtn')modalCloseBtn;
  @ViewChild('modalConfirmCloseBtn')modalConfirmCloseBtn;
  allAreaWithCity:IAreaWithArea[];
  allCity:ICity=null;
  acceptedArea:boolean=true

  currentPage:number=1;
  pageSize:number=5;
  countOfArea:number;
  numberOfPage:number;

  modelTitle:string;
  modelActionType:string;
  deleteAreaId:number;
  
  

  constructor(private _areaService:AreaService,private fb:FormBuilder,private router:Router) { }

  ngOnInit(): void {
    this.GetNumberOfPageOfAcceptedArea();
    this.GetPageingOfAcceptedArea(this.currentPage);
  }

  areaForm=this.fb.group({
    id:[""],
    name:["",[Validators.required,Validators.minLength(3)]],
    cityID:["",[Validators.required]]
  })
  GetNumberOfPageOfAcceptedArea(){
    this._areaService.countOfAcceptAreas().subscribe(data=>{
      this.countOfArea=data,
      this.numberOfPage=Math.ceil(this.countOfArea / this.pageSize)
    });
  }
  GetNumberOfPageOfNotAcceptedArea(){
    this._areaService.countOfNotAcceptAreas().subscribe(data=>{
      this.countOfArea=data,
      this.numberOfPage=Math.ceil(this.countOfArea / this.pageSize)
    });
  }
  GetPageingOfAcceptedArea(currentPage:number){
    this._areaService.getAllByPage(this.pageSize,currentPage).subscribe(data=>{
      this.allAreaWithCity=data;
      this.currentPage=currentPage;
    })
  }
  GetPageingOfNotAcceptedArea(currentPage:number){
    this._areaService.getAllByPageOfNotAccepted(this.pageSize,currentPage).subscribe(data=>{
      this.allAreaWithCity=data;
      this.currentPage=currentPage;
    })
  }
  
  getAllCity(){
    this._areaService.getAllCity().subscribe(data=>{          //delete ///update this section
      this.allCity=data;
    })
  }


  //acceptOr not
  switchState(show){
    if(show=="accept"){
      this.acceptedArea=true
      this.GetNumberOfPageOfAcceptedArea()
      this.GetPageingOfAcceptedArea(1)
    }else{
      this.acceptedArea=false
      this.GetNumberOfPageOfNotAcceptedArea()
      this.GetPageingOfNotAcceptedArea(1)
    }
  }

  
  //Model 
  openAddAreaModal(){
    this.modelTitle="Create";
    this.modelActionType="Add";
    this.areaForm.setValue({
      id:"",
      name:"",
      cityID:""
    })
    if(this.allCity == null)
      this.getAllCity();
  }

  openUpdateAreaModal(id:number){
    this.modelTitle="Update";
    this.modelActionType="Update"

    this._areaService.getById(id).subscribe(data=>{
      this.areaForm.setValue({
        id:data.id,
        name:data.name,
        cityID:data.cityID
      })
    })
    if(this.allCity ==null)
      this.getAllCity();
  }

  //add / update 
  addOrUpdateArea(){
    if(this.modelActionType=="Add"){
      const newArea:IArea={
        name:this.name.value,
        cityID:this.cityID.value
      }
      this._areaService.insertArea(newArea).subscribe(()=>{
        if(!this.acceptedArea){                                     //this.acceptedArea
          if(this.countOfArea++ % this.pageSize ==0){
            this.GetNumberOfPageOfNotAcceptedArea();                   //GetNumberOfPageOfAcceptedArea
            if(this.numberOfPage == this.currentPage)
              this.currentPage++;
          }
        }
        this.pageChange(this.currentPage);
        this.modalCloseBtn.nativeElement.click();
      })
    }
    
    else if(this.modelActionType=="Update"){
      console.error(this.idArea.value)
      const newArea:IArea={
        id:this.idArea.value,
        name:this.name.value,
        cityID:this.cityID.value
      }
      this._areaService.updateArea(this.idArea.value,newArea).subscribe(()=>{
        if(!this.acceptedArea){
          if(--this.countOfArea %this.pageSize ==0){
            this.GetNumberOfPageOfNotAcceptedArea();
            if(this.numberOfPage == this.currentPage)
              this.currentPage--;
          }
        }
        this.pageChange(this.currentPage);
        this.modalCloseBtn.nativeElement.click();
      })
    }

  }

  //DeleteArea
  openConfirmDelete(id){
    this.deleteAreaId=id;
  }
  closeConfirmDelete(){
    this.deleteAreaId=null;
  }
  confirmDelete(){
    this._areaService.deleteArea(this.deleteAreaId).subscribe(()=>{
      if(--this.countOfArea %this.pageSize ==0){
        this.GetNumberOfPageOfAcceptedArea();
        if(this.numberOfPage == this.currentPage)
          this.currentPage--;
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
    if(this.acceptedArea)
      this.GetPageingOfAcceptedArea(pageNumber);
    else
      this.GetPageingOfNotAcceptedArea(pageNumber)
  }


  //
  get idArea(){ return this.areaForm.get("id")  }
  get name(){  return this.areaForm.get("name")   }  
  get cityID(){ return this.areaForm.get("cityID")  }
}
interface ICity{
  id:number
  name:string
}
