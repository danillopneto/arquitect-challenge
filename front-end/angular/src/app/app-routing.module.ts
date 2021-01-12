import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './views/home/home.component';
import { DashboardComponent } from './views/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "dashboard",
    component: DashboardComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
