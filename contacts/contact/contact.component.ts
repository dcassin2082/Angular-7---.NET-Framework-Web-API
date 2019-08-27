import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ContactService } from '../shared/contact.service';
import { CompanyService } from 'src/app/companies/shared/company.service';
import { NgForm } from '@angular/forms';
import { Company } from 'src/app/companies/shared/company.model';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styles: []
})
export class ContactComponent implements OnInit {

  constructor(private toastrService: ToastrService, public contactService: ContactService, public companyService: CompanyService) { }

  companies: Company[];
  
  ngOnInit() {
    this.resetForm();
    this.companyService.getCompanies();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset({
        CompanyName: -1, 
        Region: -1
      });
    this.contactService.resetContact();
  }

  onSubmit(form: NgForm) {
    if (form.value.ContactID == null) {
      form.value.ContactID = 0;
      this.contactService.postContact(form.value).subscribe(x => {
        this.toastrService.success('Contact Added Successfully', 'Add Contact');
        this.contactService.getContacts();
        this.resetForm(form);
      })
    }
    else{
      this.contactService.putContact(form.value).subscribe(x => {
        this.toastrService.info('Contact Updated Successfully', 'Update Contact');
        this.contactService.getContacts();
        this.resetForm(form);
      })
    }
  }
}
