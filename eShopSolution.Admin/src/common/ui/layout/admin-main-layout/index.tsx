import React, { FC, ReactNode } from "react";
import { Container } from "react-bootstrap";
import { PageName } from "../../../../models/enum";
import Header from "./header";
import Footer from "./footer";

interface Props {
  children: ReactNode;
  active: PageName | string;
}

const AdminMainLayout: FC<Props> = (props: Props) => {
  const { children, active } = props;
  return (
    <main className="position-relative">
      <Header active={active} />
      <Container>{children}</Container>
      <Footer />
    </main>
  );
};

export default AdminMainLayout;
