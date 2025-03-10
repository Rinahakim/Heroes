import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LoginModel } from '../../../interface/loginModel';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-login',
  standalone: false,
  
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  form !: FormGroup;

  constructor(private router: Router, private service: AuthService, private userService: UserService) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }
  
  onSubmit(): void
  {
    const userName = this.form.get('userName')?.value;
    const password = this.form.get('password')?.value;
    
    const loginModel: LoginModel = {Email: userName, Password: password};
    this.userService.login(loginModel).subscribe({
      next: (response) => {
        const token = response.token;
        console.log(token);
        if(token){
          localStorage.setItem('authToken', token);
          this.service.setIsLoggedIn();
          console.log("1", this.form.get('userName')?.value);
          console.log(this.form.get('password')?.value);
          this.form.reset({
            userName: '',
            password: '',
          });
          console.log("2", this.form.get('userName')?.value);
          console.log(this.form.get('password')?.value);
          this.router.navigate(['/user']);
        }else{
          console.log("Invalid response from server");
        }
      },
      error: (error) => {
        alert("you need to sign up");
      }
  });
}

  onClickSignUp()
  {
    this.router.navigate(['/signup']);
  }
}
