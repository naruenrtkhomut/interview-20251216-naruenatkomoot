import { Routes } from '@angular/router';
import { UsersListComponent } from './components/user/list/users-list.component';
import { UserDetailComponent } from './components/user/detail/user-detail.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'users' },
  { path: 'users', component: UsersListComponent },
  { path: 'users/:id', component: UserDetailComponent }
];
