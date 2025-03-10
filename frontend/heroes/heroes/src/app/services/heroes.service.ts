import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { HeroModel } from "../interface/heroModel";

@Injectable({
  providedIn: 'root'
})
export class HeroesService{
    private apiUrl = environment.apiUrl;

    constructor(private http : HttpClient){}

    getAllAvailableHeroes(): Observable<any>{
      const token = localStorage.getItem('authToken');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get(`${this.apiUrl}/Heroes`, {headers});
    }

    addHeroToTrainer(heroId: string){
      const token = localStorage.getItem('authToken');
      console.log(token);
      const headers = new HttpHeaders()
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.post(`${this.apiUrl}/Heroes/add-to-trainer/${heroId}`, null, { headers });
    }

    getAllHeroesByUserName(userName: string){
      const token = localStorage.getItem('authToken');
      const headers = new HttpHeaders()
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.post<HeroModel[]>(`${this.apiUrl}/Heroes/user-heroes`, `\"${userName}\"`, {headers});
    }

    TrainHero(heroId: string){
      const token = localStorage.getItem('authToken');
      const headers = new HttpHeaders()
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
      return this.http.post(`${this.apiUrl}/Heroes/train/${heroId}`, null, { headers });
    }
}