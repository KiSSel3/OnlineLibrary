import {Component, inject, model} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import {FormGroup, FormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";

@Component({
  selector: 'app-borrowed-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
  ],
  templateUrl: './borrowed-dialog.component.html'
})
export class BorrowedDialogComponent {
  readonly dialogRef = inject(MatDialogRef<BorrowedDialogComponent>);
  readonly dayCount = model(1);
  onNoClick(): void {
    this.dialogRef.close();
  }

  preventInput(event: KeyboardEvent): void {
    event.preventDefault();
  }
}
