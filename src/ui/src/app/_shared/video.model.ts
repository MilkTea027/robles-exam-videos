export interface Category {
  id: number;
  name: string;
}

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