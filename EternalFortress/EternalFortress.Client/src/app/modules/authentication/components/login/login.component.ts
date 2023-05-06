import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

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

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
  }

  login() {
    var model = this.form.getRawValue();

    this.http.post(environment.apiUrl + 'account/login', model).subscribe((res: any) => {
      this.setLocalStorage(res.token);
    })
  }

  routeRegister() {
    this.router.navigate(['account/register']);
  }

  private setLocalStorage(token) {
    if (!token) return;

    console.log('set token')
    
    localStorage.setItem('access_token', token);
    localStorage.setItem('login-event', 'login' + Math.random());
  }
}
