import {ChangeDetectionStrategy, Component, inject, Input, OnInit} from '@angular/core';
import {BookService} from "../../services/book.service";
import {ActivatedRoute, Router} from "@angular/router";
import {BookDetailsResponseDTO} from "../../interfaces/book-details-response.dto";
import {
  MatCardModule
} from "@angular/material/card";
import {MatButton, MatButtonModule} from "@angular/material/button";

@Component({
  selector: 'app-book-details-page',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule
  ],
  templateUrl: './book-details-page.component.html',
  styleUrl: './book-details-page.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BookDetailsPageComponent implements OnInit {
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);

  isAvailable: boolean = false;
  @Input() bookDetails!: BookDetailsResponseDTO;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.bookService.getBookById(id).subscribe({
        next: (data) => {
          this.bookDetails = data;
        },
        error: (error) => {
          console.error('Ошибка при получении данных о книге:', error);
        }
      });

      this.checkAvailability(id);
    }
  }

  checkAvailability(id: string): void {
    this.bookService.checkBookAvailability(id).subscribe(
      (availability: boolean) => {
        this.isAvailable = availability;
      },
      error => {
        this.isAvailable = false;
      }
    );
  }
}
