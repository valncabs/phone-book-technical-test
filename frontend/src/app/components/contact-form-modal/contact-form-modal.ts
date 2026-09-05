import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

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
    public activeModal: NgbActiveModal
) {}

get isEditing(): boolean {
    return this.contact !== null;
}

save(): void {
    this.activeModal.close(this.formData);
}

cancel(): void {
    this.activeModal.dismiss();
}
}