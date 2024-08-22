import {Component, inject, Input} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {
  MatCard,
  MatCardActions,
  MatCardAvatar,
  MatCardContent,
  MatCardHeader,
  MatCardImage, MatCardSubtitle, MatCardTitle
} from "@angular/material/card";
import {LoanService} from "../../services/loan.service";
import {BookResponseDTO} from "../../interfaces/book-response.dto";
import {LoanResponseDTO} from "../../interfaces/loan-response.dto";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-loan-card',
  standalone: true,
  imports: [
    MatButton,
    MatCard,
    MatCardActions,
    MatCardAvatar,
    MatCardContent,
    MatCardHeader,
    MatCardImage,
    MatCardSubtitle,
    MatCardTitle,
    DatePipe
  ],
  templateUrl: './loan-card.component.html',
  styleUrl: './loan-card.component.css'
})
export class LoanCardComponent {
  private loanService = inject(LoanService);
  @Input() loan!: LoanResponseDTO;
}
