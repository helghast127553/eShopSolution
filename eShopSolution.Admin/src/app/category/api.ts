import { AxiosPromise } from "axios";
import { doDelete, doGet } from "../../common/util/baseAPI";

export const doGetCategories = (PageIndex: number): AxiosPromise<any> => {
  return doGet("api/category/", { PageIndex });
};

export const doDeleteCategory = (id: number): AxiosPromise<any> => {
    return doDelete(`api/category/${id}/`);
  };