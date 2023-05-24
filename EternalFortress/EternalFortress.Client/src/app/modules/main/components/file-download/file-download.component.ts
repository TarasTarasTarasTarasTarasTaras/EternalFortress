import { Component, ElementRef, Input, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { saveAs } from "file-saver-es";
import { Subscription } from 'rxjs';
import { FileService } from '../../services/file.service';

@Component({
  selector: 'app-file-download',
  templateUrl: './file-download.component.html',
  styleUrls: ['./file-download.component.css']
})
export class FileDownloadComponent implements OnInit {
  @ViewChild('fileUpload') fileUpload!: ElementRef;
  @Input() fileId: number;
  @Input() folderId: number;
  @Output() downloaded = new EventEmitter();

  fileName = '';

  downloadProgress: number = 0;

  fileDownload$: Subscription;
  isDownloadInProgress: boolean = false;

  constructor(
    private fileService: FileService) { }

  ngOnInit(): void {
  }

  download(file) {
    this.fileName = file.name;
    this.isDownloadInProgress = true;

    this.downloadProgress = 0;
    let downloadProgress = (progress: number) => {
      this.downloadProgress += progress * 100;
    }

    this.fileDownload$ = this.fileService.downloadFile(file.id, file.size, downloadProgress)
      .subscribe({
        next: (blob) => {
          saveAs(blob, file.name);
          this.downloaded.emit();
        },
        error: () => {
          this.isDownloadInProgress = false;
        },
        complete: () => this.isDownloadInProgress = false
      })
  }

  cancelDownload() {
    this.isDownloadInProgress = false;
    if (this.fileDownload$) this.fileDownload$.unsubscribe();
  }
}