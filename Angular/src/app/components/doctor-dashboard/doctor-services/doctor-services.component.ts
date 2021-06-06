import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { IDoctorService, IDoctorServiceDisplay } from 'src/app/_models/_interfaces/IDoctorService';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DoctorServicesService } from 'src/app/_services/doctor-services.service';

@Component({
  selector: 'app-doctor-services',
  templateUrl: './doctor-services.component.html',
  styleUrls: ['./doctor-services.component.scss']
})
export class DoctorServicesComponent implements OnInit {

  @ViewChild('addOrUpdateModelCloseBtn') addOrUpdateModelCloseBtn;
  hasServices:boolean=false;
  SelectedServiceIdList:IDoctorService[]=[];
  DoctorServiceForm : FormGroup;

  ServicesList:IDoctorService[]=[];
  // =[{id:"1",Name:"service1",byadmin:"true"},{id:"2",Name:"service2",byadmin:"true"},{id:"3",Name:"service3",byadmin:"true"},{id:"4",Name:"service4",byadmin:"true"},{id:"5",Name:"service5",byadmin:"true"},{id:"6",Name:"service6",byadmin:"true"},{id:"7",Name:"service7",byadmin:"false"},{id:"8",Name:"service8",byadmin:"true"},{id:"9",Name:"service9",byadmin:"true"},{id:"10",Name:"service10",byadmin:"true"}];
  ServicesList2=[{id:"11",Name:"service1"},{id:"12",Name:"service2"},{id:"13",Name:"service3"},{id:"14",Name:"service4"},{id:"15",Name:"service5"},{id:"16",Name:"service6"},{id:"17",Name:"service7"},{id:"8",Name:"service8"}];

  NewServiceList:IDoctorServiceDisplay[]=[];

  constructor(private _authenticationService:AuthenticationService,
    private _doctorServices:DoctorServicesService,
    private _formBuilder: FormBuilder,
    private _router:Router) 
    {
      
    }

  get formFields() { return this.DoctorServiceForm.controls; }

  ngOnInit(): void {
    this.DoctorServiceForm = this._formBuilder.group({
      name:['', Validators.required],
   
    });

    this._doctorServices.getAllDoctorServices().subscribe(data=>
      {
        this.ServicesList=data;
        console.log(this.ServicesList.length);
       
      })

      this._authenticationService.GetMyservices().subscribe(data=>
        {
          if(data.length !==0)
          {
            console.log("my services done");
            this.SelectedServiceIdList=data;
            console.log(this.SelectedServiceIdList);
            this.hasServices=true;
            console.log(data);
             this.setchecked();
          }
          else
          {
            this.hasServices=false;
            console.log("doctor has not services");
          }
         
        },err=>
        {
          console.log("errrrrrr");
        })
        

        
  }

  setchecked(){
    console.log(this.ServicesList.length);
    for (var i = 0; i < this.ServicesList.length; i++) {

      var ismatch = false; 
      console.log(this.SelectedServiceIdList.length);
      for (var j = 0; j < this.SelectedServiceIdList.length; j++) {

        if (this.ServicesList[i].id == this.SelectedServiceIdList[j].id) {
          ismatch = true;
          let servicedisplay:IDoctorServiceDisplay= 
          {
            id:this.ServicesList[i].id,
            byAdmin:this.ServicesList[i].byAdmin,
            name:this.ServicesList[i].name,
            checked:true,

          }
          console.log("xccccccccccc");
          this.NewServiceList.push(servicedisplay);
          break;
        }//End if
        }
      if (!ismatch) {
        let servicedisplay:IDoctorServiceDisplay= 
          {
            id:this.ServicesList[i].id,
            byAdmin:this.ServicesList[i].byAdmin,
            name:this.ServicesList[i].name,
            checked:false,

          }
          this.NewServiceList.push(servicedisplay);
      } //End if
    }
    console.log(this.NewServiceList);
  }

  changeStatus(serv,e)
  {
    console.log(serv);
    console.log(e);
    console.log(e.target.checked);
    let service:IDoctorService=
    {
       id:serv.id,
      name:serv.name,
      byAdmin:serv.byAdmin
    };
    if(e.target.checked==true)
    {
      this.SelectedServiceIdList.push(service);
    }
    else
    {
      console.log("enter else");
      this.RemoveElementFromSelectedList(service.id);
    }
    console.log(this.SelectedServiceIdList);
  }

  RemoveElementFromSelectedList(key: number) {
    this.SelectedServiceIdList.forEach((value,index)=>{
        if(value.id==key) this.SelectedServiceIdList.splice(index,1);
    });
} 
//   RemoveElementFromArray(element) {
//     this.SelectedServiceIdList.forEach((value,index)=>{
//         if(value==element) this.SelectedServiceIdList.splice(index,1);
//     });
// }
  onAddSubmit()
  {
   console.log("add");
   this._authenticationService.addservice(this.SelectedServiceIdList).subscribe(data=>
    {
      console.log("done");
      console.log(data);
    },err=>
    {
      console.log("errrrrrr");
    })
   console.log(this.SelectedServiceIdList);
  }
  onEditSubmit()
  {
    this._authenticationService.Deleteservices().subscribe(data=>
      {
        console.log("done");
        console.log(data);
        this._authenticationService.addservice(this.SelectedServiceIdList).subscribe(data=>
          {
            console.log("done");
            console.log(data);
          },err=>
          {
            console.log("errrrrrr in add");
          })
        
      },err=>
      {
        console.log("errrrrrr in delete");
      })
  }

  AddNewService()
  {
    if (this.DoctorServiceForm.invalid) {
      return;
    }
    let newDoctorService:IDoctorService = 
    {
      id:0 ,
      name : this.formFields.name.value,
      byAdmin:false

    };
    this._doctorServices.addNewDoctorService(newDoctorService)
        .pipe(first())
        .subscribe(
            data => {
                this._router.routeReuseStrategy.shouldReuseRoute = () => false;
                this._router.onSameUrlNavigation = 'reload';
                this.addOrUpdateModelCloseBtn.nativeElement.click();
                this._router.navigate([this._router.url]);
                console.log("added")
            },
            error => {
                
            });
    console.log("add newdoctorService");
  }

  checkExist(service)
  {
  
    
    this.SelectedServiceIdList.forEach(element => {
    if(service.id==element.id)
    {
      console.log("enter if");
      return true;
    }
      
  });
   return false;
  }
  
}
