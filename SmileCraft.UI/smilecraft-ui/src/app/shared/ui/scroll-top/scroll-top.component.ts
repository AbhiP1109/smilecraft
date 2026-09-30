import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-scroll-top',
  standalone: true,
  imports: [],
  templateUrl: './scroll-top.component.html',
  styleUrl: './scroll-top.component.css'
})
export class ScrollTopComponent {

  visible = false;

  @HostListener('window:scroll')
  onScroll() {

    this.visible = window.scrollY > 300;

  }

  scrollTop() {

    window.scrollTo({

      top: 0,

      behavior: 'smooth'

    });

  }

}
