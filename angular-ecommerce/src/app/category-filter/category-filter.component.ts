import { Component, Output, EventEmitter } from '@angular/core';
import { NgFor } from '@angular/common'; 

@Component({
  selector: 'app-category-filter',
  templateUrl: './category-filter.component.html',
  styleUrls: ['./category-filter.component.css'],
  standalone: true,
  imports: [NgFor]  
})
export class CategoryFilterComponent {
  categories = ['Electrónica', 'Hogar', 'Ropa'];
  selectedCategories: string[] = [];

  @Output() categoryChange = new EventEmitter<string[]>();

  onCategoryChange(category: string, event: any) {
    if (event.target.checked) {
      this.selectedCategories.push(category);
    } else {
      const index = this.selectedCategories.indexOf(category);
      this.selectedCategories.splice(index, 1);
    }
    this.categoryChange.emit(this.selectedCategories);
  }
}