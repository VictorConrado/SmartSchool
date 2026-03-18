import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Professor } from '../models/Professor';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {

  baseURL = `${environment.mainUrlAPI}Professor`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.baseURL);
  }

  getById(id: number): Observable<Professor> {
    return this.http.get<Professor>(`${this.baseURL}/byId/${id}`);
  }

  getByAlunoId(alunoId: number): Observable<Professor[]> {
    return this.http.get<Professor[]>(`${this.baseURL}/byaluno/${alunoId}`);
  }

  post(professor: Professor): Observable<Professor> {
    return this.http.post<Professor>(this.baseURL, professor);
  }

  put(professor: Professor): Observable<Professor> {
    return this.http.put<Professor>(`${this.baseURL}/${professor.id}`, professor);
  }

  patch(id: number, professor: any) {
    return this.http.patch(`${this.baseURL}/${id}`, professor);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}