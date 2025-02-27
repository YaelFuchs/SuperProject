import { NgModule } from '@angular/core'
import { CommonModule, NgClass, NgSwitch, NgSwitchCase } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CategoryRoutingModule } from './category.routing.module';
import { GetCategoriesComponent } from './component/get-categories/get-categories.component';
import { AddCategoryComponent } from './component/add-category/add-category.component';
import { UpdateCategoryComponent } from './component/update-category/update-category.component';
import { GetCategoryIdComponent } from './component/get-category-id/get-category-id.component';

@NgModule({
    declarations: [GetCategoriesComponent, AddCategoryComponent, UpdateCategoryComponent,GetCategoryIdComponent],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        NgClass,
        FormsModule,
        NgSwitch,
        NgSwitchCase,
        CategoryRoutingModule,

    ],
})

export class CategoryModule { }