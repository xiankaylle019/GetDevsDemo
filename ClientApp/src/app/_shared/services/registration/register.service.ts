import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private baseUrl: string = "http://localhost:5000/api/RegisterService";

  constructor(private http: HttpClient) { }

  register(model:any){

    return this.http.post<any>(this.baseUrl + "/register", model).pipe(

      map((response: any) => {

          console.log(response);

          return response;

      })
  );
  }
}
