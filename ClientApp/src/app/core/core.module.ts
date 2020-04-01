import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { IconsService } from './icons.service';
import { UiService } from './ui.service';
import { DataService } from './data.service';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { AuthInterceptorProvider } from './token-interceptor';

@NgModule({
  declarations: [

  ],
  imports: [
    HttpClientModule
  ],
  providers: [
    IconsService,
    UiService,
    DataService,
    AuthGuard,
    AuthService,
    AuthInterceptorProvider
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(`CoreModule has already been loaded. Import Core modules in the AppModule only.`);
    }
  }
}
