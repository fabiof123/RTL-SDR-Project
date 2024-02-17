import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabellaComponent } from './tabella.component';

describe('TabellaComponent', () => {
  let component: TabellaComponent;
  let fixture: ComponentFixture<TabellaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabellaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TabellaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
