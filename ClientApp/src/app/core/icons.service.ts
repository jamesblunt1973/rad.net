import { Injectable } from "@angular/core";

import home from '@iconify/icons-mdi/home';
import dotsVertical from '@iconify/icons-mdi/dots-vertical';
import viewDashboard from '@iconify/icons-mdi/view-dashboard';
import list from '@iconify/icons-mdi/format-list-bulleted-square';
import bookmarkCheck from '@iconify/icons-mdi/bookmark-check';
import check from '@iconify/icons-mdi/check';
import checkAll from '@iconify/icons-mdi/check-all';
import chartBar from '@iconify/icons-mdi/chart-bar';
import chartLine from '@iconify/icons-mdi/chart-line';
import settings from '@iconify/icons-mdi/settings';
import pencil from '@iconify/icons-mdi/pencil';
import magnify from '@iconify/icons-mdi/magnify';
import edit from '@iconify/icons-mdi/content-save-edit';
import error from '@iconify/icons-mdi/error';
import close from '@iconify/icons-mdi/close';
import plusCircle from '@iconify/icons-mdi/plus-circle';
import key from '@iconify/icons-mdi/key';
import user from '@iconify/icons-mdi/user';
import plus from '@iconify/icons-mdi/plus';
import sms from '@iconify/icons-mdi/sms';
import send from '@iconify/icons-mdi/send-check';
import * as del from '@iconify/icons-mdi/delete';

@Injectable()
export class IconsService {
  homeIcon = home;
  dotsVerticalIcon = dotsVertical;
  viewDashboardIcon = viewDashboard;
  listIcon = list;
  bookmarkCheckIcon = bookmarkCheck;
  checkIcon = check;
  checkAllIcon = checkAll;
  chartBarIcon = chartBar;
  chartLineIcon = chartLine;
  settingsIcon = settings;
  pencilIcon = pencil;
  magnifyIcon = magnify;
  editIcon = edit;
  deleteIcon = del.default;
  errorIcon = error;
  closeIcon = close;
  plusCircleIcon = plusCircle;
  keyIcon = key;
  userIcon = user;
  plusIcon = plus;
  smsIcon = sms;
  sendIcon = send;
}
