import {Component, OnInit} from '@angular/core';
import {DataService} from '../../services/data.service';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';

@Component({
  selector: 'app-thumbnail-display',
  templateUrl: './thumbnail-display.component.html',
  styleUrls: ['./thumbnail-display.component.css'],
  providers: [DataService]
})
export class ThumbnailDisplayComponent implements OnInit {

  public thumbImage: SafeResourceUrl;
  public showLoadingImg: boolean;
  public showThumbImg: boolean;

  constructor(private service: DataService,
              private sanitizer: DomSanitizer) {
    this.showLoadingImg = false;
    this.showThumbImg = false;
  }

  onClickLoadImg(): any {
    this.showLoadingImg = true;
    this.showThumbImg = false;

    this.service.getThumbnail().subscribe(result => {
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
