import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../_shared/services/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router,private authService:AuthService) { }

  ngOnInit() {
    if(!this.loggedIn()){
      this.router.navigateByUrl('/login')
    }
  }
  logout(){

    this.authService.userToken = null;
    localStorage.removeItem('currentUser');
    this.router.navigateByUrl('/login');
    
  }
  loggedIn(){
    return  this.authService.IsLoggedIn();
  }

}
