import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ContactFormModal } from '../contact-form-modal/contact-form-modal';
import { CommonModule } from '@angular/common';
import { ConfirmModal } from '../confirm-modal/confirm-modal';
import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [
    CommonModule, 
  ],
  templateUrl: './contact-list.html'
})
export class ContactList implements OnInit {

  contacts: Contact[] = [];

  loading = false;
  errorMessage = '';

  currentPage = 1;
  pageSize = 6;

  /**
   * Contact types currently selected in the filters.
   */
  selectedContactTypes: string[] = [
    'Person',
    'PublicOrganization',
    'PrivateOrganization'
  ];

  constructor(
    private contactService: ContactService,
    private cdr: ChangeDetectorRef,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.loadContacts();
  }

  /**
   * Loads all active contacts from the API.
   */
  loadContacts(): void {
    this.loading = true;
    this.errorMessage = '';

    this.contactService.getAll().subscribe({
      next: (contacts) => {
        console.log('CONTACTS RECEIVED:', contacts);

        this.contacts = contacts;
        this.loading = false;

        this.cdr.detectChanges();

        console.log('LOADING:', this.loading);
        console.log('CONTACTS:', this.contacts);
      },

      error: (error) => {
        console.error('ERROR LOADING CONTACTS:', error);

        this.errorMessage = 'Could not load contacts.';
        this.loading = false;

        this.cdr.detectChanges();
      }
    });
  }

  /**
   * Returns contacts according to the selected contact type filters.
   */
  get filteredContacts(): Contact[] {
    return this.contacts.filter(contact =>
      this.selectedContactTypes.includes(contact.contactType)
    );
  }

  get totalPages(): number {
  return Math.ceil(this.filteredContacts.length / this.pageSize);
}

get paginatedContacts(): Contact[] {
  const startIndex = (this.currentPage - 1) * this.pageSize;
  const endIndex = startIndex + this.pageSize;

  return this.filteredContacts.slice(startIndex, endIndex);
}

changePage(page: number): void {
  if (page < 1 || page > this.totalPages) {
    return;
  }

  this.currentPage = page;
}

  /**
   * Adds or removes a contact type from the active filters.
   */
  toggleFilter(contactType: string, checked: boolean): void {

    if (checked) {

      if (!this.selectedContactTypes.includes(contactType)) {
        this.selectedContactTypes.push(contactType);
      }

    } else {

      this.selectedContactTypes =
        this.selectedContactTypes.filter(type => type !== contactType);
    }
  }

  openAddModal(): void {
  const modalRef = this.modalService.open(ContactFormModal);

  modalRef.result.then(
    (result) => {
      console.log('Contact to create:', result);

      this.contactService.create(result).subscribe({
        next: (response) => {
          console.log('Contact created:', response);

          this.loadContacts();
        },

        error: (error) => {
          console.error('ERROR CREATING CONTACT:', error);

          this.errorMessage = 'Could not create contact.';
          this.cdr.detectChanges();
        }
      });
    },

    () => {
      console.log('Modal dismissed');
    }
  );
}

openEditModal(contact: Contact): void {
  const modalRef = this.modalService.open(ContactFormModal);

  modalRef.componentInstance.contact = contact;

  modalRef.componentInstance.formData = {
    contactType: contact.contactType,
    name: contact.name,
    lastName: contact.lastName,
    phoneNumber: contact.phoneNumber,
    comments: contact.comments,
    email: contact.email || '',
    governmentLevel: contact.governmentLevel || '',
    industry: contact.industry || ''
  };

  modalRef.result.then(
    (result) => {
      console.log('Contact to update:', result);

      this.contactService.update(contact.id, result).subscribe({
        next: (response) => {
          console.log('Contact updated:', response);

          this.loadContacts();
        },

        error: (error) => {
          console.error('ERROR UPDATING CONTACT:', error);

          this.errorMessage = 'Could not update contact.';
          this.cdr.detectChanges();
        }
      });
    },

    () => {
      console.log('Modal dismissed');
    }
  );
}

openDeleteModal(contact: Contact): void {
  const modalRef = this.modalService.open(ConfirmModal);

  modalRef.componentInstance.message =
    'Are you sure you want to delete this contact?';

  modalRef.result.then(
    (result) => {
      if (result === true) {

        this.contactService.delete(contact.id).subscribe({
          next: () => {
            console.log('Contact deleted:', contact.id);

            this.loadContacts();
          },

          error: (error) => {
            console.error('ERROR DELETING CONTACT:', error);

            this.errorMessage = 'Could not delete contact.';
            this.cdr.detectChanges();
          }
        });

      }
    },

    () => {
      console.log('Delete cancelled');
    }
  );
}

}