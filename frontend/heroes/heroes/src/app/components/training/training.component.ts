import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { HeroModel } from '../../interface/heroModel';
import { Router } from '@angular/router';
import { HeroesService } from '../../services/heroes.service';

@Component({
  selector: 'app-training',
  standalone: false,
  
  templateUrl: './training.component.html',
  styleUrl: './training.component.css'
})
export class TrainingComponent implements OnInit{
  heroes : HeroModel[] = [];
  isStartedTraining !: Boolean;
  
  constructor(private service: AuthService, private router: Router, private heroesService: HeroesService){}

  ngOnInit(): void {
    if (!this.service.getIsLoggedIn()) {
      this.router.navigate(['/login']);
    } else {
      this.GetHeroList();
    }
  }


  GetHeroList(){
    const userName = this.GetUserName();
    if(userName){
      this.heroesService.getAllHeroesByUserName(userName).subscribe({
        next: (data) => {
          console.log(data);
          if (!data || data.length === 0) {
            this.heroes = [];
          }else{
            this.heroes = data;
            this.heroes.forEach((hero) => {
                hero.heroAbility = hero.heroAbility === "1" ? "Attacker" : "Defender";
                this.isStartedTraining = hero.startedTraining == null ? false: true;
                if(hero.startedTraining !== null){
                  hero.startedTraining = new Date(hero.startedTraining).toLocaleDateString("en-CA");
                }
            });
          }
        },
        error: (err) => {
          console.log('Failed to load heroes:', err);
        },
      });
    }
  }

  GetUserName(){
    const token = localStorage.getItem('authToken');
    if (token) {
      try {
          const payload = JSON.parse(atob(token.split('.')[1]));           
          const userName = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
          if (!userName) {
            return null;
          }
          return userName;
      } catch (error) {
        return null;
      }
    }
    console.error('Token not found in localStorage');
    return null;
  }

  onClickBack(){
    this.router.navigate(['/user'])
  }

  onClickPower(heroId : string):void
  {
    this.heroesService.TrainHero(heroId).subscribe({
      next: (res) => {
        this.GetHeroList();
      },
      error: (err) => {
        console.log("error");
      },
    });
  }
}
