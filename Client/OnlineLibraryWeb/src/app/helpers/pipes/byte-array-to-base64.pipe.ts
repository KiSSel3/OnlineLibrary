import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'byteArrayToBase64',
  standalone: true
})
export class ByteArrayToBase64Pipe implements PipeTransform {
  transform(value: string): string {
    return `data:image/png;base64,${value}`
  }
}
