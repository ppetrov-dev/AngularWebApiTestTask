import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { CountriesResult } from './../models/country.model';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  url = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getCountries(): Observable<CountriesResult> {
    return this.http.get<CountriesResult>(`${this.url}/countries`)
  }
}
