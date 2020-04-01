import { Component, OnChanges, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.scss']
})
export class CounterComponent implements OnInit, OnChanges {
    @Input() number = 0;
    digits: Array<string> = [];

    constructor() { }

    ngOnInit() {
    }

    ngOnChanges() {
        var numStr = this.number.toString();
        while (numStr.length < 3) numStr = "0" + numStr;
        this.digits = numStr.split('').reverse();
    }
}
