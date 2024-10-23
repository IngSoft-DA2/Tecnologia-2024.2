import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../models/Product'; // Asegúrate de importar el modelo de producto
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css'],
  standalone: true,
  imports: [FormsModule]
})
export class ProductFormComponent implements OnInit {
  newProduct: Product = {
    id: 0,
    category: '',
    name: '',
    price: 0
  };

  isEditMode: boolean = false;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,  // Inyecta ActivatedRoute aquí
    private router: Router
  ) {}

  ngOnInit() {
    const productId = this.route.snapshot.paramMap.get('id');
    if (productId) {
      this.isEditMode = true;
      this.productService.getProductById(+productId).subscribe((product) => {
        this.newProduct = product;
      });
    }
  }

  onSubmit() {
    if (this.isEditMode) {
      // Si es edición, llamamos al servicio para actualizar el producto existente
      this.productService.updateProduct(this.newProduct).subscribe({
        next: (response) => {
          console.log('Producto actualizado:', response);
          this.router.navigate(['/products']);  // Redirige al listado de productos después de actualizar
        },
        error: (error) => {
          console.error('Error al actualizar producto:', error);
        }
      });
    } else {
      // Si es un nuevo producto, llamamos al servicio para añadirlo
      this.productService.addProduct(this.newProduct).subscribe({
        next: (response) => {
          console.log('Producto añadido:', response);
          this.router.navigate(['/products']);  // Redirige al listado de productos después de añadir
        },
        error: (error) => {
          console.error('Error al añadir producto:', error);
        }
      });
    }
  }
}