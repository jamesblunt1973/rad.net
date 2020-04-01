import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';

@Component({
    selector: 'app-pager',
    templateUrl: './pager.component.html',
    styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnChanges, OnInit {

    @Input() total: number; // تعداد کل آیتم ها
    @Input() counts: number[]; // آرایه ای از اعداد که تعداد آیتم ها در صفحه را مشخص می‌کند
    @Output() gotoPage: EventEmitter<number> = new EventEmitter();
    @Output() changeCount: EventEmitter<number> = new EventEmitter();

    pages: number[] = [];
    pagesCount: number = 0;
    pagesMin: number = 0;
    pagesMax: number = 0;
    count: number = 0;
    currentPage: number = 1;

    constructor() { }

    ngOnInit() {
        // آرایه ی counts دست کم باید یک عنصر داشته باشد.
        this.count = this.counts[0];
    }

    ngOnChanges(changes: SimpleChanges): void {
      if (this.total > 0) {
        this.calculateData();
        this.currentPage = 1;
      }
    }

    selectPage(e) {
        this.currentPage = +e;
        this.calculateData();

        this.gotoPage.emit(this.currentPage);
    }

    selectCount(e) {
        this.count = +e;
        this.currentPage = 1;
        this.calculateData();

        this.changeCount.emit(this.count);
    }

    calculateData() {
        this.pagesCount = Math.ceil(this.total / this.count);
        this.pagesMin = this.currentPage == 1 ? 1 : this.currentPage - 1;
        this.pagesMax = this.currentPage == this.pagesCount ? this.pagesCount : this.currentPage + 1;

        this.pages = [];

        for (var i = this.pagesMin; i <= this.pagesMax; i++) {
            this.pages.push(i);
        }
    }
}
