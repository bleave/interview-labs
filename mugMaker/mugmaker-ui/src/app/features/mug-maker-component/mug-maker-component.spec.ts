import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MugMakerComponent } from './mug-maker-component';

describe('MugMakerComponent', () => {
  let component: MugMakerComponent;
  let fixture: ComponentFixture<MugMakerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MugMakerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MugMakerComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
