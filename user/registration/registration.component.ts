import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService) { }

  errorMsg: string;

  ngOnInit() {
    this.service.formModel.reset();
  }

  onSubmit() {
    this.service.register().subscribe(x =>{
      if(x == 'Account Created'){
        this.service.formModel.reset();
        this.toastr.success('New user created!', 'Registration successful.');
      }
      else{
        this.toastr.error('User already exists','Registration failed.');
        this.errorMsg = "Username is already taken";
      }
      debugger;
    })
  }
}
