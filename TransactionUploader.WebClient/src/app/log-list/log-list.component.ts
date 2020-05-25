import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Log } from "../Models/Log";
import { HttpClient } from '@angular/common/http';
import { LogRepository } from "../Repositories/LogRepository";
import { ApiUrl } from "../Environment";

@Component({
  selector: 'app-log-list',
  templateUrl: './log-list.component.html',
  styleUrls: ['./log-list.component.css']
})
export class LogListComponent implements OnInit {
  private logRepository: LogRepository;

  isLoading: boolean;
  displayedColumns = ['message', 'file'];
  dataSource = new MatTableDataSource([]);
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(httpClient: HttpClient) {
    this.logRepository = new LogRepository(httpClient);
  }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.logRepository.getLogs()
      .subscribe(values => {
        this.dataSource = new MatTableDataSource(values);
        this.dataSource.paginator = this.paginator;
      });
  }

  getFileUrl(log: Log): string {
    return `${ApiUrl}/api/file/${log.fileId}`;
  }
}