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

/**
 * ContactList Component
 * ---------------------
 * Displays and manages a list of active contacts.
 * Provides filtering, pagination, creation, editing, and deletion
 * functionalities through modal dialogs.
 */
@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [
    CommonModule, 
  ],
  templateUrl: './contact-list.html'
})
export class ContactList implements OnInit {

  /** Full list of contacts loaded from the service. */
  contacts: Contact[] = [];

  /** Loading state for contact retrieval. */
  loading = false;

  /** Error message displayed when an operation fails. */
  errorMessage = '';

  /** Current page number in pagination. */
  currentPage = 1;

  /** Number of contacts displayed per page. */
  pageSize = 6;

  /**
   * Contact types currently selected in filters.
   * Used to display only contacts of the chosen types.
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

  /**
   * Angular lifecycle hook.
   * Loads contacts when the component initializes.
   */
  ngOnInit(): void {
    this.loadContacts();
  }

  /**
   * Loads all active contacts from the API.
   * Handles loading and error states.
   */
  loadContacts(): void {
    this.loading = true;
    this.errorMessage = '';

    this.contactService.getAll().subscribe({
      next: (contacts) => {
        this.contacts = contacts;
        this.loading = false;
        this.cdr.detectChanges();
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
   * Returns contacts filtered by selected contact types.
   */
  get filteredContacts(): Contact[] {
    return this.contacts.filter(contact =>
      this.selectedContactTypes.includes(contact.contactType)
    );
  }

  /**
   * Calculates the total number of pages based on filtered contacts.
   */
  get totalPages(): number {
    return Math.ceil(this.filteredContacts.length / this.pageSize);
  }

  /**
   * Returns the contacts for the current page.
   */
  get paginatedContacts(): Contact[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.filteredContacts.slice(startIndex, endIndex);
  }

  /**
   * Changes the current page if the number is valid.
   * @param page Page number to switch to.
   */
  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
  }

  /**
   * Adds or removes a contact type from active filters.
   * @param contactType Contact type to toggle.
   * @param checked Filter state (true = active).
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

  /**
   * Opens the modal to create a new contact.
   * On confirmation, sends the creation request and reloads the list.
   */
  openAddModal(): void {
    const modalRef = this.modalService.open(ContactFormModal);

    modalRef.result.then(
      (result) => {
        this.contactService.create(result).subscribe({
          next: () => this.loadContacts(),
          error: () => {
            this.errorMessage = 'Could not create contact.';
            this.cdr.detectChanges();
          }
        });
      },
      () => console.log('Modal dismissed')
    );
  }

  /**
   * Opens the modal to edit an existing contact.
   * @param contact Contact to edit.
   */
  openEditModal(contact: Contact): void {
    const modalRef = this.modalService.open(ContactFormModal);

    modalRef.componentInstance.contact = contact;
    modalRef.componentInstance.formData = {
      contactType: contact.contactType,
      name: contact.name,
      lastName: contact.lastName || '',
      phoneNumber: contact.phoneNumber,
      comments: contact.comments,
      email: contact.email || '',
      governmentLevel: contact.governmentLevel || '',
      industry: contact.industry || ''
    };

    modalRef.result.then(
      (result) => {
        this.contactService.update(contact.id, result).subscribe({
          next: () => this.loadContacts(),
          error: () => {
            this.errorMessage = 'Could not update contact.';
            this.cdr.detectChanges();
          }
        });
      },
      () => console.log('Modal dismissed')
    );
  }

  /**
   * Opens the confirmation modal to delete a contact.
   * @param contact Contact to delete.
   */
  openDeleteModal(contact: Contact): void {
    const modalRef = this.modalService.open(ConfirmModal);

    modalRef.componentInstance.message =
      'Are you sure you want to delete this contact?';

    modalRef.result.then(
      (result) => {
        if (result === true) {
          this.contactService.delete(contact.id).subscribe({
            next: () => this.loadContacts(),
            error: () => {
              this.errorMessage = 'Could not delete contact.';
              this.cdr.detectChanges();
            }
          });
        }
      },
      () => console.log('Delete cancelled')
    );
  }
}
