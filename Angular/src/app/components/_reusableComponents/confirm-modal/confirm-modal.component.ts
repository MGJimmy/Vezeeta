import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.scss']
})
export class ConfirmModalComponent implements OnInit {
  @ViewChild('modalCloseBtn') modalCloseBtn;
  loading:boolean;
  title:string;
  message:string;
  itemId:any;  
  pageUrl;
  pointerToFunction:any;
  constructor(private _router:Router, private _http:HttpClient) { }

  ngOnInit(): void {
  }
  confirm(){
    this.pointerToFunction(this.itemId)
    .pipe(first())
        .subscribe(
            data => {
              this._router.routeReuseStrategy.shouldReuseRoute = () => false;
              this._router.onSameUrlNavigation = 'reload';
              this.modalCloseBtn.nativeElement.click();
              this._router.navigate([this.pageUrl]);
            },
            error => {
                //this.errorMsg = error;
                this.loading = false;
            });
  }
}
