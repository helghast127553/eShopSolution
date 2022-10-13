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
