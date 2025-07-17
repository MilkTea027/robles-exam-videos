import { Category } from './cateogry.model';

export interface Video {
  id: number;
  name: string;
  thumbnail?: string;
  description?: string;
  categoryId: number;
  file?: string;
  size: number;
  category: Category;
}