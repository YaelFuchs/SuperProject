import { Component, OnInit } from '@angular/core';
import { CartItem, PostCart } from '../../cart.model';
import { CartService } from '../../cart.service';
import { Router } from '@angular/router';
import { PostProduct, Product } from '../../../product/product.model';

@Component({
  selector: 'app-get-cart',
  templateUrl: './get-cart.component.html',
  styleUrl: './get-cart.component.scss'
})
export class GetCartComponent implements OnInit{
  carts:CartItem[] = [];
  userId=0
  shoppingCart!: PostCart
  constructor(private _cartService: CartService, private _router : Router){}

  ngOnInit(): void {
    const authDataString = localStorage.getItem('authToken');
    if (authDataString) {
      try {
        const authData = JSON.parse(authDataString);
        this.userId = authData.userId || 0;
        console.log('User ID מ-localStorage:', this.userId);
      } catch (error) {
        console.error('שגיאה בפרסור authToken:', error);
        this.userId = 0;
      }
    } else {
      console.log('אין authToken ב-localStorage');
    }
    this.getCart();
  }
getCart(){
  this._cartService.getCartByUserId(this.userId).subscribe({
    next: (res)=>{
      this.carts = res;
      console.log("הסל שחוזר למשתמש: ", res,);
    },
    error: (err)=>{
      console.log("שגיאה בהבאת סל הקניות של המשתמש", err);
      
    }
  })
}
deleteProductFromCart(product: Product){
  const p = {
    name: product.name,
    categoryId: product.category.id,
    UnitOfMeasure: product.UnitOfMeasure
  }
  this._cartService.removeProduct(this.userId ,p).subscribe({
    next:(res)=>{
      console.log("המוצר נמחק בהצלחה", res);
      this.getCart();
    },
    error: (err)=>{
      console.log("המוצר לא הצליח להימחק");
      
    }
  })
}
addProductFromCart(product: Product){
   this.shoppingCart = {
      name: product.name, 
      categoryId: product.category.id,
      UnitOfMeasure: product.UnitOfMeasure
  };
  this._cartService.addProduct(this.userId ,this.shoppingCart).subscribe({
    next:(res)=>{
      console.log("המוצר נוסף בהצלחה", res);
      this.getCart();
    },
    error: (err)=>{
      console.log("המוצר לא הצליח להתווסף");
      
    }
  })
}

orderCart(){
  
}
}
