import { Category } from "../category/category.model"

export enum eUnitOfMeasure // הגדרת ה-enum מחוץ למחלקה
    {
        Units=0, // נמכר ביחידות
        Kilograms=1 // נמכר בק"ג
    }

export class Product{
    id! : number
    name! : string
    category!: Category
    UnitOfMeasure!: eUnitOfMeasure
    imageUrl?: string
}

export class PostProduct{
    name! : string
    categoryId!: number
    UnitOfMeasure!: eUnitOfMeasure
    ImageUrl?: string
}