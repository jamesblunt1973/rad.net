import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-counter-digit',
    templateUrl: './digit.component.html',
    styleUrls: ['./digit.component.scss']
})
export class DigitComponent implements OnInit {
    @Input() number = 0;
    top = 0;

    constructor() { }

    ngOnInit() {
        window.setTimeout(() => {
            this.top = this.number * -32;
        }, 250);
    }
}
