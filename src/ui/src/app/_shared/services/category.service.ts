import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../cateogry.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.local';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
    private apiUrl = `${environment.apiUrl}/categories`;
    
    constructor(private http: HttpClient) {}
    
    getAll(): Observable<Category[]> {
        return this.http.get<Category[]>(this.apiUrl);
    }
}