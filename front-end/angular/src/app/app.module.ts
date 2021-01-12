import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { environment } from '../environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';

registerLocaleData(localePt);

/* App Components */
import { HeaderComponent } from './components/template/header/header.component';
import { NavComponent } from './components/template/nav/nav.component';
import { FooterComponent } from './components/template/footer/footer.component';
import { HomeComponent } from './views/home/home.component';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { ConfigurationComponent } from './components/configuration/configuration.component';

/* App Directives */

/* Material */
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatGridListModule } from '@angular/material/grid-list';
import { EventsReadComponent } from './components/events/events-read/events-read.component';

/* Plugins */

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavComponent,
    HomeComponent,
    DashboardComponent,
    ConfigurationComponent,
    EventsReadComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    MatGridListModule,
    MatSlideToggleModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule,
    AppRoutingModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: false } }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
