import { Component, OnInit } from '@angular/core';
import { HeroModel } from '../../../interface/heroModel';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { HeroesService } from '../../../services/heroes.service';

@Component({
  selector: 'app-page',
  standalone: false,
  
  templateUrl: './page.component.html',
  styleUrl: './page.component.css'
})

export class PageComponent implements OnInit{
  heroes: HeroModel[] = [];

  constructor(private service : AuthService, private router: Router, private heroesService: HeroesService){}

  ngOnInit(): void {
    if (!this.service.getIsLoggedIn()) {
      this.router.navigate(['/login']);
    } else {
      this.GetHeroList();
    }
  }

  GetHeroList(){
    this.heroesService.getAllAvailableHeroes().subscribe({
      next: (data) => {
        this.heroes = data;
        this.heroes.forEach((hero) => {
          hero.heroAbility = hero.heroAbility === "1" ? "Attacker" : "Defender";
          hero.dailyTrainingCount = hero.dailyTrainingCount == null ? 0 : hero.dailyTrainingCount;
          hero.startedTraining = hero.startedTraining == null ? '-': hero.startedTraining;
        });
      },
      error: (err) => {
        console.error('Failed to load heroes:', err);
      },
    });
  }
  AddToCart(heroId: string){
    this.heroesService.addHeroToTrainer(heroId).subscribe({
      next: (res) => {
        this.GetHeroList();
      },
      error: (err) => {
        console.log("error");
      },
    });
  }

  onClickAccount(){
    this.router.navigate(['/trainingpage']);
  }
  onClickLogOut(){
    this.service.logOut();
    this.router.navigate(['/login']);
  }
}
