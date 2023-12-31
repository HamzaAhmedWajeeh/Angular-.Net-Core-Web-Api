import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from 'src/app/Models/employee.model';
import { EmployeesService } from 'src/app/Services/employees.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit{

  addEmployeeRequest: Employee = {
    id: '',
    Name: '',
    Email: '',
    Contact: 0,
    Salary: 0,
    Department: ''
  };
  
  constructor(private employeeService: EmployeesService, private router: Router){

  }

  ngOnInit(): void {
      
  }

  addEmployeeMethod(){
    this.employeeService.addEmployee(this.addEmployeeRequest)
    .subscribe({
      next: (employee) =>{
        this.router.navigate(['/employees']);
      }
    })
  }

}
