import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../app/model/user.model'; 

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5244/user'; 
  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/read`); 
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}findUser`); 
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/create`, user); 
  }

  updateUser(user: User): Observable<User> {
    const id = user.id; // 
    return this.http.put<User>(`${this.apiUrl}/${id}/update`, user); 
  }

  deleteUser(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}/vasco`); 
  }
}
