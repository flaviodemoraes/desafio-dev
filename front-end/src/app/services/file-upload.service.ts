import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private apiURL = environment.apiUrl;

  constructor(private http: HttpClient) { }

  uploadArquivo(file: File) {
    const formData = new FormData();
    const uri = this.apiURL + 'api/fileUpload';

    formData.append('file', file, file.name);
    return this.http.post(uri, formData);

  }
}
