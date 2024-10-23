// auth.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = false;

  // Función de login básica
  login(username: string, password: string): boolean {
    if (username === 'user' && password === 'password') {
      this.loggedIn = true;
      localStorage.setItem('token', 'your-token');
      return true;
    }
    return false;
  }

  // Función para cerrar sesión
  logout() {
    this.loggedIn = false;
    localStorage.removeItem('token');
  }

  // Verificar si está logueado
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}