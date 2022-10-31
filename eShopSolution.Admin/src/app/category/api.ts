import { AxiosPromise } from "axios";
import { doDelete, doGet, doPost, doPut } from "../../common/util/baseAPI";
import { CategoryFormInputs } from "../../models/form";

export const doGetSubCategories = (): AxiosPromise<any> => {
  return doGet("api/subCategory/");
};

export const doGetSubCategoriesPaging = (PageIndex: number): AxiosPromise<any> => {
  return doGet("api/subCategory/paging/", { PageIndex });
};

export const doGetParentCategories= (): AxiosPromise<any> => {
  return doGet("api/parentCategory/");
};

export const doPostCategory= (data: CategoryFormInputs): AxiosPromise<any> => {
  return doPost("api/category/", data);
};

export const doPutCategory= (id: number, data: CategoryFormInputs): AxiosPromise<any> => {
  return doPut(`api/category/${id}`, data);
};


export const doDeleteCategory = (id: number): AxiosPromise<any> => {
    return doDelete(`api/category/${id}/`);
};

