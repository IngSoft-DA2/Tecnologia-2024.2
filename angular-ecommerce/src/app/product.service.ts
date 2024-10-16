import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from './models/Product'; 

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://b2af-167-62-224-159.ngrok-free.app/api/products'; 

  constructor(private http: HttpClient) {}

  
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }
}