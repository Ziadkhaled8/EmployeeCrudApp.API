import { Routes } from '@angular/router';
import { EmployeeListPage } from './pages/employee-list.page';
import { EmployeeFormPage } from './pages/employee-form.page';

export const routes: Routes = [
    { path: '', component: EmployeeListPage },
    { path: 'add', component: EmployeeFormPage },
    { path: 'edit/:id', component: EmployeeFormPage },
  ];