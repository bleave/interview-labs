import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface MugDto {
  id: string;
  saying: string;
  createdUtc: string;
}

@Injectable({
  providedIn: 'root'
})
export class MugMakerService {
  private readonly baseUrl = '/api/mugmaker';

  constructor(private http: HttpClient) { }

  create(saying: string): Observable<MugDto> {
    return this.http.post<MugDto>(`${this.baseUrl}/mugs`, { saying });
  }

  get(id: string): Observable<MugDto> {
    return this.http.get<MugDto>(`${this.baseUrl}/mugs/${id}`);
  }
}
