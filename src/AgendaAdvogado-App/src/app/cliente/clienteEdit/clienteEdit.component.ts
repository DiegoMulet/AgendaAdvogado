import { Component, OnInit } from '@angular/core';
import { Cliente } from 'src/app/_models/Cliente';
import { BsModalService } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { ClienteService } from 'src/app/_services/cliente.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-clienteEdit',
  templateUrl: './clienteEdit.component.html',
  styleUrls: ['./clienteEdit.component.css']
})
export class ClienteEditComponent implements OnInit {

  cliente: Cliente = new Cliente();
  registerForm: FormGroup;
  titulo = 'Agenda';

  get processos(): FormArray {
    return this.registerForm.get('processos') as FormArray;
  }

  constructor(
      private clienteService: ClienteService
    , private modalService: BsModalService
    , private fb: FormBuilder
    , private toastr: ToastrService
    , private router: ActivatedRoute
    , private datePipe: DatePipe
  ) { }

  ngOnInit() {
    this.validation();
    this. carregarCliente();
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [''],
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      cnpj: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(15)]],
      estado: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
      processos: this.fb.array([])
    });
  }

  carregarCliente() {
    const idCliente = +this.router.snapshot.paramMap.get('id');

    this.resetForm();

    this.clienteService.getClienteById(idCliente)
      .subscribe(
        (cliente: Cliente) => {
          this.cliente = Object.assign({}, cliente);
          console.log(cliente);

          this.registerForm.patchValue(this.cliente);

          this.cliente.processos.forEach(processo => {
            this.processos.push(this.criaProcesso(processo));
          });
        }
      );
  }

  criaProcesso(processo: any): FormGroup {
    return this.fb.group({
      id: [processo.id],
      numeroProcesso: [processo.numeroProcesso, Validators.required],
      estado: [processo.estado, Validators.required],
      valor: [processo.valor, Validators.required],
      dataCriacao: [processo.dataCriacao, Validators.required],
      ativo: [processo.ativo],
      clienteId: [processo.clienteId, Validators.required],
    });
  }

  adicionarProcesso() {
    this.processos.push(this.criaProcesso({ id: 0, clienteId: this.cliente.id }));
  }

  removerProcesso(id: number) {
    this.processos.removeAt(id);
  }

  salvarCliente() {
    this.cliente = Object.assign({ id: this.cliente.id }, this.registerForm.value);

    this.clienteService.putCliente(this.cliente).subscribe(
      () => {
        this.carregarCliente();
        this.toastr.success('Editado com Sucesso!');
      }, error => {
        this.toastr.error(`Erro ao Editar: ${error}`);
      }
    );
  }

  resetForm() {
    while (this.processos.length) {
      this.processos.removeAt(0);
      }
    this.registerForm.reset();
  }

}
