import { AxiosPromise } from "axios";
import { doGet } from "../../common/util/baseAPI";

export const doGetUsers = (PageIndex: number): AxiosPromise<any> => {
  return doGet("api/auth/users/", { PageIndex });
};
