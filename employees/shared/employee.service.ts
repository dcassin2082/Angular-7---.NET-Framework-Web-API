import { Injectable } from '@angular/core';
import { Employee } from './employee.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  baseUrl = "https://localhost:44331/api/employees";
  employee: Employee;
  employees: Employee[];
  positions: string[] = ["CEO", "Developer", "IT Support", "Manager", "Office", "Office Asst", "Office Help" ]

  getEmployees() {
    this.http.get(this.baseUrl).toPromise().then(x => {
      this.employees = x as Employee[];
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
