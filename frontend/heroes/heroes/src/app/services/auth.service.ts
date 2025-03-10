import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedIn: boolean = false;

  constructor() {}

  getIsLoggedIn(): boolean{
    return localStorage.getItem('isLoggedIn') === 'true';
  }

  setIsLoggedIn(){
    localStorage.setItem('isLoggedIn', 'true');
  }

  logOut() {
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('authToken');
  }
}

