import {ChangeDetectionStrategy, Component, inject, Input, OnInit} from '@angular/core';
import {BookService} from "../../services/book.service";
import {ActivatedRoute, Router} from "@angular/router";
import {BookDetailsResponseDTO} from "../../interfaces/book-details-response.dto";
import {
  MatCardModule
} from "@angular/material/card";
import {MatButton, MatButtonModule} from "@angular/material/button";
import {MatDialog} from "@angular/material/dialog";
import {BorrowedDialogComponent} from "../../common-ui/borrowed-dialog/borrowed-dialog.component";
import {NgIf} from "@angular/common";
import {AuthService} from "../../services/auth.service";
import {LoanService} from "../../services/loan.service";
import {ByteArrayToBase64Pipe} from "../../helpers/pipes/byte-array-to-base64.pipe";
import {BookEditDialogComponent} from "../../common-ui/book-edit-dialog/book-edit-dialog.component";
import {DeleteConfirmDialogComponent} from "../../common-ui/delete-confirm-dialog/delete-confirm-dialog.component";

@Component({
  selector: 'app-book-details-page',
  standalone: true,
    imports: [
        MatCardModule,
        MatButtonModule,
        NgIf,
        ByteArrayToBase64Pipe
    ],
  templateUrl: './book-details-page.component.html',
  styleUrl: './book-details-page.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BookDetailsPageComponent implements OnInit {
  private bookService = inject(BookService);
  private authService = inject(AuthService);
  private loanService = inject(LoanService);
  private router = inject(Router)
  private route = inject(ActivatedRoute);
  private dialog = inject(MatDialog);

  isAvailable: boolean = false;
  isAdmin = this.authService.isAdmin();
  bookId: string | null = null;
  bookDetails!: BookDetailsResponseDTO;

  ngOnInit(): void {
    this.bookId = this.route.snapshot.paramMap.get('id');
    if (this.bookId) {
      this.checkAvailability(this.bookId);

      this.bookService.getBookById(this.bookId).subscribe({
        next: (data) => {
          console.log(data);
          this.bookDetails = data;
        },
        error: (error) => {
          console.error('Ошибка при получении данных о книге:', error);
        }
      });
    }
  }

  checkAvailability(id: string): void {
    this.loanService.checkBookAvailability(id).subscribe(
      (availability: boolean) => {
        this.isAvailable = availability;
      },
      error => {
        this.isAvailable = false;
      }
    );
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(BorrowedDialogComponent)
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        const userId = this.authService.getUserIdFromToken();

        const data = {
          bookId: this.bookId,
          userId: userId,
          dayCount: result
        }

        this.loanService.createNewLoan(data).subscribe()
        this.router.navigate(['']);
      }
    });
  }

  openUpdateDialog(book: BookDetailsResponseDTO): void {
    const dialogRef = this.dialog.open(BookEditDialogComponent, {
      data: { book }
    });

    dialogRef.afterClosed().subscribe();
  }

  openDeleteConfirmDialog(bookId: string): void {
    const dialogRef = this.dialog.open(DeleteConfirmDialogComponent, {
      data: { message: 'Are you sure you want to delete this book?' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteBook(bookId);
      }
    });
  }

  deleteBook(bookId: string): void {
    this.bookService.deleteBook(bookId).subscribe({
      next: () => {
        console.log('Book deleted successfully');
        this.router.navigate(['']);
      },
      error: (error) => {
        console.error('Error deleting book', error);
      }
    });
  }
}
