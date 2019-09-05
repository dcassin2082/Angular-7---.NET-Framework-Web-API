import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Contact } from './contact.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(private http: HttpClient) { }

  baseUrl: string;
  contact: Contact;
  contacts: Contact[];
  regions: string[] = ["East", "Midwest", "Central", "North", "Northeast", "Northwest", "South", "Southeast", "Southwest", "West"]
  
  getContacts() {
    if(environment.production){
      this.baseUrl = "http://dcassin5938-001-site1.ctempurl.com/api/contacts";
    }
    else{
      this.baseUrl = "https://localhost:44331/api/contacts"
    }
    this.http.get(this.baseUrl).toPromise().then(x => {
      this.contacts = x as Contact[];
    })
  }
  postContact(contact: Contact) {
    return this.http.post(this.baseUrl, contact);
  }
  putContact(contact: Contact) {
    return this.http.put(this.baseUrl + '/' + contact.ContactID, contact);
  }
  deleteContact(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }
  resetContact() {
    this.contact = {
      ContactID: null,
      FirstName: '',
      LastName: '',
      EmailAddress: '',
      Phone: '',
      CompanyName: '-1',
      OrganizationNumber: '',
      Region: '-1'
    }
  }
}
