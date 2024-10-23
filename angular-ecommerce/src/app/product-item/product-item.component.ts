import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { CartService } from '../cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent {
  @Input() product: any;

  constructor(private cartService: CartService, private router: Router) {}

  addToCart() {
    this.cartService.addToCart();  
  }

  onEdit() {
    this.router.navigate(['/edit-product', this.product.id]);
  }
}
