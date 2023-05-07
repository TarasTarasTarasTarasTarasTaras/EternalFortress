import { Component, Inject, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
  selector: 'app-popup-with-input',
  templateUrl: './popup-with-input.component.html',
  styleUrls: ['./popup-with-input.component.css']
})
export class PopupWithInputComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<PopupWithInputComponent>) { }

    ngOnInit(): void {
    }

    okPressed(value) {
      if (!value) return;
      this.dialogRef.close({ inputValue: value });
    }

    cancelPressed() {
      this.dialogRef.close();
    }
}
