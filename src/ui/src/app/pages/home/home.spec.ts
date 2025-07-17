import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Home } from './home';
import { of, throwError } from 'rxjs';
import { VideoService } from '../../_shared/services/video.service';
import { Video } from '../../_shared/video.model';
import { RouterTestingModule } from '@angular/router/testing';
import { CommonModule } from '@angular/common';

describe('Home Component', () => {
  let component: Home;
  let fixture: ComponentFixture<Home>;
  let mockVideoService: jasmine.SpyObj<VideoService>;

  const mockVideos: Video[] = [
    {
      id: 1,
      name: 'Test Video',
      description: 'Test desc',
      file: 'video.mp4',
      thumbnail: 'thumb.jpg',
      size: 123456,
      categoryId: 1,
      category: { id: 1, name: 'Cat 1' }
    }
  ];

  beforeEach(async () => {
    mockVideoService = jasmine.createSpyObj('VideoService', ['getAll']);

    await TestBed.configureTestingModule({
      imports: [
        Home,
        RouterTestingModule,
        CommonModule
      ],
      providers: [
        { provide: VideoService, useValue: mockVideoService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Home);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load videos on init', () => {
    mockVideoService.getAll.and.returnValue(of(mockVideos));

    fixture.detectChanges();

    expect(component.videos.length).toBe(1);
    expect(component.videos[0].name).toBe('Test Video');
  });

  it('should handle errors from getAll', () => {
    spyOn(console, 'error');
    mockVideoService.getAll.and.returnValue(throwError(() => new Error('Failed')));

    fixture.detectChanges();

    expect(console.error).toHaveBeenCalledWith('Error loading videos:', jasmine.any(Error));
    expect(component.videos.length).toBe(0);
  });
});
