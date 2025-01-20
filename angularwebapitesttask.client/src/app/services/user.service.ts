import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
//import { Observable } from 'rxjs';
import { CreateUserResult, CreateUser } from './../models/user.model';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  url = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  //getUsers(): Observable<UsersResult> {
  //  return this.http.get<UsersResult>(`${this.url}/users`)
  //}

  addUser(createUser: CreateUser) {
    return this.http.post<CreateUserResult>(`${this.url}/users`, createUser);
  }
}
