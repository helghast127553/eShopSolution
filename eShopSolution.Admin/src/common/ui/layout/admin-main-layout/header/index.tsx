import { FC } from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import style from "./header.module.scss";
import { PageName } from "../../../../../models/enum";
import PageShortcut from "../content/PageShortcut";

interface Props {
  active: PageName | string;
}

const Header: FC<Props> = (props: Props) => {
  const { active } = props;

  return (
    <Navbar className={style.header} expand="lg">
      <Container>
        <Navbar.Toggle aria-controls="collapse-navbar" />
        <Navbar.Collapse id="collapse-navbar" className="justify-content-start">
          <Nav className={style.content}>
            <PageShortcut active={active} />
          </Nav>
        </Navbar.Collapse>
        
      </Container>
    </Navbar>
  );
};

export default Header;
