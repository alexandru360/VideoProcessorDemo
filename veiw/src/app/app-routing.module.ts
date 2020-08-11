import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ThumbnailDisplayComponent} from './thumbnail/thumbnail-display/thumbnail-display.component';

const routes: Routes = [
  { path: '', redirectTo: '/thumbnail', pathMatch: 'full' },
  { path: 'thumbnail', component: ThumbnailDisplayComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
