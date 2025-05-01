import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Employee } from '../models/employee';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './employee-form.page.html',
  styleUrl: './employee-form.page.scss'
})
export class EmployeeFormPage implements OnInit {
  form!: FormGroup;
  isEdit = false;
  employeeId: number | null = null;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(30)]],
      lastName: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.email]],
      position: ['',[Validators.required]]
    });
  
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      const id = Number(idParam);
      this.isEdit = true;
      this.employeeId = id;
  
      this.employeeService.getById(id).subscribe({
        next: (res) => {
          const employee = res.result;
          this.form.patchValue({
            firstName: employee.firstName,
            lastName: employee.lastName,
            email: employee.email,
            position: employee.position
          });
          console.log(this.form);
          console.log(res);
        },
        error: (err) => {
          this.errorMessage = err?.error?.error || 'Failed to load employee data.';
        }
      });
    }
  }
  

  submitForm(): void {
    this.errorMessage = '';
  
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.errorMessage = 'Please fill out all required fields correctly.';
      return;
    }
  
    const employeeData = this.form.value;
  
    if (this.isEdit && this.employeeId) {
      const updatedEmployee: Employee = {
        id: this.employeeId,
        ...employeeData
      };
  
      this.employeeService.update(updatedEmployee).subscribe({
        next: () => {
          this.router.navigate(['/'], {
            state: { successMessage: 'Employee updated successfully!' }
          });
        },
        error: (error) => {
          this.errorMessage = error?.error?.error || 'An unexpected error occurred while updating.';
        }
      });
    } else {
      this.employeeService.create(employeeData).subscribe({
        next: () => {
          this.router.navigate(['/'], {
            state: { successMessage: 'Employee updated successfully!' }
          });
        },
        error: (error) => {
          this.errorMessage = error?.error?.error || 'An unexpected error occurred while creating.';
        }
      });
    }
  }
  
  
}
