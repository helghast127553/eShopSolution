import { AxiosPromise } from "axios";
import { doDelete, doGet, doPost, doPut } from "../../common/util/baseAPI";

export const doGetProducts = (PageIndex: number): AxiosPromise<any> => {
  return doGet("api/manage/product/", { PageIndex });
};

export const doPostProduct = (data: FormData): AxiosPromise<any> => {
  return doPost("api/product/", data);
};

export const doPutProduct = (id: number, data: FormData): AxiosPromise<any> => {
  return doPut(`api/product/${id}`, data);
};

export const doDeleteProduct = (id: number): AxiosPromise<any> => {
    return doDelete(`api/product/${id}/`);
};