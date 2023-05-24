import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  userId: number = 0;

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit() {
    this.getUserId();
  }

  start() {
    if (!this.userId) {
      this.router.navigate(['account/register']);
    }
    else {
      this.router.navigate(['dashboard']);
    }
  }

  getUserId() {
    this.http.get(environment.apiUrl + 'account/user-id').subscribe({
      next: (res: any) => {
        this.userId = res.userId;
      }
    })
  }
}
