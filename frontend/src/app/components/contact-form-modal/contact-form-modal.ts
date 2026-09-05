import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModal } from '../confirm-modal/confirm-modal';

import {
Contact,
CreateContact
} from '../../models/contact';

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

@Input() contact: Contact | null = null;

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
    public activeModal: NgbActiveModal,
    private modalService: NgbModal
) {}

get isEditing(): boolean {
    return this.contact !== null;
}

save(): void {

if (!this.formData.name.trim()) {
    alert('Name is required.');
    return;
}

if (!this.formData.lastName.trim()) {
    alert('Last Name is required.');
    return;
}

if (!this.formData.phoneNumber.trim()) {
    alert('Phone Number is required.');
    return;
}

if (
    this.formData.email &&
    !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.formData.email)
) {
    alert('Please enter a valid email.');
    return;
}

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

cancel(): void {
    this.activeModal.dismiss();
}
}