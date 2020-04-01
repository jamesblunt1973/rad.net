import { Component, OnInit } from '@angular/core';

import { IconsService } from '../../core/icons.service';
import { UiService } from '../../core/ui.service';
import { AuthService } from '../../core/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    menuStatus = '';
    userName = '';

    constructor(public icons: IconsService, private uiService: UiService, private authService: AuthService) { }

    ngOnInit() {
        this.uiService.getMessage().subscribe(status => {
            this.menuStatus = status;
        });
        this.authService.getUsername().subscribe(un => {
            this.userName = un;
        });
    }

    changeMenuBtn() {
        this.uiService.changeSidebarStatus();
    }

    logout() {
        this.authService.logout();
    }
}
