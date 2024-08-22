import {Component, inject, OnInit} from '@angular/core';
import {BookCardComponent} from "../../common-ui/book-card/book-card.component";
import {MatPaginator} from "@angular/material/paginator";
import {NgForOf} from "@angular/common";
import { LoanService } from '../../services/loan.service';
import {LoanResponseDTO} from "../../interfaces/loan-response.dto";
import {AuthService} from "../../services/auth.service";
import {LoanCardComponent} from "../../common-ui/loan-card/loan-card.component";

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [
    BookCardComponent,
    MatPaginator,
    NgForOf,
    LoanCardComponent
  ],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent implements OnInit {
  private loanService = inject(LoanService);
  private authService = inject(AuthService);

  loans: LoanResponseDTO[] = [];

  ngOnInit() {
    const userId = this.authService.getUserIdFromToken();
    if (userId){
      this.loanService.getLoansByUserId(userId).subscribe(
        value => {
          this.loans = value
        }
      )
    }
  }

}
