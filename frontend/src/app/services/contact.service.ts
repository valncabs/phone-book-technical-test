import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
Contact,
CreateContact,
UpdateContact,
ApiResponse
} from '../models/contact';

@Injectable({
providedIn: 'root'
})
export class ContactService {

private readonly apiUrl = 'http://localhost:5260/api/Contact';

constructor(private http: HttpClient) {}

/**
 * Retrieves all active contacts from the API.
 *
 * @returns An Observable containing the list of contacts.
 */
getAll(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.apiUrl);
}

/**
 * Retrieves a single contact by its identifier.
 *
 * @param id The unique identifier of the contact.
 * @returns An Observable containing the requested contact.
 */
getById(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/${id}`);
}

/**
 * Creates a new contact.
 *
 * @param contact The contact information to be created.
 * @returns An Observable containing the API response with the created contact.
 */
create(contact: CreateContact): Observable<ApiResponse<Contact>> {
    return this.http.post<ApiResponse<Contact>>(
    this.apiUrl,
    contact
    );
}

/**
 * Updates an existing contact.
 *
 * @param id The unique identifier of the contact.
 * @param contact The updated contact information.
 * @returns An Observable containing the API response with the updated contact.
 */
update(
    id: number,
    contact: UpdateContact
): Observable<ApiResponse<Contact>> {
    return this.http.put<ApiResponse<Contact>>(
    `${this.apiUrl}/${id}`,
    contact
    );
}

/**
 * Soft deletes an existing contact.
 *
 * @param id The unique identifier of the contact.
 * @returns An Observable containing the deletion response.
 */
delete(id: number): Observable<unknown> {
    return this.http.delete(`${this.apiUrl}/${id}`);
}
}