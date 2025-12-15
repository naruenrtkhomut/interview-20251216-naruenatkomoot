import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
    standalone: true,
    selector: 'app-user-detail',
    imports: [CommonModule],
    templateUrl: './user-detail.component.html',
})
export class UserDetailComponent {
    id!: string;
    user!: User;
    permissions: Permission[] = [];
    constructor(private route: ActivatedRoute, private http: HttpClient) {}

    ngOnInit(): void {
        this.id = this.route.snapshot.paramMap.get('id')!;
        this.http.get<User>(`https://localhost:5000/api/User/GetUserById/${this.id}`,{
            headers: {
                "x-api-key": "A6F9E3C2D7B81F4E0A9C5D6B2E1F8A7C4D0E9B6F5A3C8D2E1B7F9A4C6E0D5B8A1F2C9E7D6B4A3F5E0C8D2"
            }
        })
        .subscribe(data => {
            this.user = data;
            data.userRoleMappings.forEach(role => {
                role.role.permissions.forEach(permission => {
                    this.permissions.push(permission);
                    console.log(permission);
                });
            });
            console.log(this.permissions);
        });
    }
    getPermissionCount(roles: MapRole[]): number {
        return roles.map(role => role.role.permissions.length).reduce((total, n) => total + n, 0);
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