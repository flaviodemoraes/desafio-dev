import { Component } from '@angular/core';
import { FileUploadService } from '../services/file-upload.service';

@Component({
  selector: 'app-file-uploads',
  templateUrl: './file-uploads.component.html',
  styleUrls: ['./file-uploads.component.scss']
})
export class FileUploadsComponent {

  selectedFile: File | null = null;

  constructor(private _fileService: FileUploadService) { }

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
