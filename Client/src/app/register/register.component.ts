import { Component, EventEmitter, Input, Output} from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
//Transmetre un composant enfant a un parent 
@Output() cancelRegister = new EventEmitter();
 model: any = {};

constructor(private accountService : AccountService){}

 ngOInit() : void{}



 register(){
  this.accountService.register(this.model).subscribe({
    next: () => {
      this.concel();
    }, error: error=>console.log(error)
  })
 }

//Annulation d'enregistrement
 concel(){
    this.cancelRegister.emit(false);
 }


}
