import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CotizacionesCryptoComponent } from './cotizaciones-crypto.component';

describe('CotizacionesCryptoComponent', () => {
  let component: CotizacionesCryptoComponent;
  let fixture: ComponentFixture<CotizacionesCryptoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CotizacionesCryptoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CotizacionesCryptoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
