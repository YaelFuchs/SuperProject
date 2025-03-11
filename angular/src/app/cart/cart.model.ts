import { Branch } from "../branch/branch.model"
import { Product, eUnitOfMeasure } from "../product/product.model"

export class Cart {
    id!: number
    userId!: number
    listItem!: CartItem[]
}
export class CartItem {
    Id!: number
    shoppingCartId !: number
    shoppingCart!: Cart
    productId !: number
    product!: Product
    quantity!: number
}

export class PostCart {
    name!: string
    categoryId!: number
    UnitOfMeasure!: eUnitOfMeasure
}
export class ResultDto {
    prices!: ProductPriceDto[]
    cheapestShoppingCartResult!: CheapestShoppingCartResult
}
export class ProductPriceDto {
    product!: Product
    price!: number
}
export class CheapestShoppingCartResult {
    bestCost!: number
    selectedBranch!: Branch[]
    productOrigins!: Map<Product, Branch>
}