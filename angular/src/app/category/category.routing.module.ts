import { NgModule } from '@angular/core'
import { Route, RouterModule } from '@angular/router'
import { GetCategoriesComponent } from './component/get-categories/get-categories.component'
import { AddCategoryComponent } from './component/add-category/add-category.component'
import { UpdateCategoryComponent } from './component/update-category/update-category.component'

const routes: Route[] = [
    { path: '', component: GetCategoriesComponent },
    { path: 'add-category', component: AddCategoryComponent},
    { path: 'update-category', component: UpdateCategoryComponent}

]

@NgModule({
    imports: [
        RouterModule.forChild(routes)//אלו הנתיבים של המודול הזה, תשתמש בהם כשתצטרך
    ],
})
export class CategoryRoutingModule { }

