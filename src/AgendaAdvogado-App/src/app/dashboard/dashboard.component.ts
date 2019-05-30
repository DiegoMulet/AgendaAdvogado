import { Component, OnInit } from '@angular/core';
import { ClienteService } from '../_services/cliente.service';
import { ProcessoService } from '../_services/processo.service';
import { ProcessoFiltros } from '../_models/ProcessoFiltros';
import { Processo } from '../_models/Processo';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  mediaValorProcessosCliente: number;
  qtdValorPorcessosAtivos: number;
  qtdProcessos: number;
  qtdProcessosAtivos: number;
  qtdProcessosValorAcima: number;
  titulo = 'Agenda Advogado';
  filtros: ProcessoFiltros = new ProcessoFiltros();

  constructor(
      private clienteService: ClienteService
    , private processoService: ProcessoService
  ) { }

  ngOnInit() {
    this.getMediaValorProcessosCliente();
    this.getValorProcessosAtivos();
    this.geProcessosByValor();
    this.getQtdProcessosAtivos();
    this.getValorProcessosAtivos();
  }

  getMediaValorProcessosCliente() {
    this.processoService.getMediaValorProcessosByCliente(9, 'Rio de Janeiro').subscribe(
      (qtd: number) => {
        this.mediaValorProcessosCliente = qtd;
      },
      error => { console.log(error); }
      );
  }

  getQtdProcessos() {
    this.processoService.getQtdProcessos().subscribe(
      (qtd: number) => {
        this.qtdProcessos = qtd;
      },
      error => { console.log(error); }
      );
  }

  getValorProcessosAtivos() {
    this.processoService.getValorProcessos(true).subscribe(
      (qtd: number) => {
        this.qtdValorPorcessosAtivos = qtd;
      },
      error => { console.log(error); }
      );
  }

  geProcessosByValor() {
    this.filtros.valor = 100000;
    this.processoService.getAllProcessoByFiltros(this.filtros).subscribe(
      (processos: Processo[]) => {
        this.qtdProcessosValorAcima = processos.length;
      },
      error => { console.log(error); }
      );
  }

  getQtdProcessosAtivos() {
    this.processoService.getQtdProcessosAtivos(true).subscribe(
      (qtd: number) => {
        this.qtdProcessosAtivos = qtd;
      },
      error => { console.log(error); }
      );
  }
}
