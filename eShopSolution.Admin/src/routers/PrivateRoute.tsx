import React, { FC, ReactElement, ReactNode } from "react";
import Login from "../app/login";
import { ScopeKey, ScopeValue } from "../models/enum";

interface Props {
  children: ReactElement;
}

const PrivateRoute: FC<Props> = (props: Props) => {
  let isAuthenticated = localStorage.getItem(ScopeKey.IS_AUTHENTICATED);

  return (isAuthenticated as ScopeValue) === ScopeValue.TRUE ? (
    props.children
  ) : (
    <Login/>
  );

  // return props.children;
};

export default PrivateRoute;
