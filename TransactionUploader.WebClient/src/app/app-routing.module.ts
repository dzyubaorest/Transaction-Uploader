import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { TransactionUploadComponent } from './transaction-upload/transaction-upload.component';
import { LogListComponent } from './log-list/log-list.component';


const routes: Routes = [
  { path: 'transaction-list', component: TransactionListComponent },
  { path: '', redirectTo: '/transaction-list', pathMatch: 'full' },
  { path: 'log-list', component: LogListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
