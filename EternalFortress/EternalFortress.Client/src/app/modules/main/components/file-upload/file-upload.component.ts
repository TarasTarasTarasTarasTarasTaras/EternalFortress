import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { FileService } from '../../services/file.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  @ViewChild('fileUpload') fileUpload!: ElementRef;;
  @Input() folderId: number;
  @Output() uploaded = new EventEmitter();

  fileName = '';
  uploadProgress: number = 0;

  fileId: number;
  fileUpload$: Subscription;

  uploadButton;

  constructor(
    public dialog: MatDialog,
    private fileService: FileService) {
  }

  ngOnInit(): void {
  }

  openFileDialog() {
    this.fileUpload.nativeElement.click();
  }

  onFileSelected(event) {
    const file: File = event.target.files[0];
    this.fileUpload.nativeElement.value = null;

    this.uploadButton = document.getElementById('uploadBtn' + this.folderId);

    if (file) {
      this.fileName = file.name;

      this.uploadFile(file);
    }
  }

  async uploadFile(file: File) {
    this.uploadButton.disabled = true;

    this.saveFileInfo(file).subscribe({
      next: async (fileId) => {
        this.fileId = fileId;
  
        this.uploadProgress = 0;
        let uploadProgress = (progress: number) => this.uploadProgress += progress * 100;
  
        this.fileUpload$ = this.fileService.uploadFile(file, fileId, this.folderId, uploadProgress).subscribe({
          next: () => {  
            this.uploaded.emit();
          },
          complete: () => {
            this.uploadButton.disabled = false;
          }
        })
      }
    })
  }

  saveFileInfo(file: File) {
    let fileDetail = {
      name: file.name,
      size: file.size,
      folderId: this.folderId
    };

    return this.fileService.saveFileInfo(fileDetail);
  }

  get isUploadInProgress() {
    return this.uploadButton?.disabled ?? false;
  }
}
