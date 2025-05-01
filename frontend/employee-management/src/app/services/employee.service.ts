import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';
import { CreateEmployee } from '../models/CreateEmployee';
import { PagedResult } from '../models/PagedResult';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private baseUrl = 'https://localhost:7240/api/Employee';
  private pageSize = 10;
  constructor(private http: HttpClient) { }
  getAll(page: number): Observable<PagedResult<Employee[]>> {
    return this.http.get<PagedResult<Employee[]>>(`${this.baseUrl}/${page},${this.pageSize}`);
  }

  getById(id: number): Observable<any> {
    return this.http.get<Employee>(`${this.baseUrl}/${id}`);
  }

  create(employee: CreateEmployee): Observable<any> {
    return this.http.post(this.baseUrl, employee);
  }

  update(employee: Employee): Observable<any> {
    return this.http.put(this.baseUrl, employee);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
  searchEmployees(search: string, page: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/search/${search},${page},${this.pageSize}`);
  }
}
