import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeControlComponent } from './type-control.component';

describe('TypeControlComponent', () => {
  let component: TypeControlComponent;
  let fixture: ComponentFixture<TypeControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TypeControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
