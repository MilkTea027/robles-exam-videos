import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { Upload } from './upload';
import { VideoService } from '../../_shared/services/video.service';
import { CategoryService } from '../../_shared/services/category.service';
import { Category } from '../../_shared/cateogry.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

describe('Upload Component', () => {
  let component: Upload;
  let fixture: ComponentFixture<Upload>;
  let mockVideoService: jasmine.SpyObj<VideoService>;
  let mockCategoryService: jasmine.SpyObj<CategoryService>;

  const mockCategories: Category[] = [
    { id: 1, name: 'Action' },
    { id: 2, name: 'Drama' }
  ];

  beforeEach(async () => {
    mockVideoService = jasmine.createSpyObj('VideoService', ['upload']);
    mockCategoryService = jasmine.createSpyObj('CategoryService', ['getAll']);

    await TestBed.configureTestingModule({
      imports: [Upload, CommonModule, FormsModule],
      providers: [
        { provide: VideoService, useValue: mockVideoService },
        { provide: CategoryService, useValue: mockCategoryService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Upload);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load categories on init', () => {
    mockCategoryService.getAll.and.returnValue(of(mockCategories));
    fixture.detectChanges(); // triggers ngOnInit

    expect(component.categories).toEqual(mockCategories);
    expect(component.loadingCategories).toBeFalse();
  });

  it('should handle category load error', () => {
    const consoleSpy = spyOn(console, 'error');
    mockCategoryService.getAll.and.returnValue(throwError(() => new Error('Load fail')));

    fixture.detectChanges();

    expect(consoleSpy).toHaveBeenCalledWith('Failed to load categories:', jasmine.any(Error));
    expect(component.loadingCategories).toBeFalse();
  });

  it('should set upload error if video file is invalid', () => {
    const fakeEvent = {
      target: { files: [new File(['data'], 'test.txt', { type: 'text/plain' })] }
    } as unknown as Event;

    component.onFileSelected(fakeEvent);

    expect(component.file).toBeNull();
    expect(component.uploadError).toBe('Only MP4, AVI, or MOV files are allowed.');
  });

  it('should accept valid video file', () => {
    const validFile = new File(['data'], 'video.mp4', { type: 'video/mp4' });

    const fakeEvent = {
      target: { files: [validFile] }
    } as unknown as Event;

    component.onFileSelected(fakeEvent);
    expect(component.file).toBe(validFile);
    expect(component.uploadError).toBe('');
  });

  it('should accept thumbnail image file', () => {
    const thumbnailFile = new File(['data'], 'thumb.jpg', { type: 'image/jpeg' });

    const fakeEvent = {
      target: { files: [thumbnailFile] }
    } as unknown as Event;

    component.onFileSelected(fakeEvent, true);
    expect(component.thumbnail).toBe(thumbnailFile);
  });

  it('should set upload error if form is incomplete', () => {
    component.uploadVideo();
    expect(component.uploadError).toBe('Please fill all fields and select files.');
  });

  it('should call videoService.upload on valid upload', () => {
    const mockResponse = of({});
    mockVideoService.upload.and.returnValue(mockResponse);

    component.name = 'Test';
    component.description = 'Desc';
    component.categoryId = 1;
    component.file = new File(['video'], 'video.mp4', { type: 'video/mp4' });
    component.thumbnail = new File(['thumb'], 'thumb.jpg', { type: 'image/jpeg' });

    component.uploadVideo();

    expect(mockVideoService.upload).toHaveBeenCalled();
    expect(component.uploadSuccess).toBeTrue();
    expect(component.uploadError).toBe('');
  });

  it('should handle upload error', () => {
    const consoleSpy = spyOn(console, 'error');
    mockVideoService.upload.and.returnValue(throwError(() => new Error('Upload fail')));

    component.name = 'Test';
    component.description = 'Desc';
    component.categoryId = 1;
    component.file = new File(['video'], 'video.mp4', { type: 'video/mp4' });
    component.thumbnail = new File(['thumb'], 'thumb.jpg', { type: 'image/jpeg' });

    component.uploadVideo();

    expect(component.uploadSuccess).toBeFalse();
    expect(component.uploadError).toBe('Upload failed.');
    expect(consoleSpy).toHaveBeenCalledWith('Upload error:', jasmine.any(Error));
  });
});
