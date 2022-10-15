import { AxiosPromise } from "axios";
import { doPost, doGet } from "../../common/util/baseAPI";
import { LoginFormInputs } from "../../models/form";

export const doLogin = (loginData: LoginFormInputs): AxiosPromise<any> => {
  return doPost("api/auth/token/", loginData);
};

export const getProfile = (): AxiosPromise<any> => {
  return doGet("api/auth/user-info/");
};
