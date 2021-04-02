import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AlunosComponent } from './components/alunos/alunos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { ProfessoresComponent } from './components/professores/professores.component';
import { ProfessoresAlunosComponent } from './components/alunos/professores-alunos/professores-alunos.component';
import { NavComponent } from './components/shared/nav/nav.component';
import { TituloComponent } from './components/shared/titulo/titulo.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ProfessorDetalheComponent } from './components/professores/professor-detalhe/professor-detalhe.component';
import { AlunosProfessoresComponent } from './components/professores/alunos-professores/alunos-professores.component';

@NgModule({
  declarations: [
    AppComponent,
    AlunosComponent,
    AlunosProfessoresComponent,
    DashboardComponent,
    PerfilComponent,
    ProfessoresAlunosComponent,
    ProfessoresComponent,
    ProfessorDetalheComponent,
    NavComponent,
    TituloComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxSpinnerModule,
    ToastrModule.forRoot({
      timeOut: 3500,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
      closeButton: true
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
