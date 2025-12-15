import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-users-list',
  imports: [CommonModule, FormsModule],
    templateUrl: './users-list.component.html',
})
export class UsersListComponent {
  users: User[] = [];
  sortUsers: User[] = [];

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.loadUsers();
  }


  loadUsers(): void {
    this.http.get<User[]>("https://localhost:44307/api/User/GetUsers", {
      headers: {
        "x-api-key": "A6F9E3C2D7B81F4E0A9C5D6B2E1F8A7C4D0E9B6F5A3C8D2E1B7F9A4C6E0D5B8A1F2C9E7D6B4A3F5E0C8D2"
      }
    }).subscribe(data => {
      console.log(data);
      this.users = data;
      this.sortUsers = this.users;
    });
  }
  getPermissionCount(roles: MapRole[]): number {
    return roles.map(role => role.role.permissions.length).reduce((total, n) => total + n, 0);
  }
  searchUser(event: Event): void {
    const value = (event.target as HTMLInputElement).value.trim();
    this.sortUsers = value === ''
    ? this.users
    : this.users.filter(user => 
      user.id.includes(value)
      || user.userId.includes(value)
      || user.username.includes(value)
      || user.userProfile.firstName.includes(value)
      || user.userProfile.lastName.includes(value)
    )
  }
}

interface Permission {
  permission: string;
  permissionId: number;
}
interface Role {
  userRoleMappingId: string;
  permissions: Permission[];
  roleId: number;
  roleName: string;
}
interface MapRole {
  userRoleMappingId: string;
  role: Role;
}
interface UserProfile {
  firstName: string;
  lastName: string;
  age: number;
  profileId: string;
}
interface User {
  id: string;
  username: string;
  userId: string;
  userProfile: UserProfile;
  userRoleMappings: MapRole[];
}