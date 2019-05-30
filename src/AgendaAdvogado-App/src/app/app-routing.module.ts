import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClienteEditComponent } from './cliente/clienteEdit/clienteEdit.component';
import { ClienteComponent } from './cliente/cliente.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProcessoComponent } from './processo/processo.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'cliente', component: ClienteComponent },
  { path: 'processo', component: ProcessoComponent },
  { path: 'cliente/:id/agenda', component: ClienteEditComponent },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
