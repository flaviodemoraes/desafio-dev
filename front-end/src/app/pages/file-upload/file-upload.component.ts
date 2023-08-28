import { Component } from '@angular/core';
import { FileUploadService } from 'src/app/services/file-upload.service';

import 'devextreme/data/odata/store';
import { Router } from '@angular/router';

@Component({
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent {
  selectedFile: File | null = null;

  constructor(private _fileService: FileUploadService, private router: Router) { }

  public get fileService(): FileUploadService {
    return this._fileService;
  }

  public set fileService(value: FileUploadService) {
    this._fileService = value;
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
  }

  uploadFile() {
    if (this.selectedFile) {
      if (this.selectedFile.type === 'text/plain') {
        this.fileService.uploadArquivo(this.selectedFile)
          .subscribe(
            response => {
              console.log(response);
              alert('Arquivo enviado com sucesso!');
              this.router.navigate(['operacoes'])

            },
            error => {
              console.error(error);
              alert('Erro no envio do arquivo.');
            }
          );
      } else {
        alert('Apenas arquivos de texto (txt) são permitidos!');
      }
    } else {
      alert('Por favor, selecione um arquivo antes de enviar.');
    }
  }
}