import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ContactService } from '../shared/contact.service';
import { Contact } from '../shared/contact.model';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styles: []
})
export class ContactListComponent implements OnInit {

  constructor(private toastrService: ToastrService, public contactService: ContactService) { }

  p: number = 1;
  key: string = 'FirstName';
  reverse: boolean = false;
  filter: string;

  clearSearch(){
    this.filter = null;
  }
  sort(key: string){
    this.key = key;
    this.reverse = !this.reverse;
  }
  ngOnInit() {
    this.contactService.getContacts();
  }
  populateForm(contact: Contact){
    this.contactService.contact = Object.assign({}, contact);
  }
  onDelete(id: number){
   if(confirm("Are you sure?")){
    this.contactService.deleteContact(id).subscribe(x => {
      this.toastrService.warning("Contact Deleted Successfully", "Delete Contact");
      this.contactService.getContacts();
      this.contactService.resetContact();
    })
   }
  }
}
