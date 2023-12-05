import { Component } from '@angular/core';
import { AuthenticationService } from '../../Services/authentication.service';

@Component({
  selector: 'app-login',
  template: `
    <form (submit)="login()">
      <input type="text" [(ngModel)]="UserName" name="UserName" placeholder="Email" required><br>
      <input type="password" [(ngModel)]="Password" name="Password" placeholder="Password" required><br>
      <button type="submit">Login</button>
    </form>
  `
})
export class LoginComponent {
  UserName: string = 'hamza@gmail.com';
  Password: string = 'hello';

  constructor(private authService: AuthenticationService) {}

  login() {
    this.authService.login(this.UserName, this.Password);
  }
}
