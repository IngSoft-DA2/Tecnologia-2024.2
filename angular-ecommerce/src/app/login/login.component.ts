// login.component.ts
import { Component } from '@angular/core';
import { AuthService } from '../auth.service';  // Importamos el servicio de autenticación
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [FormsModule]
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    // Autenticar al usuario usando el AuthService
    if (this.authService.login(this.username, this.password)) {
      this.router.navigate(['/products']);  // Redirige a la página de productos después del login
    } else {
      alert('Usuario o contraseña incorrectos');
    }
  }
}