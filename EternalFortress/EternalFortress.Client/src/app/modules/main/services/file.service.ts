import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, map, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';

const baseUrl = `${environment.apiUrl}dashboard`;

@Injectable({
  providedIn: 'root'
})

export class FileService {
  private chunkSize = 100000;

  constructor(
    private http: HttpClient) { }

  uploadFile(file: File, fileId: number, folderId: number, updateProgress: Function) {
    let chunkIndex = 1;
    let chunkCount = this.getChunkCount(file.size);

    let uploads: Observable<Object>[] = [];
    for (let offset = 0; offset < file.size; offset += this.chunkSize) {
      const chunk = file.slice(offset, offset + this.chunkSize);
      let uploadSubscription = this.saveChunk(fileId, folderId, chunkIndex, chunk)
        .pipe(tap(() => {
          if (updateProgress)
            updateProgress(1 / chunkCount);
        }));
      uploads.push(uploadSubscription);

      chunkIndex++;
    }

    return forkJoin(uploads);
  }

  saveFileInfo(file): Observable<number> {
    return this.http.post<number>(`${baseUrl}/save-file-info`, file);
  }

  private saveChunk(fileId: number, folderId: number, chunkIndex: number, content: Blob): Observable<Object> {
    let formData: FormData = new FormData();
    formData.append("chunk", content);
    formData.append("fileId", fileId.toString());
    formData.append("folderId", folderId.toString());
    formData.append("chunkIndex", chunkIndex.toString());

    return this.http.post(`${baseUrl}/upload`, formData);
  }

  private getChunkCount(fileSize: number) {
    return Math.floor(fileSize / this.chunkSize) + (fileSize % this.chunkSize > 0 ? 1 : 0);
  }
  
  downloadFile(id: number, size: number, updateProgress: Function) {
    let chunkCount = this.getChunkCount(size);
    let chunks$: Observable<any>[] = [];

    for (let i = 1; i <= chunkCount; i++) {
      let chunk$ = this.downloadChunk(id, i).pipe(
        map((res: any) => {
          return res.chunk;
        }),
        tap(() => {
          updateProgress(1 / chunkCount);
        }));

      chunks$.push(chunk$);
    }

    return forkJoin(chunks$).pipe(map((blob) => {
      return new Blob(blob, { type: 'application/octect-binary' });
    }));
  }

  private downloadChunk(id: number, index: number) {
    return this.http.get(`${baseUrl}/get-chunk`, {
      params: {
        fileId: id,
        index: index
      }
    });
  }
}
