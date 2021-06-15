import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IMakeOffer } from 'src/app/_models/_interfaces/IMakeOffer';
import { IMakeOfferImage } from 'src/app/_models/_interfaces/IMakeOfferImage';
import { IOffer } from 'src/app/_models/_interfaces/IOffer';
import { ISubOffer } from 'src/app/_models/_interfaces/ISubOffer';
import { MakeOfferImageService } from 'src/app/_services/make-offer-image.service';
import { MakeOfferService } from 'src/app/_services/make-offer.service';
import { OfferService } from 'src/app/_services/offer.service';
import { SubOfferService } from 'src/app/_services/sub-offer.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-doctor-make-offer',
  templateUrl: './doctor-make-offer.component.html',
  styleUrls: ['./doctor-make-offer.component.scss']
})
export class DoctorMakeOfferComponent implements OnInit {
  
  @ViewChild('modalCloseBtn')modalCloseBtn;

  constructor(private _makeOffer:MakeOfferService,private _makeOfferImage:MakeOfferImageService,
    private _fb:FormBuilder,private _offer:OfferService,private _subOffer:SubOfferService) { }

    allOffer:IOffer[];
    makeOffer:IMakeOffer;
    modelTitle:string;
    modelActionType:string;
    allSubOffer:ISubOffer[];
    imageToInsert:IMakeOfferImage[]=[];
    allmakeOfferRelatedToDoctor:IMakeOffer[];
    url=environment.apiUrl

  ngOnInit(): void {
    this._offer.getAll().subscribe(data=>{
      this.allOffer=data;
    })
    this.load()
  }

  load(){
    this._makeOffer.GetAllRelatedToDoctor().subscribe(data=>{
      this.allmakeOfferRelatedToDoctor=data;      
    })
  }
  
  makeOfferForm=this._fb.group({
    id:[],
    title:['',Validators.required],
    numberOfSession:['',Validators.required],
    fees:[0,Validators.required],
    discount:[0,Validators.required],
    details:['',Validators.required],
    information:['',Validators.required],
    state:[true],
    offerId:[,Validators.required],
    subOfferId:[,Validators.required],   
    offerImages:[,Validators.required],
    doctorId:[]
  })








  //Model 
  openAddModal(){
    this.modelTitle="Create";
    this.modelActionType="Add";
    this.makeOfferForm.setValue({
      id:"",
      title:'',
      numberOfSession:"",
      fees:"",
      discount:"",
      details:"",
      information:"",
      state:'',
      offerId:'',
      subOfferId:'',   
      offerImages:'',
      doctorId:''
    })    
  }
  
  addOrUpdate(){
    if(this.modelActionType=="Add"){
      const newOffer:IMakeOffer={
        title:this.title.value,
        numberOfSession:this.numberOfSession.value,
        details:this.details.value,
        discount:this.discount.value,
        fees:this.fees.value,
        information:this.information.value,
        offerId:this.offerId.value,
        state:true,
        subOfferId:this.subOfferId.value,
        offerImages:this.offerImages.value
      }
      this._makeOffer.createMakeOffer(newOffer).subscribe(data=>{
          // this.imageToInsert.forEach(element => {
          //   element.MakeOfferId=data.id
          // });      
          this.modalCloseBtn.nativeElement.click();
          this.load();
      })
    }
  
    else if(this.modelActionType=="Update"){
      const newOffer:IMakeOffer={
        id:this.id.value,
        title:this.title.value,
        numberOfSession:this.numberOfSession.value,
        details:this.details.value,
        discount:this.discount.value,
        fees:this.fees.value,
        information:this.information.value,
        offerId:this.offerId.value,        
        state:JSON.parse(this.state.value),
        subOfferId:this.subOfferId.value,
        offerImages:this.offerImages.value,
        doctorId:this.doctorId.value
      }
      this._makeOffer.UpdateMakeOffer(newOffer).subscribe(()=>{        
        this.modalCloseBtn.nativeElement.click();
        this.load();
      })
    }

  }
  openUpdateModal(id:number){
    this.modelTitle="Update";
    this.modelActionType="Update"

    this._makeOffer.GetById(id).subscribe(data=>{
      console.log(data)
      this._subOffer.getAll().subscribe(sub=>{
        this.allSubOffer=sub.filter(i=>i.offerId==data.offerId)
      })
      this.makeOfferForm.setValue({
        id:data.id,
        title:data.title,
        numberOfSession:data.numberOfSession,
        fees:data.fees,
        discount:data.discount,
        details:data.details,
        information:data.information,
        state:data.state.toString(),
        offerId:data.offerId,
        subOfferId:data.subOfferId,   
        offerImages:data.offerImages,
        doctorId:data.doctorId
      })
    })
  }

  public uploadClinicImages = (event) => {
    event.forEach(element => {
      this.imageToInsert.push({id:0,image:element})
    });
    this.makeOfferForm.controls['offerImages'].setValue(this.imageToInsert);
  }
  chooseOffer(i){
    this._subOffer.getAll().subscribe(data=>{
      this.allSubOffer=data.filter(n=>n.offerId==i);
    })
  }

  get id(){return this.makeOfferForm.get('id')}
  get title(){return this.makeOfferForm.get('title')}
  get numberOfSession(){return this.makeOfferForm.get('numberOfSession')}
  get fees(){return this.makeOfferForm.get('fees')}
  get discount(){return this.makeOfferForm.get('discount')}
  get details(){return this.makeOfferForm.get('details')}
  get information(){return this.makeOfferForm.get('information')}
  get state(){return this.makeOfferForm.get('state')}
  get offerId(){return this.makeOfferForm.get('offerId')}
  get subOfferId(){return this.makeOfferForm.get('subOfferId')}
  get offerImages(){return this.makeOfferForm.get('offerImages')}
  get doctorId(){return this.makeOfferForm.get('doctorId')}

}
