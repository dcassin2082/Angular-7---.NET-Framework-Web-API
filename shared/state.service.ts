import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { State } from './state.model';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  constructor(private http: HttpClient) { }

  baseUrl = "https://localhost:44331/api/states";
  state: State;
  states: State[];

  getStates(){
    this.http.get(this.baseUrl).toPromise().then(x => {
      this.states = x as State[]; 
    })
  }
}
