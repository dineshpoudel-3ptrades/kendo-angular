import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandCellsComponent } from './command-cells.component';

describe('CommandCellsComponent', () => {
  let component: CommandCellsComponent;
  let fixture: ComponentFixture<CommandCellsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandCellsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandCellsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
