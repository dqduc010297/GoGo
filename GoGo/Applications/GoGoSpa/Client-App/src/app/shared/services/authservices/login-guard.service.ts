import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class LoginGuardService implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }

  canActivate(): boolean {
    const token = localStorage.getItem('tokenKey');
    if (token != null || token != undefined) {
      this.router.navigate(['home']);
      return false;
    }
    return true;
  }
}