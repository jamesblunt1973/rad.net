import { Component, OnInit } from '@angular/core';

import { IMneuItem } from './menuItem.model';
import { IconsService } from '../../core/icons.service';
import { UiService } from '../../core/ui.service';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

    menuItems: IMneuItem[] = [];

    constructor(public iconService: IconsService, private uiService: UiService) { }

    ngOnInit() {

        this.menuItems = [{
            icon: this.iconService.viewDashboardIcon,
            title: 'داشبورد',
            url: '/home'
        }, {
            icon: this.iconService.listIcon,
            title: 'لیست داروها',
            url: '/medicines'
        }, {
            icon: this.iconService.listIcon,
            title: 'لیست بیماران',
            url: '/patients'
        }, {
            icon: this.iconService.listIcon,
            title: 'لیست داروخانه ها',
            url: '/pharmacies'
        }, {
            icon: this.iconService.listIcon,
            title: 'لیست پزشکان',
            url: '/physicians'
        }, {
            icon: this.iconService.checkAllIcon,
            title: 'تاییدیه ها',
            url: '/verifications'
        }, {
            icon: this.iconService.chartBarIcon,
            title: 'گزارش تجمیعی داروها',
            url: '/medicines-acc'
        }, {
            icon: this.iconService.chartBarIcon,
            title: 'گزارش تجمیعی بیماران',
            url: '/patients-acc'
        }, {
            icon: this.iconService.pencilIcon,
            title: 'گزارش درخواست ها',
            url: '/requests'
        }, {
            icon: this.iconService.sendIcon,
            title: 'توزیع و تبادلات دارو',
            url: '/distributions'
        }, {
            icon: this.iconService.settingsIcon,
            title: 'تنظیمات کاربری',
            url: '/settings'
        }];
    }

    collapseSidebar() {
        this.uiService.changeSidebarStatus();
    }

}
