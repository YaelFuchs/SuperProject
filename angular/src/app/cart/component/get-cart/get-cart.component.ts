import { Component, OnInit } from '@angular/core';
import { CartItem, PostCart, ProductPriceDto } from '../../cart.model';
import { CartService } from '../../cart.service';
import { Router } from '@angular/router';
import { Product } from '../../../product/product.model';

@Component({
  selector: 'app-get-cart',
  templateUrl: './get-cart.component.html',
  styleUrl: './get-cart.component.scss'
})
export class GetCartComponent implements OnInit {
  carts: CartItem[] = [];
  userId = 0
  shoppingCart!: PostCart
  orderPrice: number = 0
  productPrice!: ProductPriceDto[]
  constructor(private _cartService: CartService, private _router: Router) { }

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
    const c = localStorage.getItem('orderPrice');
    if (c) {
      this.orderPrice = JSON.parse(c);
    }
  }
  getCart() {
    this._cartService.getCartByUserId(this.userId).subscribe({
      next: (res) => {
        this.carts = res;
        const productList = JSON.parse(localStorage.getItem("orderProductList") || "[]");

        if (this.carts.length != productList.length ||
          !this.carts.every((item, index) => item.quantity === productList[index].quantity)
        ) {
          this.orderPrice = 0
          localStorage.removeItem('orderPrice')
          localStorage.removeItem('orderProductList')
          localStorage.removeItem('prices')
        }
        const prices = JSON.parse(localStorage.getItem('prices') || "[]");
        if (prices != null) {
          this.productPrice = prices
        }
      },
      error: (err) => {
        console.log("שגיאה בהבאת סל הקניות של המשתמש", err);
      }
    })
  }
  deleteProductFromCart(product: Product) {
    const p = {
      name: product.name,
      categoryId: product.category.id,
      unitOfMeasure: product.unitOfMeasure
    }
    this._cartService.removeProduct(this.userId, p).subscribe({
      next: (res) => {
        console.log("המוצר נמחק בהצלחה", res);
        this.getCart();
      },
      error: (err) => {
        console.log("המוצר לא הצליח להימחק");
      }
    })
  }
  addProductFromCart(product: Product) {
    this.shoppingCart = {
      name: product.name,
      categoryId: product.category.id,
      UnitOfMeasure: product.unitOfMeasure
    };
    this._cartService.addProduct(this.userId, this.shoppingCart).subscribe({
      next: (res) => {
        console.log("המוצר נוסף בהצלחה", res);
        this.getCart();
      },
      error: (err) => {
        console.log("המוצר לא הצליח להתווסף");
      }
    })
  }
  getPrice() {
    this._cartService.CalculateCheapestCart(this.userId).subscribe({
      next: (res) => {
        console.log("תוצאת האלגוריתם:", res);
        this.orderPrice = res?.cheapestShoppingCartResult?.bestCost
        this.productPrice = res.prices
        localStorage.setItem('orderPrice', JSON.stringify(this.orderPrice))
        localStorage.setItem('orderProductList', JSON.stringify(this.carts))
        localStorage.setItem('prices', JSON.stringify(this.productPrice))
      },
      error: (err) => {
        alert("שגיאה בביצוע ההזמנה")
        console.log("err", err);

      }
    })
  }
  orderCart() {
    this._router.navigate(['paypal', this.userId, this.orderPrice]);
    this._cartService.addCart(this.userId).subscribe({
      next: (res) => {
        console.log("פתיחת סל חזר בוצעה בהצלחה", res);
        localStorage.removeItem('orderPrice');
      },
      error: (err) => {
        console.log("לא נפתח סל חדש");

      }
    })
  }
  getProductPrice(productId: number): number {
    const productprice = this.productPrice.find(p => p.product.id === productId);
    const product = this.carts.find(p => p.product.id === productId);

    if (product && productprice) {
      return product.quantity * productprice.price;
    }
    return 0;
  }
  
  getUnitOfMeasureText(p: Product): string {
   
    return p.unitOfMeasure==0? 'יחידות': 'קילוגרמים';
  }
}
