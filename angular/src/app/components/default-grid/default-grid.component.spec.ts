import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultGridComponent } from './default-grid.component';

describe('DefaultGridComponent', () => {
  let component: DefaultGridComponent;
  let fixture: ComponentFixture<DefaultGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DefaultGridComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DefaultGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
