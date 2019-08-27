import { Component, OnInit } from '@angular/core';
import { DataManager, Query, ReturnOption } from '@syncfusion/ej2-data';

@Component({
  selector: 'app-column-chart',
  templateUrl: './column-chart.component.html',
  styles: []
})
export class ColumnChartComponent implements OnInit {

  constructor() { }

  baseUrl = "https://localhost:44331/api/charts/column";

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
  // public query: Query = new Query().take(5).where('Estimate', 'lessThan', 3, false);
  ngOnInit(): void {
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
