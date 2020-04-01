import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable()
export class UiService {

    private sidebarStatus = new BehaviorSubject<string>('');

    changeSidebarStatus() {
        var currentState = this.sidebarStatus.getValue();
        var nextState = currentState === '' ? 'open' : '';
        this.sidebarStatus.next(nextState);
    }

    getMessage(): Observable<string> {
        return this.sidebarStatus.asObservable();
    }
}
