import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { AutoresComponent } from './pages/autores/autores.component';
import { EditorialesComponent } from './pages/editoriales/editoriales.component';
import { LibrosComponent } from './pages/libros/libros.component';

import { RestService } from './services/rest.service';
import { ModalService } from './services/modal.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AutoresComponent,
    EditorialesComponent,
    LibrosComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    NgbPaginationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'autores', component: AutoresComponent },
      { path: 'editoriales', component: EditorialesComponent },
      { path: 'libros', component: LibrosComponent }
    ])
  ],
  providers: [
    RestService,
    ModalService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
