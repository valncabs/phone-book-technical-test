import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  templateUrl: './confirm-modal.html'
})
export class ConfirmModal {

  @Input() message = 'Are you sure you want to delete this contact?';

  constructor(
    public activeModal: NgbActiveModal
  ) {}

  confirm(): void {
    this.activeModal.close(true);
  }

  cancel(): void {
    this.activeModal.dismiss();
  }
}