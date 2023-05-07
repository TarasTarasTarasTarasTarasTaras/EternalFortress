import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PopupWithInputComponent } from '../popup-with-input/popup-with-input.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(public dialog: MatDialog, private snackBar: MatSnackBar) { }

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
        
      },
      error: () => {
        this.snackBar.open('Щось пішло не так', 'Закрити', {
          duration: 5000,
        });
      }
    })
  }
}
