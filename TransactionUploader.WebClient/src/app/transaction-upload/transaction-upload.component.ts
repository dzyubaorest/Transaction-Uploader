import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { catchError, last, map, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { HttpClient, HttpRequest, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar, MatSnackBarRef, SimpleSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-transaction-upload',
  templateUrl: './transaction-upload.component.html',
  styleUrls: ['./transaction-upload.component.css'],
  animations: [
    trigger('fadeInOut', [
      state('in', style({ opacity: 100 })),
      transition('* => void', [
        animate(300, style({ opacity: 0 }))
      ])
    ])
  ]
})
export class TransactionUploadComponent implements OnInit {

  /** Link text */
  @Input() text = 'Upload Transaction';
  /** Name used in form which will be sent in HTTP request. */
  @Input() param = 'file';
  /** Target URL for file uploading. */
  @Input() target = 'https://localhost:44359/api/transaction/post-file';
  /** File extension that accepted, same as 'accept' of <input type="file" />. 
      By the default, it's set to 'image/*'. */
  @Input() accept = '.csv,.xml';

  /** Allow you to add handler after its completion. Bubble up response text from remote. */
  @Output() complete = new EventEmitter();

  /** Allow you to add handler after its completion. Bubble up response text from remote. */
  @Output() start = new EventEmitter();

  files: Array<FileUploadModel> = [];

  constructor(private _http: HttpClient, public snackBar: MatSnackBar) {
    this.snackBarMessageSubscription

  }

  snackbarRef?: MatSnackBarRef<SimpleSnackBar> = null;
  private snackBarMessageSubscription: Subscription;
  private isSnackBarVisible: boolean = false;
  private messageQueue: Array<SnackBarMessage> = Array<SnackBarMessage>();

  ngOnInit(): void {
  }

  onClick() {
    const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
    fileUpload.onchange = () => {
      for (let index = 0; index < fileUpload.files.length; index++) {
        const file = fileUpload.files[index];
        const bytesInMb: number = 1000000;

        if (file.size == 0) {
          this.addSnackBarMessage("Upload of file: '" + file.name + "'  has failed: file must not be empty", "red-toaster", 2000);
          continue;
        }

        if (file.size > bytesInMb) {
          this.addSnackBarMessage("Upload of file: '" + file.name + "'  has failed: file`s size must be up to 1 mb.", "red-toaster", 2000);
          continue;
        }

        if (!(file.name.endsWith('csv') || file.name.endsWith('xml'))) {
          this.addSnackBarMessage("Upload of file: '" + file.name + "'  has failed: file must have .csv or .xml extension.", "red-toaster", 2000);
          continue;
        }

        this.files.push({
          data: file, state: 'in',
          inProgress: false, progress: 0
        });
      }
      this.uploadFiles();
    };
    fileUpload.click();
  }

  private uploadFile(file: FileUploadModel) {
    const fd = new FormData();
    fd.append(this.param, file.data);

    const req = new HttpRequest('POST', this.target, fd, {
      reportProgress: true
    });

    file.inProgress = true;
    this._http.request(req).pipe(
      map(event => {
        switch (event.type) {
          case HttpEventType.UploadProgress:
            file.progress = Math.round(event.loaded * 100 / event.total);
            break;
          case HttpEventType.Response:
            return event;
        }
      }),
      catchError((error: HttpErrorResponse) => {
        this.removeFileFromArray(file);
        file.inProgress = false;
        this.addSnackBarMessage("Upload of file: '" + file.data.name + "'  has failed.", "red-toaster");
        this.complete.emit();
        return of(`${file.data.name} upload failed.`);
      })
    )
      .subscribe(
        (event: any) => {
          if (event && event.hasOwnProperty('body') && event.body.hasOwnProperty('status')) {
            let status: string = event.body.status;
            if (status == "Success") {
              this.addSnackBarMessage("File: '" + file.data.name + "' is uploaded.", "green-toaster");
            } else if (status == "Failure") {
              this.addSnackBarMessage("Upload of file: '" + file.data.name + "'  has failed.", "red-toaster");
            } else {
              console.log("Unknown result status");
            }

            this.removeFileFromArray(file);
          }
        }
      );
  }

  private uploadFiles() {
    this.start.emit();

    const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
    fileUpload.value = '';

    if (this.files.length == 0) {
      this.complete.emit();
    }

    this.files.forEach(file => {
      this.uploadFile(file);
    });
  }

  private removeFileFromArray(file: FileUploadModel) {
    const index = this.files.indexOf(file);
    if (index > -1) {
      this.files.splice(index, 1);
    }
    if (this.files.length == 0) {
      this.complete.emit();
    }
  }

  private addSnackBarMessage(message: string, style: string, duration: number = 1500): void {
    let config = new MatSnackBarConfig();
    config.verticalPosition = 'top';
    config.horizontalPosition = 'right';
    config.duration = duration;
    config.panelClass = [style];

    this.messageQueue.push(new SnackBarMessage(message, config));
    if (!this.isSnackBarVisible) {
      this.showSnackBar();
    }
  }

  private showSnackBar() {

    if (this.messageQueue.length == 0) {
      return;
    }
    this.isSnackBarVisible = true;
    let message: SnackBarMessage = this.messageQueue.shift();
    this.snackbarRef = this.snackBar.open(message.message, undefined, message.config);
    this.snackbarRef.afterDismissed().subscribe(() => {
      this.isSnackBarVisible = false;
      this.showSnackBar();
    });
  }
}

export class SnackBarMessage {
  constructor(public message: string, public config: MatSnackBarConfig) { }
}

export class FileUploadModel {
  data: File;
  state: string;
  inProgress: boolean;
  progress: number;
  sub?: Subscription;
}