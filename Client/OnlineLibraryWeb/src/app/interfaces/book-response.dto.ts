import {BookDTO} from "./book.dto";

export interface BookResponseDTO {
  id: string;
  bookDTO: BookDTO;
  image: Uint8Array | null;
}
