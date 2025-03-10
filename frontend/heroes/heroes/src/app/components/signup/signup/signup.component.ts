import { Component, inject, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { group } from 'console';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { SignupModel } from '../../../interface/signupModel';

@Component({
  selector: 'app-signup',
  standalone: false,
  
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit{
  form !: FormGroup;

  constructor(private router: Router, private userService : UserService){}

  ngOnInit(): void {
    this.form = new FormGroup({
      userName: new FormControl('', [Validators.required, this.userNameValidator]),
      password: new FormControl('', [Validators.required, Validators.minLength(8), this.newPasswordValidator]),
      repeatPassword: new FormControl('', [Validators.required])
    },{validators:[this.passwordValidator]});
  }

  onSubmit():void
  {
    const signupModel: SignupModel = {
      Email: this.form.get('userName')?.value, 
      Password: this.form.get('password')?.value, 
      ConfirmPassword: this.form.get('password')?.value
    };
    this.userService.signup(signupModel).subscribe({
      next: (response) => {
        this.router.navigate(['/login']);
      },
      error: (error) => {
        console.error('failed', error);
      }
  });
  this.form.reset({
    userName: '',
    password: '',
    repeatPassword: ''
  });
  }

  newPasswordValidator(control: any): ValidationErrors | null {
    const password: string = control.value;
    const hasUpperCase = /[A-Z]/.test(password);
    const hasDigit = /[0-9]/.test(password);
    const hasNonAlphanumeric = /[^a-zA-Z0-9]/.test(password);
    if(!hasUpperCase || !hasDigit || !hasNonAlphanumeric){
      console.log(hasUpperCase ,hasDigit ,hasNonAlphanumeric);
      return {weakpassword: true};
    }
    return null;
  }

  passwordValidator(control: any): ValidationErrors | null  {
    const password: string = control.get('password')?.value;
    const repeatPassword: string = control.get('repeatPassword')?.value;

    if (password !== repeatPassword) {
      return {mismatch: true};  
    }
    return null;
  }

  userNameValidator(control: any): ValidationErrors | null  {
    const userName : string = control.value;
    const getUserNameFromLocal = localStorage.getItem(userName);
    if (getUserNameFromLocal) {
      return {takenName: true};
    }
    return null;
  }

  onClickLogin()
  {
    this.router.navigate(['/login']);
  }
}
