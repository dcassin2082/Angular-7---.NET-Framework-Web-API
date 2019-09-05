import { Component, OnInit } from '@angular/core';
import { DataManager, Query, ReturnOption } from '@syncfusion/ej2-data';
import { UserService } from '../user/shared/user.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-line-series-chart',
    templateUrl: './line-series-chart.component.html',
    styles: []
})
export class LineSeriesChartComponent implements OnInit {

    constructor(private service: UserService) { }

    baseUrl: string = environment.production ? "http://dcassin5938-001-site1.ctempurl.com/api/charts/lineseries" : "https://localhost:44331/api/charts/lineseries" ;
    public primaryXAxis: Object;
    public chartData: Object[];
    public primaryYAxis: Object;
    public legendSettings: Object;
    public tooltip: Object;
    public title: string;
    public marker: Object;
    public dataManager: DataManager = new DataManager({
        url: this.baseUrl
    });
    ngOnInit(): void {
        if (localStorage.getItem('token') !== null) {
            this.service.loggedIn = true;
        }
        this.tooltip = {
            enable: true
        }
        this.dataManager.executeQuery(new Query()).then((e: ReturnOption) => {
            this.chartData = e.result as object[];
        }).catch((e) => true);
        this.primaryXAxis = {
            valueType: 'Category',
            title: 'Months'
        };
        this.primaryYAxis = {
            labelFormat: '${value}K',
            title: 'Sales'
        };
        this.marker = {
            dataLabel: {
                visible: true
            }
        };
        this.legendSettings = {
            visible: true
        };
        this.title = 'Sales Analysis';
    }
}
