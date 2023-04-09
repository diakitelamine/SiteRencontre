import { Component, EventEmitter, Input, Output} from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
//Transmetre un composant enfant a un parent 
@Output() cancelRegister = new EventEmitter();
 model: any = {};

constructor(private accountService : AccountService, private toastr: ToastrService){}

 ngOInit() : void{}



 register(){
  this.accountService.register(this.model).subscribe({
    next: () => {
      this.concel();
    }, error:error =>{
      this.toastr.error(error.error);
    }
  })
 }

//Annulation d'enregistrement
 concel(){
    this.cancelRegister.emit(false);
 }


}
