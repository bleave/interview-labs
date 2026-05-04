import { TestBed } from '@angular/core/testing';

import { MugMakerService } from './mug-maker-service';

describe('MugMakerService', () => {
  let service: MugMakerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MugMakerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
