import {AfterViewInit, Component, OnDestroy, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { TaskApi } from 'src/app/common/ApiModels';
import { DataApiService } from 'src/app/services/data-api.service';
//import { TaskApi } from 'src/app/common/ApiModels';
//import { DataApiService } from 'src/app/services/data-api.service';

@Component({
  selector: 'app-task-table',
  templateUrl: './task-table.component.html',
  styleUrls: ['./task-table.component.scss']
})
export class TaskTableComponent implements AfterViewInit {
  // constructor() {
    
  // }
  displayedColumns: string[] = [
    // 'taskNumber',
    // 'taskName',
    // 'statusId',
    // 'employeeName',
    // 'departmentId',
    // 'dueDate'
  ];
  private data: TaskApi[] = [];
  dataSource = new MatTableDataSource<TaskApi>(this.data);

  @ViewChild(MatPaginator) paginator !: MatPaginator ;
  // constructor(private dataSvc : DataApiService) {
    
  // }
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
}
