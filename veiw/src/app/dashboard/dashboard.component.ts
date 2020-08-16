import {Component} from '@angular/core';
import {BreakpointObserver} from '@angular/cdk/layout';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';
import {DashboardDataService} from './dashboard-data.service';

class SafeUrl {
  Url: SafeResourceUrl;
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  public thumbImages: Array<SafeUrl>;
  public showLoadingImg: boolean;
  public showThumbImg: boolean;

  public H264Response: string;
  public MultiHlsResponse: string;

  constructor(private breakpointObserver: BreakpointObserver,
              private service: DashboardDataService,
              private sanitizer: DomSanitizer) {
    this.showLoadingImg = false;
    this.showThumbImg = false;
    this.thumbImages = [];
  }

  onClickLoadImg(): any {
    this.thumbImages = [];
    this.showLoadingImg = true;
    this.showThumbImg = false;

    this.service.getThumbnails().subscribe(result => {
      console.log(result);
      result.forEach(itm => {
        const safeUrl = new SafeUrl();
        safeUrl.Url = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
          + itm.fileContents);
        this.thumbImages.push(safeUrl);
      });
      this.showLoadingImg = false;
      this.showThumbImg = true;
      console.log(this.thumbImages);
    });
  }

  onClickLoadH264(): any {
    this.H264Response = '';
    this.service.getH264().subscribe(data => {
        this.H264Response = data.content;
      },
      error => {
        console.log(error);
      });
  }

  onClickLoadMultiHls(): any {
    this.MultiHlsResponse = '';
    this.service.getMultibitHls().subscribe(data => {
        this.MultiHlsResponse = data.content;
      },
      error => {
        console.log(error);
      });
  }
}
