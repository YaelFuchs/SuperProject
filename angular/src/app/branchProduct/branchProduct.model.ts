import { Branch } from "../branch/branch.model"
import { Product } from "../product/product.model"

export class BranchProduct{
    id!:number 
    branch!:Branch
    product !:Product
    price!: number

}
export class BranchProductPostModel{
    id!:number 
    branchId!:number
    productId !:number
    price!: number
}