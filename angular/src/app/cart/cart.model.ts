import { Product, eUnitOfMeasure } from "../product/product.model"

export class Cart{
    id!:number
    userId!:number
    listItem!:CartItem[]
}
export class CartItem{
    Id!:number
    shoppingCartId !:number
    shoppingCart!:Cart
    productId !:number
    product!:Product
    quantity!:number
}

export class PostCart{
    name! : string
    categoryId!: number
    UnitOfMeasure!: eUnitOfMeasure
}