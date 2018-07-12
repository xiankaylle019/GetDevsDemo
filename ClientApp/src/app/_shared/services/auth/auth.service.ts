import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthService {

    private baseUrl: string = "http://localhost:5000/api/Auth";
    userToken: any;
    constructor(private http: HttpClient) {

    }

    login(model: any) {

        return this.http.post<any>(this.baseUrl + "/login", model).pipe(

            map((response: any) => {

                const res = response;
                console.log(response);

                if (res) {

                    localStorage.setItem("currentUser", res);
                    this.userToken = res.token;
                }
            })
        );
    }

    IsLoggedIn(){
        const token = localStorage.getItem('currentUser');
        return  !!token;
    }
}
