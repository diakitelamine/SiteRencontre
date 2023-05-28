import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../_models/member';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.baseUrl}users`);
  }

  getMember(username: string): Observable<Member> {
    return this.http.get<Member>(`${this.baseUrl}users/${username}`);
  }

  getMembersLikedByCurrentUser(predicate: string): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.baseUrl}likes?predicate=${predicate}`);
  }

  addMember(member: Member): Observable<any> {
    return this.http.post(`${this.baseUrl}users`, member);
  }

  updateMember(member: Member): Observable<any> {
    return this.http.put(`${this.baseUrl}users`, member);
  }

  deleteMember(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}users/${id}`);
  }

 

}

