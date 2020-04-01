import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { DataService } from '../../core/data.service';
import { HomeReportData } from '../../shared/models/home-report';
import { AutoUnsubscribe } from '../../shared/auto-unsubscribe';

@AutoUnsubscribe
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private dataService: DataService) { }

  data: HomeReportData = {};
  weekly = [];

  chartRawData: any = {};
  ngOnInit() {
    var sub = this.dataService.getHomeReport().subscribe(res => {
      this.data = res;
    });
    this.subscriptions.push(sub);

    sub = this.dataService.getLastWeekStats().subscribe(res => {
      if (res == null)
        return;
      this.weekly = [];
      for (let item of res) {
        this.weekly.push({ name: item.dayOfWeek, value: item.count });
      }
    });
    this.subscriptions.push(sub);

    sub = this.dataService.getChartFake('monthly.json').subscribe((data: Array<number>) => {
      this.monthly = [];

      this.monthly.push({ name: 'فروردین', value: data[0] });
      this.monthly.push({ name: 'اردیبهشت', value: data[1] });
      this.monthly.push({ name: 'خرداد', value: data[2] });
      this.monthly.push({ name: 'تیر', value: data[3] });
      this.monthly.push({ name: 'مرداد', value: data[4] });
      this.monthly.push({ name: 'شهریور', value: data[5] });
      this.monthly.push({ name: 'مهر', value: data[6] });
      this.monthly.push({ name: 'آبان', value: data[7] });
      this.monthly.push({ name: 'آذر', value: data[8] });
      this.monthly.push({ name: 'دی', value: data[9] });
      this.monthly.push({ name: 'بهمن', value: data[10] });
      this.monthly.push({ name: 'اسفند', value: data[11] });
    });
    this.subscriptions.push(sub);

    this.gender = [
      {
        "name": "مرد",
        "value": 185
      },
      {
        "name": "زن",
        "value": 118
      }
    ];

    this.ages = [
      {
        "name": "زیر 3 سال",
        "value": 5
      }, {
        "name": "3 تا 10 سال",
        "value": 15
      }, {
        "name": "10 تا 18 سال",
        "value": 18
      }, {
        "name": "18 تا 35 سال",
        "value": 16
      }, {
        "name": "35 تا 50 سال",
        "value": 12
      }, {
        "name": "بالای 50 سال",
        "value": 8
      }
    ];
    sub = this.dataService.getChartFake('polar.json').subscribe((data: Array<any>) => {
      this.polar = data;
    });
    this.subscriptions.push(sub);
  }


  subscriptions: Subscription[] = [];

  // chart data
  monthly: any[];
  gender: any[];
  ages: any[];
  polar: any[];
  colorScheme = {
    domain: [
      'aqua']
  };
  onSelect(event) {
    console.log(event);
  }

}
