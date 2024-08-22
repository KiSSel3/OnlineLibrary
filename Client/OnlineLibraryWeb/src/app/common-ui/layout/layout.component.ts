import {Component, inject} from '@angular/core';
import {Router, RouterLink, RouterOutlet} from "@angular/router";
import {AuthService} from "../../services/auth.service";
import {NgIf} from "@angular/common";
import {BorrowedDialogComponent} from "../borrowed-dialog/borrowed-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {GenreCreateDialogComponent} from "../genre-create-dialog/genre-create-dialog.component";
import {AuthorCreateDialogComponent} from "../author-create-dialog/author-create-dialog.component";
import {BookCreateDialogComponent} from "../book-create-dialog/book-create-dialog.component";

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    NgIf
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
  private authService = inject(AuthService);
  private router = inject(Router)
  private dialog = inject(MatDialog);

  userData = this.authService.getUserFromToken();
  isAuthenticated = this.authService.isAuth;
  isAdmin = this.authService.isAdmin();

  logout() {
    const userId = this.authService.getUserIdFromToken();
    if (userId) {
      this.authService.logout(userId).subscribe();
      this.authService.clearCookies();

      this.router.navigate(['/login']);
    }
  }

  AddGenre() {
    const dialogRef = this.dialog.open(GenreCreateDialogComponent);
  }

  AddAuthor() {
    const dialogRef = this.dialog.open(AuthorCreateDialogComponent);
  }

  AddBook() {
    const dialogRef = this.dialog.open(BookCreateDialogComponent)
  }
}
