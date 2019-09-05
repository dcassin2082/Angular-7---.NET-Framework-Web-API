import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  baseUri: string = environment.production ? "http://dcassin5938-001-site1.ctempurl.com/api/account" : "https://localhost:44331/api/account";
  loggedIn: boolean = false;
  username: string;
  returnUrl: string;
  
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    FullName: [''],
    Email: ['', Validators.email],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    // passwordMismatch
    // confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value) {
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      }
      else {
        confirmPswrdCtrl.setErrors(null);
      }
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password,
      Credits: 1000
    };
    return this.http.post(this.baseUri + '/register', body);
  }

  login(formData) {
    return this.http.post(this.baseUri + '/login', formData);
  }

  getUserProfile() {
    return this.http.get(this.baseUri + 'UserProfile');
  } 

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    allowedRoles.forEach(element => {
      if (userRole == element) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
  
  userIsAuthenticated(){
    return this.http.get(this.baseUri + 'UserProfile/IsAuthenticated');
  }
  
  resetPassword(email, password) {
    const body = {
      Email: email,
      Password: password
    }
    var requestHeader = new HttpHeaders({ 'No-Auth': 'True' });
    return this.http.post(this.baseUri + 'ForgotPassword', body, { headers: requestHeader });
  }
}
