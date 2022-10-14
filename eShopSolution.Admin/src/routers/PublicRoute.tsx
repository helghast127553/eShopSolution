import { FC, ReactElement } from "react";

interface Props {
  children: ReactElement
}

const PublicRoute: FC<Props> = (props: Props) => {
  return props.children;
};

export default PublicRoute;
