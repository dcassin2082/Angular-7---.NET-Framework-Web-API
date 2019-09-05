import { Injectable } from '@angular/core';
import { Employee } from './employee.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient, private spinner: Ng4LoadingSpinnerService) { }

  baseUrl: string;
  employee: Employee;
  employees: Employee[];
  positions: string[] = ["CEO", "Developer", "IT Support", "Manager", "Office", "Office Asst", "Office Help" ]

  getEmployees() {
    this.spinner.show();
    if(environment.production){
      this.baseUrl = "http://dcassin5938-001-site1.ctempurl.com/api/employees";
    }
    else{
      this.baseUrl = "https://localhost:44331/api/employees"
    }
    this.http.get(this.baseUrl).toPromise().then(x => {
      this.employees = x as Employee[];
      this.spinner.hide();
    })
  }

  postEmployee(employee: Employee) {
    return this.http.post(this.baseUrl, employee);
  }

  putEmployee(employee: Employee) {
    return this.http.put(this.baseUrl + '/' + employee.EmployeeID, employee);
  }

  deleteEmployee(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }
  
  resetEmployee() {
    this.employee = {
      EmployeeID: null,
      FullName: '',
      EMPCode: '',
      Mobile: '',
      Position: '-1',
      Rate: null,
      Email: '',
      Address: '',
      City: '',
      State: '-1',
      Zip: ''
    }
  }

}
