
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterService } from './../../_shared/services/registration/register.service';
import { NgForm } from '@angular/forms'
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  model:any = {};
  errMsg: string = "";

  @ViewChild('regForm') regForm: NgForm;

  constructor(private router: Router,private regService: RegisterService) { }

  ngOnInit() {
  }
  register(){

      this.regService.register(this.model).subscribe(response => {

        console.log(response);
        
        if(response){
          this.regForm.reset();
        }
          
      });

  }
}
