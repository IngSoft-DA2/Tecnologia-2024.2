import { Routes } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { CartComponent } from './cart/cart.component';
import { AuthGuard } from './auth.guard';
import { LoginComponent } from './login/login.component';
import { ProductFormComponent } from './product-form/product-form.component';

export const routes: Routes = [
 { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },  // Protegido por AuthGuard
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },  // Protegido por AuthGuard
  { path: 'new-product', component: ProductFormComponent },
  { path: 'edit-product/:id', component: ProductFormComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },  // Ruta para el Login
];