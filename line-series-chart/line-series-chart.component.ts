import { Component, OnInit } from '@angular/core';
import { DataManager, Query, ReturnOption } from '@syncfusion/ej2-data';

@Component({
    selector: 'app-line-series-chart',
    templateUrl: './line-series-chart.component.html',
    // template:
    //   `<ejs-chart id="chart-container" [primaryXAxis]='primaryXAxis' [primaryYAxis]='primaryYAxis'
    // [legendSettings]='legendSettings' [tooltip]='tooltip' [title]='title'>
    //     <e-series-collection>
    //         <e-series [dataSource]='chartData' type='Line' xName='month' yName='sales' name='Sales' [marker]='marker'></e-series>
    //     </e-series-collection>
    // </ejs-chart>`,
    styles: []
})
export class LineSeriesChartComponent implements OnInit {

    baseUrl = "https://localhost:44331/api/charts/lineseries";
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
        // Tooltip for chart
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
