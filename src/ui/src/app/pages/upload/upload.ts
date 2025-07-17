import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VideoService } from '../../_shared/services/video.service';
import { CategoryService } from '../../_shared/services/category.service';
import { environment } from '../../../environments/environment.local';
import { Category } from '../../_shared/cateogry.model';

@Component({
  selector: 'app-upload',
  imports: [CommonModule, FormsModule],
  standalone: true,
  templateUrl: './upload.html',
  styleUrl: './upload.scss'
})
export class Upload implements OnInit {
  env = environment;
  name: string = '';
  description: string = '';
  categoryId: number | null = null;
  file: File | null = null;
  thumbnail: File | null = null;
  uploadSuccess = false;
  uploadError = '';
  categories: Category[] = [];
  loadingCategories = true;

  constructor(private videoService: VideoService, private categoryService: CategoryService) {}
  
  ngOnInit(): void {
    this.categoryService.getAll().subscribe({
      next: (response) => {
        this.categories = response;
        this.loadingCategories = false;
      },
      error: (err) => {
        console.error('Failed to load categories:', err);
        this.loadingCategories = false;
      }
    });
  }

  onFileSelected(event: Event, isThumbnail = false) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      if (!isThumbnail) {
        const allowedTypes = ['video/mp4', 'video/avi', 'video/quicktime'];

        if (!allowedTypes.includes(file.type)) {
          this.uploadError = 'Only MP4, AVI, or MOV files are allowed.';
          this.file = null;
          return;
        }
        
        this.file = file;
      } else {
        this.thumbnail = file;
      }
  }
}

  uploadVideo() {
    if (!this.name || !this.categoryId || !this.file || !this.thumbnail) {
      this.uploadError = 'Please fill all fields and select files.';
      return;
    }

    const formData = new FormData();
    formData.append('Name', this.name);
    formData.append('Description', this.description);
    formData.append('CategoryId', this.categoryId.toString());
    formData.append('File', this.file);
    formData.append('Thumbnail', this.thumbnail);

    this.videoService.upload(formData).subscribe({
      next: () => {
        this.uploadSuccess = true;
        this.uploadError = '';
      },
      error: (err) => {
        this.uploadError = 'Upload failed.';
        console.error('Upload error:', err);
      }
    });
  }
}
