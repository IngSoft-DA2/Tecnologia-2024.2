import { Component } from '@angular/core';
import { NgFor } from '@angular/common';  
import { CategoryFilterComponent } from '../category-filter/category-filter.component';
import { ProductItemComponent } from '../product-item/product-item.component';
import { Product } from '../models/Product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  standalone: true,
  imports: [NgFor, CategoryFilterComponent, ProductItemComponent]  
})
export class ProductListComponent {
  products = [
    { id: 1, name: 'Producto 1', category: 'Electrónica', price: 100 },
    { id: 2, name: 'Producto 2', category: 'Hogar', price: 200 },
    { id: 3, name: 'Producto 3', category: 'Ropa', price: 300 }
  ];
  filteredProducts = [...this.products];

  apiProducts: Product[] = []; //services
  constructor(private productService: ProductService) {}
  ngOnInit(): void {
    this.loadProducts();
  }


  onCategoryChange(selectedCategories: string[]) {
    if (selectedCategories.length === 0) {
      this.filteredProducts = [...this.products];
    } else {
      this.filteredProducts = this.products.filter(product =>
        selectedCategories.includes(product.category)
      );
    }
  }
  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data) => this.apiProducts = data,  
      error: (err) => console.error('Error:', err)  
    });
  }
}