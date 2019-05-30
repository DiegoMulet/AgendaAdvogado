import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Processo } from '../_models/Processo';
import { ProcessoFiltros } from '../_models/ProcessoFiltros';

@Injectable({
  providedIn: 'root'
})
export class ProcessoService {

constructor(private http: HttpClient) { }

baseURL = 'http://localhost:57785/api/processo';

getAllProcesso(): Observable<Processo[]> {
  return this.http.get<Processo[]>(this.baseURL);
}

getProcessoByID(id: number): Observable<Processo> {
  return this.http.get<Processo>(`${this.baseURL}/${id}`);
}

getAllProcessoByFiltros(filtros: ProcessoFiltros): Observable<Processo[]> {
  return this.http.post<Processo[]>(`${this.baseURL}/getProcessosByFiltros/`, filtros);
}

getAllProcessoByEstadoCliente(id: number): Observable<Processo[]> {
  return this.http.get<Processo[]>(`${this.baseURL}/getProcessosByEstadoCliente/${id}`);
}

getQtdProcessos(): Observable<number> {
  return this.http.get<number>(`${this.baseURL}/getQtdProcessos`);
}

getMediaValorProcessosByCliente(id: number, estado: string): Observable<number> {
  return this.http.get<number>(`${this.baseURL}/getMediaValorProcessosByCliente/${id}/${estado}`);
}

getQtdProcessosAtivos(ativo: boolean): Observable<number> {
  return this.http.get<number>(`${this.baseURL}/getQtdProcessos/${ativo}`);
}

getValorProcessos(ativo: boolean): Observable<number> {
  return this.http.get<number>(`${this.baseURL}/getValorProcessos/${ativo}`);
}

postProcesso(processo: Processo) {
  return this.http.post(this.baseURL, processo);
}

putProcesso(processo: Processo) {
 return this.http.put(`${this.baseURL}/${processo.id}`, processo);
}

deleteProcesso(id: number) {
  return this.http.delete(`${this.baseURL}/${id}`);
}

}
