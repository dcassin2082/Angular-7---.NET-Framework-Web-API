import { Component, OnInit } from '@angular/core';
import { DataManager, Query, ReturnOption } from '@syncfusion/ej2-data';
import { UserService } from '../user/shared/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-trackball-chart',
  templateUrl: './trackball-chart.component.html'
})
export class TrackballChartComponent implements OnInit {

  constructor(private service: UserService) { }

  baseUrl: string = environment.production ? "http://dcassin5938-001-site1.ctempurl.com/api/charts/trackball" : "https://localhost:44331/api/charts/trackball";

  public primaryXAxis: Object;
  public primaryYAxis: Object;
  public chartData: Object[];
  public crosshair: Object;
  public title: string;
  public tooltip: Object;
  public marker: Object;
  public items: object[];
  public dataManager: DataManager = new DataManager({
    url: this.baseUrl
  });
  ngOnInit(): void {
    if (localStorage.getItem('token') !== null) {
      this.service.loggedIn = true;
    }

    this.dataManager.executeQuery(new Query()).then((e: ReturnOption) => {
      this.chartData = e.result as object[];
    }).catch((e) => true);

    this.primaryXAxis = {
      title: 'Years',
      minimum: new Date(2013, 1, 1), maximum: new Date(2020, 2, 11),
      intervalType: 'Years',
      valueType: 'DateTime',
    };

    this.primaryYAxis = {
      labelFormat: '${value}K',
      title: 'Sales'
    }
    
    this.tooltip = { enable: true, shared: true, format: '${series.name} : ${point.x} : ${point.y}' };
    this.crosshair = { enable: true, lineType: 'Vertical' };
    this.marker = { visible: true };
    this.title = 'Average Sales per Person';
  }

}
