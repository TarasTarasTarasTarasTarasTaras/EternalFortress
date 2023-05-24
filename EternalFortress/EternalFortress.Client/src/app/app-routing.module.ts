import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './modules/main/components/about/about.component';

const routes: Routes = [
  { path: 'about', component: AboutComponent },
  { path: 'account', loadChildren: () => import('./modules/authentication/authentication.module').then(m => m.AuthenticationModule) },
  { path: 'dashboard', loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule) },
  { path: '**', redirectTo: '/account/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
