import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';

export interface Thumbnail {
  id: number;
  fileName: string;
  fileContents: string;
}

export interface Message {
  content: string;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardDataService {

  private REST_API_SERVER = 'http://localhost:5000/videotools';
  private HEADER = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private httpClient: HttpClient) {
  }

  public getThumbnails(): any {
    return this.httpClient.post<Array<Thumbnail>>(
      `${this.REST_API_SERVER}/thumbnails`,
      null,
      {headers: this.HEADER});
  }

  public getH264(): Observable<Message> {
    return this.httpClient.post<Message>(
      `${this.REST_API_SERVER}/hd-264`,
      null,
      {headers: this.HEADER});
  }

  public getMultibitHls(): Observable<Message> {
    return this.httpClient.post<Message>(
      `${this.REST_API_SERVER}/hls-files`,
      null,
      {headers: this.HEADER});
  }
}
