import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToastrService } from 'ngx-toastr';

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

  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar, private toastr: ToastrService) { }

  ngOnInit() {
  }

  login() {
    if (this.form.invalid) {
      return;
    }
    
    var model = this.form.getRawValue();

    this.http.post(environment.apiUrl + 'account/login', model).subscribe({
      next: (res: any) => {
        this.setLocalStorage(res.token);
        this.router.navigate(['dashboard']);
        this.toastr.success('Ви успішно авторизувались', 'Успіх');
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
}
