import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'EternalFortress.Client';

  userId: number;

  constructor(private router: Router, private snackBar: MatSnackBar, private http: HttpClient) {
    this.getUserId();
  }
  
  main() {
    this.router.navigate(['']);
  }

  about() {
    this.router.navigate(['about'])
  }

  logout() {
    this.userId = 0;
    
    this.snackBar.open('Ви успішно вийшли з системи', 'Закрити', {
      duration: 5000,
    });

    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.setItem('logout-event', 'logout' + Math.random());

    this.router.navigate(['account/login']);
  }

  getUserId() {
    this.http.get(environment.apiUrl + 'account/user-id').subscribe({
      next: (res: any) => {
        this.userId = res.userId;
      }
    })
  }
}
