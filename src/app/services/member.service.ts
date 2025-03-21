import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  httpClient = inject(HttpClient);

  constructor() { }

  register(form: any){
    return this.httpClient.post(environment.baseApiUrl + '/member', form);
  }
}
