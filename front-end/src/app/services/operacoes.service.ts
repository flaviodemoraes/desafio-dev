import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OperacoesService {
  private apiURL = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // obterListaLojas
  obterListaLojas(page: number, limit: number) {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());

    return this.http.get(`${this.apiURL}/api/obterListaLojas`, { params });
  }

  // obterOperacoesPorLojas
  obterOperacoesPorLojas(lojaId: string, page: number, limit: number) {
    const params = new HttpParams()
      .set('lojaId', lojaId.toString())
      .set('page', page.toString())
      .set('limit', limit.toString());

    return this.http.get(`${this.apiURL}/api/obterOperacoesPorLojas`, { params });
  }
}
