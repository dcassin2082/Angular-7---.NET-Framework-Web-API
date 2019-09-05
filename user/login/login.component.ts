import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../shared/user.service';
import { Router, ActivatedRoute  } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {

  constructor(private service: UserService, private route: ActivatedRoute, private router: Router, private toastr: ToastrService) { }

  returnUrl: string;

  formModel = {
    UserName: '',
    Password: ''
  }
  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res);
        localStorage.setItem('username', this.formModel.UserName);
        localStorage.setItem('loggedIn', 'true');
        this.router.navigateByUrl(this.returnUrl);
        this.service.loggedIn = true;
      },
      err => {
        if (err.status == 400) {
          this.toastr.error('Incorrect Username or Password', 'Authentication Failed');
        }
        else {
          console.log(err);
        }
      }
    );
  }
}
