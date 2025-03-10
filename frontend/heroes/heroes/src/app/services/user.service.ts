import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { LoginModel } from "../interface/loginModel";
import { SignupModel } from "../interface/signupModel";

@Injectable({
  providedIn: 'root'
})
export class UserService{
    private apiUrl = environment.apiUrl;

    constructor(private http : HttpClient){}

    signup(signupModel: SignupModel): Observable<any> {
        return this.http.post(`${this.apiUrl}/Account/signup`, signupModel);
    }

    login(loginModel: LoginModel): Observable<any> {
        return this.http.post(`${this.apiUrl}/Account/login`, loginModel)
    }  
}