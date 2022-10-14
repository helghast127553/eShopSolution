import axios, { AxiosError, AxiosPromise } from "axios";
import { ScopeKey, ScopeValue } from "../../models/enum";

let baseURL = process.env.REACT_APP_API_URL;
if (!baseURL) baseURL = window.location.origin;

axios.defaults.baseURL = baseURL;
axios.defaults.headers.common["Content-Type"] = "application/json";

axios.interceptors.request.use(
  (config) => {
    const accessToken = window.atob(
      sessionStorage.getItem(ScopeKey.ACCESS_TOKEN) || ""
    );

    if (accessToken.length > 0) {
      if (config.headers !== undefined) {
        config.headers["Authorization"] = `Bearer ${accessToken}`;
      }
    }
    return config;
  },
  (error) => Promise.reject(error)
);

axios.interceptors.response.use(
  (response) => {
    if (response && response.data) {
      return response.data;
    }

    return response;
  },
  (error: AxiosError) => {
    if (error.response?.status === 401) {
      handleUnauthorize();
    } else {
      return Promise.reject(error);
    }
  }
);

export const doGet = (url: string, params?: Object): AxiosPromise<any> => {
  return axios({
    method: "GET",
    url: url,
    params: params,
  });
};

export const doGetWithToken = (
  url: string,
  accessToken?: string,
  params?: Object
): AxiosPromise<any> => {
  return axios({
    method: "GET",
    url: url,
    params: params,
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
};

export const doPost = (
  url: string,
  data?: object | FormData
): AxiosPromise<any> => {
  return axios({
    method: "POST",
    url: url,
    data: data,
  });
};

export const doPostWithToken = (
  url: string,
  data: object | FormData,
  accessToken?: string
): AxiosPromise<any> => {
  return axios({
    method: "POST",
    url: url,
    data: data,
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
};

export const doPut = (
  url: string,
  data: object | FormData
): AxiosPromise<any> => {
  return axios({
    method: "PUT",
    url: url,
    data: data,
  });
};

export const doPatch = (
  url: string,
  data: object | FormData
): AxiosPromise<any> => {
  return axios({
    method: "PATCH",
    url: url,
    data: data,
  });
};

export const doDelete = (url: string): AxiosPromise<any> => {
  return axios({
    method: "DELETE",
    url: url,
  });
};

const handleUnauthorize = () => {
  localStorage.setItem(ScopeKey.IS_AUTHENTICATED, ScopeValue.FALSE);
  localStorage.setItem(ScopeKey.IS_ADMIN, ScopeValue.FALSE);
  window.location.reload();
};
