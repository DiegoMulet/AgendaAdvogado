import { Component, OnInit } from '@angular/core';
import { Processo } from '../_models/Processo';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProcessoService } from '../_services/processo.service';
import { BsModalService, DateFormatter } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ProcessoFiltros } from '../_models/ProcessoFiltros';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-processo',
  templateUrl: './processo.component.html',
  styleUrls: ['./processo.component.css']
})
export class ProcessoComponent implements OnInit {

  processos: Processo[];
  processo: Processo;
  registerForm: FormGroup;
  titulo = 'Processos';
  filtro: string;
  filtros: ProcessoFiltros = new ProcessoFiltros();

  constructor(
        private processoService: ProcessoService
      , private fb: FormBuilder
      , private toastr: ToastrService
      , private datePipe: DatePipe
  ) { }

  ngOnInit() {
    this.getProcessos();
  }

  getProcessos() {
    this.processoService.getAllProcesso().subscribe(
      (processos: Processo[]) => {
        this.processos = processos;
      },
      error => { console.log(error); }
      );
    }

    getProcessosData() {
      this.filtros = new ProcessoFiltros();
      this.filtros.dataCriacao = new Date(this.datePipe.transform(this.filtro, 'dd/MM/yyyy'));
      this.processoService.getAllProcessoByFiltros(this.filtros).subscribe(
        (processos: Processo[]) => {
          this.processos = processos;
        },
        error => { console.log(error); }
        );
      }

      getProcessosPorNumero() {
        this.filtros = new ProcessoFiltros();
        this.filtros.numeroProcesso = this.filtro;
        this.processoService.getAllProcessoByFiltros(this.filtros).subscribe(
          (processos: Processo[]) => {
            this.processos = processos;
          },
          error => { console.log(error); }
          );
        }

}
