import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ThumbnailDisplayComponent} from './thumbnail-display/thumbnail-display.component';
import {DashboardComponent} from './dashboard/dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'thumbnail', component: ThumbnailDisplayComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
