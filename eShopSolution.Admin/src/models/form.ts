export interface LoginFormInputs {
    username: string;
    password: string;
}

export interface CategoryFormInputs {
  name: string;
  description: string
  parentId: number;
}

export interface ProductFormInputs {
  name: string;
  description: string
  price: string;
  categoryId: number;
}
  
  