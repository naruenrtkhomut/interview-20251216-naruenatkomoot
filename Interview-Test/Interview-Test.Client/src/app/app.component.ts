import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  template: `
    <header>
      <div class="container">
        <a routerLink="/users" style="color:white; text-decoration:none; font-weight:600;">Interview Test - Users</a>
      </div>
    </header>
    <main class="container">
      <router-outlet></router-outlet>
    </main>
  `
})
export class AppComponent {}
