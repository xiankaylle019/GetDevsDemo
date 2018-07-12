import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_shared/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model:any = {};
  errMsg: string = "";

  constructor(private router: Router,private authService: AuthService) { }

  ngOnInit() {

  }

  logIn(){

    this.authService.login(this.model).subscribe(response => {

      this.router.navigateByUrl('/home');

    }, error => {

      this.errMsg = "failed to login";

    });
  }

  loggedIn(){
    return  this.authService.IsLoggedIn();
  }
 
}
