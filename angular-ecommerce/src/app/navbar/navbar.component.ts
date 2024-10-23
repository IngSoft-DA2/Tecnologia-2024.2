// navbar.component.ts
import { Component } from '@angular/core';
import { AuthService } from '../auth.service';  // Importamos el servicio de autenticación
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  imports: [CommonModule, RouterModule],

})
export class NavbarComponent {
  constructor(public authService: AuthService, private router: Router) {}

  // Función para hacer logout
  logout() {
    this.authService.logout();
    this.router.navigate(['/']);  // Redirige al inicio después de cerrar sesión
  }
}