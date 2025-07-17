import { Routes } from '@angular/router';
import { Home } from './pages/home/home'
import { Upload } from './pages/upload/upload';
import { Stream } from './pages/stream/stream';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: Home },
    { path: 'upload', component: Upload },
    { path: 'stream/:id', component: Stream },
];
