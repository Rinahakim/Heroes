import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const guardGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService); //inject -> allowing access components and services 
  const router = inject(Router);

  if(authService.getIsLoggedIn())
  {
    console.log(authService.getIsLoggedIn());
    return true; 
  } else{
    router.navigate(['/login']);
    return false;
  }
};
