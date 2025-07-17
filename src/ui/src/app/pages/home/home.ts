import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { VideoService } from '../../_shared/services/video.service';
import { Video } from '../../_shared/video.model';
import { environment } from '../../../environments/environment.local';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})

export class Home implements OnInit {
  private videoService = inject(VideoService);
  videos: Video[] = [];
  env = environment;

  ngOnInit(): void {
    this.videoService.getAll().subscribe({
      next: (res) => this.videos = res,
      error: (err) => console.error('Error loading videos:', err)
    });
  }

}
