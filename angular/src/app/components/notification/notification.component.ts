import { NgFor, NgIf } from '@angular/common';
import { Component, Input, signal, WritableSignal } from '@angular/core';
import { Router } from '@angular/router';
import {
  AuthService,
  LPX_AUTH_SERVICE_TOKEN,
  ToolbarService,
  UserProfileService,
} from '@volo/ngx-lepton-x.core';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss',
})
export class NotificationComponent {
  @Input() notifications = [
    { id: 1, message: 'New parts created', link: '/parts' },
    {
      id: 2,
      message: 'Admin mentioned you in feedback and long text test',
      link: '/feedbacks',
    },
    { id: 3, message: 'New Test Created', link: '/tests' },
  ];

  constructor(private router: Router) {}
  showNotification: WritableSignal<boolean> = signal(false);

  toggleNotification() {
    this.showNotification.update(value => !value);
  }

  navigateHandler(path: string) {
    this.router.navigate([path]);
  }

  clearNotifications() {
    this.notifications = [];
  }
}
