import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PopupWithInputComponent } from '../popup-with-input/popup-with-input.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(public dialog: MatDialog, private snackBar: MatSnackBar, private http: HttpClient) { }

  ngOnInit() {
  }

  createFolder() {
    this.dialog.open(PopupWithInputComponent, {
      data: {
        title: 'Створення нової папки'
      },
      panelClass: 'bg-color',
      disableClose: true
    }).afterClosed().subscribe({
      next: res => {
        const model = { name: res.inputValue };
        this.http.post(environment.apiUrl + 'dashboard/create-folder', model).subscribe(() => {
          console.log('folder created')
        })
      },
      error: () => {
        this.snackBar.open('Щось пішло не так', 'Закрити', {
          duration: 5000,
        });
      }
    })
  }
}
