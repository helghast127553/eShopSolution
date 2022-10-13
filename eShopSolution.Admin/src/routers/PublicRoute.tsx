import React, { FC, ReactElement, ReactNode } from "react";
import { Route } from "react-router-dom";
import { PageURL } from "../models/enum";

interface Props {
  children: ReactElement
}

const PublicRoute: FC<Props> = (props: Props) => {
  return props.children;
};

export default PublicRoute;
