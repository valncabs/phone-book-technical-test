import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModal } from '../confirm-modal/confirm-modal';

import {
Contact,
CreateContact
} from '../../models/contact';

/**
 * ContactFormModal Component
 * --------------------------
 * Modal dialog for creating or editing a contact.
 * Provides form validation and confirmation before saving.
 */
@Component({
selector: 'app-contact-form-modal',
standalone: true,
imports: [
    CommonModule,
    FormsModule
],
templateUrl: './contact-form-modal.html',
})
export class ContactFormModal {

/** Contact to edit. If null, the modal is used for creating a new contact. */
@Input() contact: Contact | null = null;

/** Form data bound to the contact form fields. */
formData: CreateContact = {
    contactType: 'Person',
    name: '',
    lastName: '',
    phoneNumber: '',
    comments: '',
    email: '',
    governmentLevel: '',
    industry: ''
};

constructor(
    /** Active modal instance used to close or dismiss the modal. */
    public activeModal: NgbActiveModal,
    /** Service used to open confirmation modals. */
    private modalService: NgbModal
) {}

/**
 * Indicates whether the modal is in editing mode.
 * Returns true if a contact is provided, false otherwise.
 */
get isEditing(): boolean {
    return this.contact !== null;
}

/**
 * Validates the form and opens a confirmation modal before saving.
 * If confirmed, closes the modal and returns the form data.
 */
save(): void {
    // Validate required fields
    if (!this.formData.name.trim()) {
    alert('Name is required.');
    return;
    }

    if (!this.formData.phoneNumber.trim()) {
    alert('Phone Number is required.');
    return;
    }

    // Validate email format if provided
    if (
    this.formData.email &&
    !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.formData.email)
    ) {
    alert('Please enter a valid email.');
    return;
    }

    // Open confirmation modal
    const confirmModal = this.modalService.open(ConfirmModal);

    confirmModal.componentInstance.message = this.isEditing
    ? 'Are you sure you want to save these changes?'
    : 'Are you sure you want to create this contact?';

    confirmModal.result.then(
    (confirmed) => {
        if (confirmed === true) {
        this.activeModal.close(this.formData);
        }
    },
    () => {
        console.log('Confirmation cancelled');
    }
    );
}

/**
 * Cancels the operation and dismisses the modal without saving.
 */
cancel(): void {
    this.activeModal.dismiss();
}
}
