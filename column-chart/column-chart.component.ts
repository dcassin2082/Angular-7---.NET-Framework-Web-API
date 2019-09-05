import { Component, OnInit } from '@angular/core';
import { DataManager, Query, ReturnOption } from '@syncfusion/ej2-data';
import { UserService } from '../user/shared/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-column-chart',
  templateUrl: './column-chart.component.html',
  styles: []
})
export class ColumnChartComponent implements OnInit {

  baseUrl: string = environment.production ? "http://dcassin5938-001-site1.ctempurl.com/api/charts/column" : "https://localhost:44331/api/charts/column" ;
  public legendSettings: Object;
  public tooltip: Object;
  public title: string;
  public marker: Object;
  public primaryXAxis: Object;
  public primaryYAxis: Object;
  public items: object[];
  public dataManager: DataManager = new DataManager({
    url: this.baseUrl
  });

  constructor(private service: UserService) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') !== null) {
      this.service.loggedIn = true;
    }

    this.dataManager.executeQuery(new Query().take(30)).then((e: ReturnOption) => {
      this.items = e.result as object[];
    }).catch((e) => true);
    this.primaryXAxis = {
      rangePadding: 'Additional',
      valueType: 'Category',
      title: 'Date'
    };
    this.primaryYAxis = {
      title: 'Sales',
      minimum: 0,
      maximum: 110,
      interval: 10,
      labelFormat: '${value}K'
    };
    this.legendSettings = {
      visible: true
    };
    this.title = 'September 2019';
    this.tooltip = {
      enable: true
    }
    this.marker = {
      dataLabel: {
        visible: true
      }
    };
  }
}
