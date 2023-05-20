import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PopupWithInputComponent } from '../popup-with-input/popup-with-input.component';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { FileUploadComponent } from '../file-upload/file-upload.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  folders = [];
  selectedFolder: number;
  selectedFiles = [];

  @ViewChild('fileUpload') fileUploadComponent: FileUploadComponent;

  constructor(
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private http: HttpClient) { }

  ngOnInit() {
    this.getFolders();
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
          this.getFolders();
          this.snackBar.open('Папку створено успішно', 'Закрити', {
            duration: 5000,
          });
        })
      },
      error: () => {
        this.snackBar.open('Щось пішло не так', 'Закрити', {
          duration: 5000,
        });
      }
    })
  }

  getFolders() {
    this.http.get(environment.apiUrl + 'dashboard/get-user-folders').subscribe((folders: any) => {
      this.folders = folders;
      // this.folders = [];
      this.selectedFolder = this.selectedFolder ?? folders[0].id;
    });
  }

  getSelectedFolderName(): string {
    return this.folders.find(f => f.id == this.selectedFolder).name;
  }

  selectFolder(folderId) {
    this.selectedFolder = folderId;
    this.selectedFiles = this.folders.find(f => f.id == folderId).files;

    const queryParams = { folderId: folderId };
    this.router.navigate([], { queryParams: queryParams });
  }

  onFileUploadClick() {
    if (this.fileUploadComponent)
      this.fileUploadComponent.openFileDialog();
  }
}
