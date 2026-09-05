import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';

import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-list.html'
})
export class ContactList implements OnInit {

  contacts: Contact[] = [];

  loading = false;
  errorMessage = '';

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
    private cdr: ChangeDetectorRef
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
}