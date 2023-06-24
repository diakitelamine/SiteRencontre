import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

member: Member | undefined;
galleryOptions: NgxGalleryOptions[] = [];
galleryImages: NgxGalleryImage[] = [];

  constructor( private memberService : MembersService, private route : ActivatedRoute) { }


  ngOnInit(): void {
    this.loadMember(); 
    this.getOptions();
   
  }

  getOptions() : void{
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }]

  }

  //permet de récupérer les images du membre
  getImages(): NgxGalleryImage[]{
    const imageUrls = [];
    for(const photo of this.member!.photos){

      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }


 //Recuperation d'un membre en fonction de son nom d'utilisateur 
  loadMember(){
    var username = this.route.snapshot.paramMap.get('username');
    this.memberService.getMember(username!).subscribe({
      next :member =>{
        this.member = member;
        this.galleryImages = this.getImages();
      } 
    })
  }

}
