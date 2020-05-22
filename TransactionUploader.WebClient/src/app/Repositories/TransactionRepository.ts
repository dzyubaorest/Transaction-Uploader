import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError  } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { TransactionModel } from "../Models/TransactionModel";

export class TransactionRepository {

  private baseUrl: string = "https://localhost:44359/";
  constructor(private httpClient: HttpClient) { }

  public getTransactions(
    currencyCode?: string | undefined,
    status?: string | undefined,
    startDate?: Date | undefined,
    endDate?: Date | undefined) : Observable<TransactionModel[]> {

    let params: HttpParams = new HttpParams();
    if (currencyCode) {
      params = params.set('currencyCode', currencyCode);
    }
    if (status) {
      params = params.set('status', status);
    }
    if (startDate) {
      params = params.set('startDate', startDate.toUTCString());
    }
    if (endDate) {
      var millisecondsInOneDayJustBeforeDayFinished = (23*60*60 + 59 * 60 + 59) * 1000;
      let newDate = new Date(Date.parse(endDate.toDateString()) + millisecondsInOneDayJustBeforeDayFinished );

      params = params.set('endDate', newDate.toUTCString());
    }

    return this.httpClient.get<TransactionModel[]>(this.baseUrl + "api/transaction", { params }).
      pipe(
        map((data: TransactionModel[]) => {
          return data;
        }),
        catchError(error => {
          return throwError('Something went wrong while getting transaction from server!');
        }));
  }
}