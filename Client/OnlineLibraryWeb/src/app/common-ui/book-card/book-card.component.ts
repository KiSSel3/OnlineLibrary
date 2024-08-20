import {Component, inject, Input, OnInit} from '@angular/core';
import {BookResponseDTO} from "../../interfaces/book-response.dto";
import {BookService} from "../../services/book.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.css'
})
export class BookCardComponent implements OnInit {
  private bookService = inject(BookService);
  private router = inject(Router)


  isAvailable: boolean = false;
  @Input() book!: BookResponseDTO;

  ngOnInit(): void {
    this.checkAvailability();
  }

  checkAvailability(): void {
    this.bookService.checkBookAvailability(this.book.id).subscribe(
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
