import { Component, OnInit} from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{

model: any = {};



constructor(public acccountService: AccountService){}

ngOnInit(): void {
  
}

login() {
  this.acccountService.login(this.model).subscribe(
    response => {
      console.log(response);
    },
    err => {
      console.error('Une erreur est survenue lors de la connexion', err);
    }
  );
}

logout(){
  this.acccountService.logout();
}

}
