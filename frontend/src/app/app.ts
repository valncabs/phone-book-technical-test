import { Component, signal } from '@angular/core';
import { ContactList } from '../app/components/contact-list/contact-list';

@Component({
  imports: [ ContactList],
  selector: 'app-root',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('frontend');
}
