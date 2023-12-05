import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Employee } from '../Models/employee.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthenticationService } from './authentication.service';

// const jwtToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoYW16YUBnbWFpbC5jb20iLCJlbWFpbCI6ImhhbXphQGdtYWlsLmNvbSIsIm5iZiI6MTY5NjQwNzkxNSwiZXhwIjoxNjk2NDA4NTE1LCJpYXQiOjE2OTY0MDc5MTUsImlzcyI6Iklzc3VlciIsImF1ZCI6IkF1ZGllbmNlIn0.2mu2G6DHGjLfaLLgJ-ZqcMIpd8L0wMl8AvYY3ETG3ifnY5osVvxRSkEx9i1d_LARzlvBh0gfkUZrk_3eHA3-gg";


@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  private apiUrl = 'https://localhost:7211/api/Auth';

  baseApiUrl: string = environment.baseApiURL;

  constructor(private http: HttpClient, private authService: AuthenticationService) { }
  


 getAllEmployees(): Observable<Employee[]> {
  return this.http.post<any[]>(this.apiUrl + '/getallusers', {}).pipe(
    map(employees => employees.map(employee => ({
      id: employee.id,
      Name: employee.name,
      Email: employee.email,
      Contact: employee.contact,
      Salary: employee.salary,
      Department: employee.department
    })))
  );
}

addEmployee(addEmployeeRequest: Employee): Observable<Employee>{
  addEmployeeRequest.id = '00000000-0000-0000-0000-000000000000';
  return this.http.post<Employee>(this.baseApiUrl + '/api/Employees', addEmployeeRequest);
}

getEmployee(id: string): Observable<Employee>{
  return this.http.post<any>(this.baseApiUrl + '/api/Employees/' + id, id).pipe(
    map(employee => ({
      id: employee.id,
      Name: employee.name,
      Email: employee.email,
      Contact: employee.contact,
      Salary: employee.salary,
      Department: employee.department
    }))
  );
}

updateEmployee(id: string, updateEmployeeRequest: Employee):Observable<Employee>{
  return this.http.post<Employee>(this.baseApiUrl + '/api/Employees/' + id + '/update', updateEmployeeRequest);
}

deleteEmployee(id:string):Observable<Employee>{
 return this.http.post<Employee>(this.baseApiUrl + '/api/Employees/' + id + '/delete', id)
}

}
