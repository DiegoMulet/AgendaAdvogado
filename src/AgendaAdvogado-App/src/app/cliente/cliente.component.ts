import { Component, OnInit } from '@angular/core';
import { Cliente } from '../_models/Cliente';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ClienteService } from '../_services/cliente.service';
import { BsModalService } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  clientes: Cliente[];
  cliente: Cliente;
  modoSalvar = 'post';
  bodyDeletarCliente: string;
  registerForm: FormGroup;
  FiltroLista: string;
  titulo = 'Clientes';

  constructor(
      private clienteService: ClienteService
    , private fb: FormBuilder
    , private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.validation();
    this.getClientes();
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [''],
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      cnpj: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(15)]],
      estado: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]]
    });
  }

  getClientes() {
    this.clienteService.getAllCliente().subscribe(
      (clientes: Cliente[]) => {
        this.clientes = clientes;
      },
      error => { console.log(error); }
      );
    }

    novoCliente(template: any) {
      this.modoSalvar = 'post';
      this.openModal(template);
    }

    editarCliente(cliente: Cliente, template: any) {
      this.modoSalvar = 'put';
      this.openModal(template);
      this.cliente = Object.assign({}, cliente);
      this.registerForm.patchValue(this.cliente);
    }

    excluirCliente(cliente: Cliente, template: any) {
      this.openModal(template);
      this.cliente = cliente;
      this.bodyDeletarCliente = `Tem certeza que deseja excluir o Cliente: ${cliente.nome}?`;
    }

    confirmeDelete(template: any) {
      this.clienteService.deleteCliente(this.cliente.id).subscribe(
        () => {
            template.hide();
            this.getClientes();
            this.toastr.success('Cliente deletado com sucesso.');
          }, error => {
            this.toastr.error(error, 'Falha ao deletar cliente');
          }
      );
    }

    salvarAlteracao(template: any) {
      if (this.registerForm.valid) {
        if (this.modoSalvar === 'post') {
          this.cliente = Object.assign({}, this.registerForm.value);

          this.clienteService.postCliente(this.cliente).subscribe(
            (novoCliente: Cliente) => {
              template.hide();
              this.getClientes();
              this.toastr.success('Cliente criado com sucesso.');
            }, error => {
              this.toastr.error(error, 'Falha ao criar cliente');
            }
          );
        } else {
          this.cliente = Object.assign({id: this.cliente.id}, this.registerForm.value);

          this.clienteService.putCliente(this.cliente).subscribe(
            (novoCliente: Cliente) => {
              template.hide();
              this.getClientes();
              this.toastr.success('Cliente editado com sucesso.');
            }, error => {
              this.toastr.error(error, 'Falha ao editar cliente');
            }
          );
        }
      }
    }

    openModal(template: any) {
      this.registerForm.reset();
      template.show();
    }
}
