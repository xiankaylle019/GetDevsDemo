import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  model:any = {};
  errMsg: string = "";
  constructor() { }

  ngOnInit() {
  }
  register(){

  }
}
