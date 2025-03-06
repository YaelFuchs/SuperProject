import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBranchProductComponent } from './add-branch-product.component';

describe('AddBranchProductComponent', () => {
  let component: AddBranchProductComponent;
  let fixture: ComponentFixture<AddBranchProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddBranchProductComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBranchProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
