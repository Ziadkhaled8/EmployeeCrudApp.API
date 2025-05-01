import { Component } from '@angular/core';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './employee-list.page.html',
  styleUrl: './employee-list.page.scss'
})
export class EmployeeListPage {
  employees: Employee[] = [];
  searchTerm: any = '';
  page = 1;
  totalPages = 0;
  currentPage = 1;
  pages: number[] = [];
  totalCount = 0;
  isLoading = true;
  hasData = false;
  successMessage: string = '';

  private searchSubject = new Subject<string>();

  constructor(private employeeService: EmployeeService, private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.successMessage = nav?.extras?.state?.['successMessage'] ?? '';
    if (this.successMessage) {
      setTimeout(() => {
        this.successMessage = '';
      }, 3000);
      history.replaceState({}, '', this.router.url);
    }
  }
  ngOnInit() {
    this.searchSubject.pipe(debounceTime(300), distinctUntilChanged()).subscribe((term) => {
      this.page = 1;
      this.searchTerm = term;
      this.loadEmployees();
      console.log(this.employees);
    });

    this.loadEmployees();
  }

  loadEmployees(page: number = this.page) {
    this.isLoading = true;
    this.page = page;
    this.currentPage = page;

    const apiCall = this.searchTerm
      ? this.employeeService.searchEmployees(this.searchTerm, this.page)
      : this.employeeService.getAll(this.page);

    apiCall.subscribe((res) => {
      this.employees = res.result.items;
      this.totalCount = res.result.totalCount;
      this.totalPages = res.result.totalPages;
      this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
      this.hasData = this.employees.length > 0;
      this.isLoading = false;
    });
  }

  onSearch(term: string) {
    this.searchSubject.next(term);
  }

  nextPage() {
    if (this.page < this.totalPages) {
      this.page++;
      this.loadEmployees();
    }
  }

  prevPage() {
    if (this.page > 1) {
      this.page--;
      this.loadEmployees();
    }
  }
  deleteEmployee(id: number) {
    this.successMessage = '';
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.delete(id).subscribe({
        next: (res) => {
          this.loadEmployees(this.page);
          this.successMessage = 'Employee deleted successfully.';
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        }
      });
    }
  }


}


