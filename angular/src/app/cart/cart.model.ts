import { Product } from "../product/product.model"

export class Cart{
    id!:number
    userId!:number
    listItem!:CartItem[]
}
export class CartItem{
    Id!:number
    ShoppingCartId !:number
    ShoppingCart!:Cart
    ProductId !:number
    Product!:Product
    Quantity!:number
}