import {UserResponseDTO} from "./user-response.dto";
import {BookDTO} from "./book.dto";

export interface LoanResponseDTO {
  userDTO: UserResponseDTO;
  bookDTO: BookDTO;
  borrowedAt: Date;
  returnBy: Date;
}
