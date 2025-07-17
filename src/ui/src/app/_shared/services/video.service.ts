import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Video } from '../video.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.local';

@Injectable({
  providedIn: 'root'
})
export class VideoService {
  private apiUrl = `${environment.apiUrl}/videos`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Video[]> {
    return this.http.get<Video[]>(this.apiUrl);
  }

  getById(id: number): Observable<Video> {
    return this.http.get<Video>(`${this.apiUrl}/${id}`);
  }
}
