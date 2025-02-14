import { AuthService } from '@abp/ng.core';
import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }
  alertHidden = false;
  constructor(private oAuthService: OAuthService, private authService: AuthService) {}
  hideAlert(): void {
    this.alertHidden = true;
  }
  login() {
    this.authService.navigateToLogin();
  }
}
