import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private REST_API_SERVER = 'http://localhost:5000/';

  constructor(private httpClient: HttpClient) {
  }

  public getThumbnail(): any {
    return this.httpClient.get(`${this.REST_API_SERVER}thumbnail`);
  }
}
