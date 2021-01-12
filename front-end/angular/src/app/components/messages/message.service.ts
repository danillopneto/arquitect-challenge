import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private snackBar: MatSnackBar) { }

  showMessage(msg: string, isError: boolean = false, time?: number): void {
    this.snackBar.open(msg, 'x', {
      duration: time != null ? time : 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: isError ? 'msg-error' : 'msg-success'
    });
  }
}
