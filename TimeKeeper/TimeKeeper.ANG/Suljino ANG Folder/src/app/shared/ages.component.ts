import { Component, OnChanges, Input, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'ages',
    templateUrl: 'ages.component.html',
    styleUrls: ['ages.component.css']
})

export class AgesComponent implements OnChanges {
    @Input() period: number;
    agesWidth: number;
    @Output() notify: EventEmitter<string> = new EventEmitter<string>();

    ngOnChanges(): void {
        this.agesWidth = this.period * 17;
    }

    onClick(): void {
        this.notify.emit(`The rating ${this.period} was clicked!`);
    }
}