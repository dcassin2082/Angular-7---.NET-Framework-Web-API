import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from '../shared/employee.service';
import { Employee } from '../shared/employee.model';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styles: []
})
export class EmployeeListComponent implements OnInit {

  constructor(private toastrService: ToastrService, public employeeService: EmployeeService) { }

  p: number = 1;
  key: string = 'FullName';
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
    this.employeeService.getEmployees();
  }
  populateForm(employee: Employee){
    this.employeeService.employee = Object.assign({}, employee);
  }
  onDelete(id: number){
   if(confirm("Are you sure?")){
    this.employeeService.deleteEmployee(id).subscribe(x => {
      this.toastrService.warning("Delete Successful", "Employee");
      this.employeeService.getEmployees();
      this.employeeService.resetEmployee();
    })
   }
  }
}
