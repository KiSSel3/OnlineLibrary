import {Component, inject, Input, OnInit} from '@angular/core';
import {BookResponseDTO} from "../../interfaces/book-response.dto";
import {BookService} from "../../services/book.service";
import {Router} from "@angular/router";
import {LoanService} from "../../services/loan.service";
import {ByteArrayToBase64Pipe} from "../../helpers/pipes/byte-array-to-base64.pipe";
import {JsonPipe, NgIf} from "@angular/common";

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [
    ByteArrayToBase64Pipe,
    NgIf,
    JsonPipe
  ],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.css'
})
export class BookCardComponent implements OnInit {
  private bookService = inject(BookService);
  private loanService = inject(LoanService);

  private router = inject(Router)


  isAvailable: boolean = false;
  @Input() book!: BookResponseDTO;

  ngOnInit(): void {
    this.checkAvailability();
  }

  checkAvailability(): void {
    this.loanService.checkBookAvailability(this.book.id).subscribe(
      (availability: boolean) => {
        this.isAvailable = availability;
      },
      error => {
        this.isAvailable = false;
      }
    );
  }

  goToDetails(bookId: string): void {
    this.router.navigate(['/book', bookId]);
  }
}
