import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtraInformationComponent } from './extra-information.component';

describe('ExtraInformationComponent', () => {
  let component: ExtraInformationComponent;
  let fixture: ComponentFixture<ExtraInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExtraInformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExtraInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
