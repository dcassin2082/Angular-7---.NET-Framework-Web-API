import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LineSeriesChartComponent } from './line-series-chart.component';

describe('LineSeriesChartComponent', () => {
  let component: LineSeriesChartComponent;
  let fixture: ComponentFixture<LineSeriesChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LineSeriesChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LineSeriesChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
