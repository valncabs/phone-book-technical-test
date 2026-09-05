import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

/**
 * ConfirmModal Component
 * ----------------------
 * A simple confirmation dialog used to ask the user
 * to confirm or cancel an action (e.g., deleting a contact).
 * 
 * The modal returns `true` when confirmed, and dismisses otherwise.
 */
@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  templateUrl: './confirm-modal.html'
})
export class ConfirmModal {

  /**
   * Message displayed in the confirmation dialog.
   * Defaults to a delete confirmation message.
   */
  @Input() message = 'Are you sure you want to delete this contact?';

  constructor(
    /** Active modal instance used to close or dismiss the modal. */
    public activeModal: NgbActiveModal
  ) {}

  /**
   * Confirms the action and closes the modal,
   * returning `true` to the caller.
   */
  confirm(): void {
    this.activeModal.close(true);
  }

  /**
   * Cancels the action and dismisses the modal
   * without returning a value.
   */
  cancel(): void {
    this.activeModal.dismiss();
  }
}
