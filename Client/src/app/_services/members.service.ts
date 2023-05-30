import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../_models/member';
import { Observable, map, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private baseUrl = 'https://localhost:5001/api/';
  members: Member[] = [];

  constructor(private http: HttpClient) { }
   
  //Recuperation des membres
  getMembers(): Observable<Member[]> {
    //Si on a déjà des membres dans le tableau, on les retourne
    if (this.members.length > 0) return of(this.members);
    //Sinon on les récupère
    return this.http.get<Member[]>(`${this.baseUrl}users`).pipe(
      map(members => {
        //On stocke les membres dans le tableau
        this.members = members;
        return members;
      })
    );        
  }
  
  //Recuperation d'un membre
  getMember(username: string): Observable<Member> {
    //Si on a déjà des membres dans le tableau, on les retourne
    const member = this.members.find(x => x.userName === username);
    //Sinon on les récupère
     if (member !== undefined) return of(member);
    return this.http.get<Member>(`${this.baseUrl}users/${username}`);
  }

  //Ajout d'un membre
  addMember(member: Member): Observable<any> {
    return this.http.post(`${this.baseUrl}users`, member);
  }

  //Mise à jour d'un membre
  updateMember(member: Member): Observable<any> {
    return this.http.put(`${this.baseUrl}users`, member).pipe(
      map(() => {
        //On met à jour le tableau des membres
        const index = this.members.indexOf(member);
        //On met à jour le tableau des membres
        this.members[index] = {...this.members[index], ...member};
      }
    ));
  }
  
  //Suppression d'un membre
  deleteMember(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}users/${id}`);
  }

  /**
   * Permet de définir une photo comme photo principale
   * @param photoId 
   * @returns 
   */
 setMainPhoto(photoId: number): Observable<any> {
    return this.http.put(`${this.baseUrl}users/set-main-photo/${photoId}`, {});
  }

  /**
   * Supprression d'une photo
   * @param photoId
   * @returns
   */
  deletePhoto(photoId: number) {
    return this.http.delete(`${this.baseUrl}users/delete-photo/${photoId}`);
  }


}

