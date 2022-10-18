import { PageURL } from "./enum";

export interface Shortcut {
  name: string;
  url: PageURL;
  isActive: boolean;
  subName?: SubName[];
}

export interface SubName {
  url: PageURL;
  name: string;
}

export interface APIResponse<T> {
  resultObj: {
    pageIndex: number;
    pageSize: number;
    items: Array<T>;
    totalRecords: number;
  };
}

export interface UserProfile {
  email: string;
  phoneNumber: string;
  userName: string;
  firstName: string;
  lastName: string;
}

export interface ProductData {
  id: number;
  name: string;
  categoryName: string;
  description: string;
  price: string;
  imageUrl: string;
  time_Created: string;
  time_Updated: string;
}

export interface CategoryData {
  id: number;
  name: string;
  description: string;
  time_Created: string;
  time_Updated: string;
}

