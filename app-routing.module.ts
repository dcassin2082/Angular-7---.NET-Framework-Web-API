import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactsComponent } from './contacts/contacts.component';
import { EmployeesComponent } from './employees/employees.component';
import { ColumnChartComponent } from './column-chart/column-chart.component';
import { LineSeriesChartComponent } from './line-series-chart/line-series-chart.component';
import { TrackballChartComponent } from './trackball-chart/trackball-chart.component';
import { AllChartsComponent } from './all-charts/all-charts.component';
import { GameComponent } from './game/game.component';


const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'contacts', component: ContactsComponent },
  { path: 'employees', component: EmployeesComponent },
  { path: 'column', component: ColumnChartComponent },
  { path: 'line-series', component: LineSeriesChartComponent },
  { path: 'trackball', component: TrackballChartComponent },
  { path: 'all-charts', component: AllChartsComponent },
  { path: 'game', component: GameComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
