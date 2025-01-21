import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ProvincesResult } from './../models/province.model';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  url = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getProvinces(countryId: number): Observable<ProvincesResult> {
    return this.http.get<ProvincesResult>(`${this.url}/provinces/${countryId}`);
  }
}
