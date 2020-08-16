import {Component, OnInit} from '@angular/core';
import {DashboardDataService} from '../dashboard/dashboard-data.service';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';

@Component({
  selector: 'app-thumbnail-display',
  templateUrl: './thumbnail-display.component.html',
  styleUrls: ['./thumbnail-display.component.css'],
  providers: [DashboardDataService]
})
export class ThumbnailDisplayComponent implements OnInit {

  public thumbImage: SafeResourceUrl;
  public showLoadingImg: boolean;
  public showThumbImg: boolean;

  constructor(private service: DashboardDataService,
              private sanitizer: DomSanitizer) {
    this.showLoadingImg = false;
    this.showThumbImg = false;
  }

  onClickLoadImg(): any {
    this.showLoadingImg = true;
    this.showThumbImg = false;

    this.service.getThumbnails().subscribe(result => {
      console.log(result);

      this.thumbImage = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
        + result.fileContents);
      this.showLoadingImg = false;
      this.showThumbImg = true;

    });
  }

  ngOnInit(): void {
  }
}
