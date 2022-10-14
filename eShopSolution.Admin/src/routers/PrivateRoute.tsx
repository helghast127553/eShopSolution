import { FC, ReactElement } from "react";
import Login from "../app/login";
import { ScopeKey, ScopeValue } from "../models/enum";

interface Props {
  children: ReactElement;
}

const PrivateRoute: FC<Props> = (props: Props) => {
  let isAuthenticated = localStorage.getItem(ScopeKey.IS_AUTHENTICATED);
  let isAdmin = localStorage.getItem(ScopeKey.IS_ADMIN);

  return (isAdmin as ScopeValue) === ScopeValue.TRUE &&
    (isAuthenticated as ScopeValue) === ScopeValue.TRUE ? (
    props.children
  ) : (
    <Login />
  );
};

export default PrivateRoute;
