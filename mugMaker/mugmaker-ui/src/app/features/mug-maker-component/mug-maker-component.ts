import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MugMakerService, MugDto } from '../../api/mug-maker-service';
import { finalize } from 'rxjs/operators';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-mug-maker',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './mug-maker-component.html',
  styleUrls: ['./mug-maker-component.scss']
})
export class MugMakerComponent {
  saying = '';
  created?: MugDto;
  errorMsg?: string;
  isSubmitting = false;

  constructor(private mugApi: MugMakerService, private cdr: ChangeDetectorRef) { }

  create(): void {
    this.errorMsg = undefined;
    this.created = undefined;

    const text = this.saying.trim();
    if (!text) {
      this.errorMsg = 'Please enter a saying.';
      return;
    }

    this.isSubmitting = true;

    this.mugApi.create(text)
      .pipe(finalize(() => {
        this.isSubmitting = false;
        this.cdr.detectChanges();
        console.log('Finalize ran — resetting isSubmitting');
      }))
      .subscribe({
        next: (mug) => {
          this.created = mug;
          this.errorMsg = undefined;
        },
        error: (err: any) => {
          this.errorMsg = err?.message ?? err?.title ?? 'Request failed.';
        }
      });
  }
}
