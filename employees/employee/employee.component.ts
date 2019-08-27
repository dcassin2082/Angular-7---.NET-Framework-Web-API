import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from '../shared/employee.service';
import { NgForm } from '@angular/forms';
import { StateService } from 'src/app/shared/state.service';
import { State } from 'src/app/shared/state.model';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styles: []
})
export class EmployeeComponent implements OnInit {

  constructor(private toastrService: ToastrService, public employeeService: EmployeeService, public stateService: StateService) { }

  states: State[];

  ngOnInit() {
    this.resetForm();
    this.stateService.getStates();
  }
  resetForm(form?: NgForm) {
    debugger;

    if (form != null) {
      form.reset({
        Position: -1, 
        State: -1
      });
    } 
    this.employeeService.resetEmployee();
  }
  onSubmit(form: NgForm){
    if(form.value.EmployeeID == null){
      form.value.EmployeeID = 0;
      this.employeeService.postEmployee(form.value).subscribe(x => {
        this.toastrService.success('Insert Success', 'Employee');
        this.employeeService.getEmployees();
        this.resetForm(form);
      })
    }
    else{
      this.employeeService.putEmployee(form.value).subscribe(x => {
        this.toastrService.info('Update Success', 'Employee');
        this.employeeService.getEmployees();
        this.resetForm(form);
      })
    }
  }
}
