import { FC, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Spinner } from "react-bootstrap";
import { LoginFormInputs } from "../../models/form";
import { ScopeKey, ScopeValue, Roles, PageURL } from "../../models/enum";
import { SubmitErrorHandler, SubmitHandler, useForm } from "react-hook-form";
import style from "./login.module.scss";
import CInputHint from "../../common/ui/base/input/CInputHint";
import { doLogin, getProfile } from "./api";
import { AxiosError } from "axios";

interface Props {}

const Login: FC<Props> = (props: Props) => {
  const navigate = useNavigate();
  const { register, handleSubmit, errors } = useForm<LoginFormInputs>();
  const [error, setError] = useState<string>();
  const [onLoad, setOnLoad] = useState<boolean>(false);

  const onLoginValid: SubmitHandler<LoginFormInputs> = (data, event) => {
    setOnLoad(true);
    doLogin(data)
      .then((response: any) => {
        setOnLoad(false);
        if (response.isSuccessed) {
          sessionStorage.setItem(
            ScopeKey.ACCESS_TOKEN,
            window.btoa(response.resultObj)
          );
        }
        localStorage.setItem(ScopeKey.IS_AUTHENTICATED, ScopeValue.TRUE);

        getProfile()
          .then((res) => {
            const roles = res.data.roles;
            if (roles.includes(Roles.ADMIN)) {
              localStorage.setItem(ScopeKey.IS_ADMIN, ScopeValue.TRUE);
              navigate(PageURL.ADMIN_PRODUCT);
            }
          })
          .catch((error: AxiosError) => console.log(error));
      })
      .catch((error) => {
        if(error.response.data) {
          setError(error.response.data.message);
        }
      });
  };

  const onLoginInvalid: SubmitErrorHandler<LoginFormInputs> = (_, event) => {};

  return (
    <div className={style.templateLogin}>
      <div className={style.login}>
        <h1 className={style.loginHeading}>Đăng nhập</h1>
        <div className={style.error}>
          {error}
        </div>
        <Form
          className={style.loginForm}
          noValidate
          onSubmit={handleSubmit(onLoginValid, onLoginInvalid)}
        >
          <Form.Group>
            <Form.Label
              className={`${style.loginLabel} required`}
              htmlFor="username"
            >
              Tên đăng nhập
            </Form.Label>
            <input
              autoComplete="off"
              type="text"
              name="username"
              className={`${style.loginInput}`}
              placeholder="Eg: helghast127553"
              ref={register({ required: "Trường này là bắt buộc" })}
            />
            {errors.username && <CInputHint>{errors.username.message}</CInputHint>}
          </Form.Group>
          <Form.Group>
            <Form.Label
              className={`${style.loginLabel} required`}
              htmlFor="password"
            >
              Mật khẩu
            </Form.Label>
            <input
              autoComplete="off"
              type="password"
              name="password"
              className={style.loginInput}
              ref={register({ required: "Trường này là bắt buộc" })}
            />
            {errors.password && <CInputHint>{errors.password.message}</CInputHint>}
          </Form.Group>
          <div className={style.btnGroups}>
            <button className={style.signUpSubmit}>
              {onLoad ? (
                <Spinner size="sm" variant="light" animation="border" />
              ) : (
                <span>Đăng nhập</span>
              )}
            </button>
          </div>
        </Form>
      </div>
    </div>
  );
};

export default Login;
