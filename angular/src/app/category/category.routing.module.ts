import { NgModule } from '@angular/core'
import { Route, RouterModule } from '@angular/router'
import { GetCategoriesComponent } from './component/get-categories/get-categories.component'
import { AddCategoryComponent } from './component/add-category/add-category.component'
import { UpdateCategoryComponent } from './component/update-category/update-category.component'
import { GetCategoryIdComponent } from './component/get-category-id/get-category-id.component'

const routes: Route[] = [
    { path: '', component: GetCategoriesComponent },
    { path: 'add-category', component: AddCategoryComponent },
    { path: 'update-category', component: UpdateCategoryComponent },
    { path: 'get-category-id/:id', component: GetCategoryIdComponent }

]

@NgModule({
    imports: [
        RouterModule.forChild(routes)//אלו הנתיבים של המודול הזה, תשתמש בהם כשתצטרך
    ],
})
export class CategoryRoutingModule { }

