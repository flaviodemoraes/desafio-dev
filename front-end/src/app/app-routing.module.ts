import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './shared/services';
import { HomeComponent } from './pages/home/home.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { DxDataGridModule, DxFormModule } from 'devextreme-angular';
import { OperacoesComponent } from './pages/operacoes/operacoes.component';
import { FileUploadComponent } from './pages/file-upload/file-upload.component';

const routes: Routes = [
  {
    path: 'operacoes',
    component: OperacoesComponent
  },
  {
    path: 'file-upload',
    component: FileUploadComponent
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true }), DxDataGridModule, DxFormModule],
  providers: [AuthGuardService],
  exports: [RouterModule],
  declarations: [
    HomeComponent,
    ProfileComponent,
    TasksComponent,
    OperacoesComponent,
    FileUploadComponent
  ]
})
export class AppRoutingModule { }
