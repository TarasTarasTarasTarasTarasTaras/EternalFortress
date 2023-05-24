import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email], this.uniqueEmail.bind(this)),
    password: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    countryId: new FormControl('', Validators.required)
  });

  countries: any;

  constructor(private http: HttpClient, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    this.getCountries();
  }

  register() {
    if (this.form.invalid) {
      return;
    }

    var model = this.form.getRawValue();

    this.http.post(environment.apiUrl + 'account/register', model).subscribe(() => {
      this.http.post(environment.apiUrl + 'account/login', model).subscribe((res: any) => {
        this.setLocalStorage(res.token);
        this.toastr.success('Ви успішно зареєструвались', 'Успіх');
        this.router.navigate(['dashboard']);
      })
    })
  }

  getCountries() {
    this.http.get(environment.apiUrl + 'account/countries').subscribe(res => {
      this.countries = res;
    })
  }

  routeLogin() {
    this.router.navigate(['account/login']);
  }

  uniqueEmail(control: AbstractControl): Observable<ValidationErrors | null> {
    return this.http.get<boolean>(environment.apiUrl + `account/email-exists?email=${control.value}`).pipe(
      switchMap(res => {
        if (res) {
          return of({ emailExists: true });
        } else {
          return of(null);
        }
      })
    );
  }
  
  private setLocalStorage(token) {
    if (!token) return;

    localStorage.setItem('access_token', token);
    localStorage.setItem('login-event', 'login' + Math.random());
  }
}
