/**
 * Represents the available contact types.
 */
export type ContactType = | 'Person' | 'PublicOrganization' | 'PrivateOrganization';

/**
 * Represents the available contact statuses.
 */
export type ContactStatus = | 'Active' | 'Inactive';

/**
 * Represents a contact returned by the API.
 */
    export interface Contact {
        id: number;
        contactType: ContactType;
        name: string;
        lastName: string;
        phoneNumber: string;
        comments: string;
        email?: string;
        governmentLevel?: string;
        industry?: string;
        status: ContactStatus;
        createdAt: string;
        updatedAt: string;
    }

/**
 * Represents the data required to create a contact.
 */
    export interface CreateContact {
        contactType: ContactType;
        name: string;
        lastName: string;
        phoneNumber: string;
        comments: string;
        email?: string;
        governmentLevel?: string;
        industry?: string;
    }

/**
 * Represents the data required to update a contact.
 */
    export interface UpdateContact {
        contactType: ContactType;
        name: string;
        lastName: string;
        phoneNumber: string;
        comments: string;
        email?: string;
        governmentLevel?: string;
        industry?: string;
        status: ContactStatus;
    }

    /**
 * Represents a standard API response containing a message and data.
 *
 * @template T The type of data returned by the API.
 */
export interface ApiResponse<T> {
    message: string;
    data: T;
}