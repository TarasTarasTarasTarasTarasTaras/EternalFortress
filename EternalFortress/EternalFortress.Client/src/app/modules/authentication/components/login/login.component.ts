import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar, private appComponent: AppComponent) { }

  ngOnInit() {
    this.getUserId();
  }

  getUserId() {
    this.http.get(environment.apiUrl + 'account/user-id').subscribe({
      next: (res: any) => {
        if (res?.userId) {
          this.router.navigate(['dashboard']);
        }
        else {
          this.router.navigate(['account/login']);
        } 
      }
    })
  }

  login() {
    if (this.form.invalid) {
      return;
    }
    
    var model = this.form.getRawValue();

    this.http.post(environment.apiUrl + 'account/login', model).subscribe({
      next: (res: any) => {
        this.setLocalStorage(res.token);
        this.updateUserId();
        
        this.router.navigate(['dashboard']);

        this.snackBar.open('Ви успішно авторизувались', 'Закрити', {
          duration: 5000,
        });
      },
      error: () => {
        this.snackBar.open('Невірний Email/Пароль', 'Закрити', {
          duration: 5000,
        });
      }
    })
  }

  routeRegister() {
    this.router.navigate(['account/register']);
  }

  private setLocalStorage(token) {
    if (!token) return;
    
    localStorage.setItem('access_token', token);
    localStorage.setItem('login-event', 'login' + Math.random());
  }

  private updateUserId() {
    this.http.get(environment.apiUrl + 'account/user-id').subscribe({
      next: (res: any) => {
        this.appComponent.userId = res?.userId;
      }
    })
  }
}
