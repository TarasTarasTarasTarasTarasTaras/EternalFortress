import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main-routing.module';
import { MaterialModule } from 'src/app/material.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PopupWithInputComponent } from './components/popup-with-input/popup-with-input.component';
import { AboutComponent } from './components/about/about.component';

@NgModule({
  imports: [
    CommonModule,
    MainRoutingModule,
    MaterialModule
  ],
  declarations: [
    DashboardComponent,
    PopupWithInputComponent,
    AboutComponent
  ]
})
export class MainModule { }
