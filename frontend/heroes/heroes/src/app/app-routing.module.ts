import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login/login.component';
import { SignupComponent } from './components/signup/signup/signup.component';

import { guardGuard } from './guards/guard.guard';
import { PageComponent } from './components/page-user/page/page.component';
import { TrainingComponent } from './components/training/training.component';

const routes: Routes = [
  {path: '', redirectTo: '/login', pathMatch: 'full'},
  {path:'signup', component: SignupComponent},
  {path: 'login', component: LoginComponent},
  {path: 'user', component: PageComponent, canActivate: [guardGuard]},
  {path: 'trainingpage', component: TrainingComponent, canActivate: [guardGuard]},
  {path: '**', redirectTo: '/login', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule] 
})

export class AppRoutingModule {}

