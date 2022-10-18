import { AxiosPromise } from "axios";
import { doDelete, doGet } from "../../common/util/baseAPI";

export const doGetProducts = (PageIndex: number): AxiosPromise<any> => {
  return doGet("api/manage/product/", { PageIndex });
};

export const doDeleteProduct = (id: number): AxiosPromise<any> => {
    return doDelete(`api/product/${id}/`);
  };