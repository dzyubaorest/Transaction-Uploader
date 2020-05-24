import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { HttpClient } from '@angular/common/http';
import { TransactionModel } from '../Models/TransactionModel';
import { TransactionRepository } from "../Repositories/TransactionRepository";

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.css']
})
export class TransactionListComponent implements OnInit {
  private transactionRepository: TransactionRepository;

  constructor(httpClient: HttpClient) {
    this.transactionRepository = new TransactionRepository(httpClient);
  }

  isLoading: boolean;
  isUploadingRunning: boolean;
  currencyFilterIsChecked: boolean;
  statusFilterIsChecked: boolean;
  dateFilterIsChecked: boolean;

  currencyFilter: string;
  statusFilter: string;
  startDateFilter: Date;
  endDateFilter: Date;

  displayedColumns = ['id', 'payment', 'status', 'date'];

  dataSource = new MatTableDataSource([]);
  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit(): void {
    this.isLoading = false;
    this.currencyFilterIsChecked = false;
    this.statusFilterIsChecked = false;
    this.dateFilterIsChecked = false;
    this.isUploadingRunning = false;
  }

  ngAfterViewInit() {
  }

  search(): void {
    let currency: string | null = this.currencyFilterIsChecked ? this.currencyFilter : null;
    let status: string | null = this.statusFilterIsChecked ? this.statusFilter : null;
    let startDate: Date | null = this.dateFilterIsChecked ? this.startDateFilter : null;
    let endDate: Date | null = this.dateFilterIsChecked ? this.endDateFilter : null;

    this.transactionRepository.getTransactions(currency, status, startDate, endDate)
      .subscribe(values => {
        this.dataSource = new MatTableDataSource(values);
        this.dataSource.paginator = this.paginator;
      });
  }

  onFileComplete() {
    this.isUploadingRunning = false;
    this.search();
  }

  onUploadStart(){
    this.isUploadingRunning = true;
  }
}