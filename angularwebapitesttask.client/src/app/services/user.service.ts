import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
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

  addUser(createUser: CreateUser): Observable<CreateUserResult> {
    return this.http.post<CreateUserResult>(`${this.url}/users`, createUser)
      .pipe(
        catchError(this.handleError),
        map((result: CreateUserResult) => {
          if (result.error)
            throw new Error(result.error);
          else
            return result;
        })
      );
  }
  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, message is: ${error.message} `;
      if (error.error && error.error.detail) {
        errorMessage += `. Detailed error is: ${error.error.detail}`
      }
      else if (typeof error.error === 'string') {
        errorMessage = `${error.error}`;
      }
    }
    console.error(errorMessage);
    return throwError(() => errorMessage)
  }
}
