import { TestBed } from '@angular/core/testing';

import { CameraFilterApiService } from './camera-filter-api.service';

describe('CameraFilterApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CameraFilterApiService = TestBed.get(CameraFilterApiService);
    expect(service).toBeTruthy();
  });
});
