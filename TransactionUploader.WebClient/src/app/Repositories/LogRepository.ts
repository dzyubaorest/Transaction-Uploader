import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError  } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Log } from "../Models/Log";
import { ApiUrl } from "../Environment";

export class LogRepository {
  constructor(private httpClient: HttpClient) { }

  public getLogs() : Observable<Log[]> {
    return this.httpClient.get<Log[]>(`${ApiUrl}/api/log`).
      pipe(
        map((data: Log[]) => {
          return data;
        }),
        catchError(error => {
          return throwError('Something went wrong while getting logs from server!');
        }));
  }
}