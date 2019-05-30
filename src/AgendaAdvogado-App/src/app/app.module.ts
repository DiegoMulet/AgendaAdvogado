import { BrowserModule } from '@angular/platform-browser';

import { NgModule, LOCALE_ID } from '@angular/core';
import localept from '@angular/common/locales/pt';
import {registerLocaleData} from '@angular/common';
registerLocaleData(localept, 'pt');

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule, TooltipModule, ModalModule, BsDatepickerModule, TabsModule  } from 'ngx-bootstrap';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { ClienteComponent } from './cliente/cliente.component';
import { ProcessoComponent } from './processo/processo.component';
import {ClienteEditComponent} from './cliente/clienteEdit/clienteEdit.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavComponent } from './nav/nav.component';
import { TituloComponent } from './_shared/titulo/titulo.component';
import { DatePipe } from '@angular/common';

@NgModule({
   declarations: [
      AppComponent,
      ClienteComponent,
      ProcessoComponent,
      ClienteEditComponent,
      DashboardComponent,
      NavComponent,
      TituloComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      ReactiveFormsModule,
      BrowserAnimationsModule,
      TabsModule.forRoot(),
      ToastrModule.forRoot({
         timeOut: 10000,
         positionClass: 'toast-bottom-right',
         preventDuplicates: true,
       })
   ],
   providers: [
      DatePipe,
      { provide: LOCALE_ID, useValue: 'pt' }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
