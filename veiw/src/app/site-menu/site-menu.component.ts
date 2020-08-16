import {Component} from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {Observable} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';

@Component({
  selector: 'app-site-menu',
  templateUrl: './site-menu.component.html',
  styleUrls: ['./site-menu.component.css']
})
export class SiteMenuComponent {
  title = 'Video processor Demo';

  showSecondRow: boolean;

  constructor() {
    this.showSecondRow = false;
  }

  onClickShowSecondRow() {
    this.showSecondRow = !this.showSecondRow;
  }
}
