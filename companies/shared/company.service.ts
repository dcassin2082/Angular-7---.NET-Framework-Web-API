import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Company } from './company.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpClient) { }

  baseUrl = "https://localhost:44331/api/companies";
  company: Company;
  companies: Company[];

  getCompanies() {
    this.http.get(this.baseUrl).toPromise().then(x => {
      this.companies = x as Company[];
    })
  }
  postCompany(company: Company) {
    return this.http.post(this.baseUrl, company);
  }
  putCompany(company: Company) {
    return this.http.put(this.baseUrl + '/' + company.CompanyId, company);
  }
  deleteCompany(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }
  resetCompany() {
    this.company = {
      CompanyId: null,
      CompanyName: '',
      OrganizationNumber: '',
      Region: '',
      Address1: '',
      Address2: '',
      City: '',
      State: '-1',
      Zip: ''
    }
  }
}
