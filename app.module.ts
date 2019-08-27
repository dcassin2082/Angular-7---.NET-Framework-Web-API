import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { Ng2OrderModule } from 'ng2-order-pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ChartModule } from '@syncfusion/ej2-angular-charts';
import { NgxMaskModule, IConfig } from 'ngx-mask'
import { ValidatorModule } from './shared/validator.module';

import {
  CategoryService, DateTimeService, ScrollBarService, ColumnSeriesService, ChartAnnotationService, RangeColumnSeriesService, StackingColumnSeriesService, LegendService, TooltipService
} from '@syncfusion/ej2-angular-charts';

import { DataLabelService, LineSeriesService } from '@syncfusion/ej2-angular-charts';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { EmployeesComponent } from './employees/employees.component';
import { ContactsComponent } from './contacts/contacts.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { ColumnChartComponent } from './column-chart/column-chart.component';
import { LineSeriesChartComponent } from './line-series-chart/line-series-chart.component';
import { TrackballChartComponent } from './trackball-chart/trackball-chart.component';
import { AllChartsComponent } from './all-charts/all-charts.component';
import { ContactComponent } from './contacts/contact/contact.component';
import { ContactListComponent } from './contacts/contact-list/contact-list.component';
import { EmployeeComponent } from './employees/employee/employee.component';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';
import { GameComponent } from './game/game.component';
import { CompaniesComponent } from './companies/companies.component';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = {};

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    EmployeesComponent,
    ContactsComponent,
    HeaderComponent,
    FooterComponent,
    ColumnChartComponent,
    LineSeriesChartComponent,
    TrackballChartComponent,
    AllChartsComponent,
    ContactComponent,
    ContactListComponent,
    EmployeeComponent,
    EmployeeListComponent,
    GameComponent,
    CompaniesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    NgxPaginationModule,
    Ng2SearchPipeModule,
    BrowserAnimationsModule,
    Ng2OrderModule,
    RouterModule,
    ChartModule,
    NgxMaskModule.forRoot(options),
    ValidatorModule
  ],
  providers: [CategoryService, DataLabelService, DateTimeService, ScrollBarService, LineSeriesService, ColumnSeriesService, ChartAnnotationService,
    RangeColumnSeriesService, StackingColumnSeriesService, LegendService, TooltipService],
  bootstrap: [AppComponent]
})
export class AppModule { }
