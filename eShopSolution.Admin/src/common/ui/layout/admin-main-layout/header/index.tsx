import { FC } from "react";
import { useNavigate } from "react-router-dom";
import { IconContext } from "react-icons";
import { IoMdLogOut } from "react-icons/io";
import { Container, Nav, Navbar } from "react-bootstrap";
import style from "./header.module.scss";
import { PageName, PageURL, ScopeKey, ScopeValue } from "../../../../../models/enum";
import PageShortcut from "../content/PageShortcut";

interface Props {
  active: PageName | string;
}

const Header: FC<Props> = (props: Props) => {
  const { active } = props;
  const navigate = useNavigate();

  const logout = (): void => {
    sessionStorage.setItem(ScopeKey.ACCESS_TOKEN, window.btoa(""));
    localStorage.setItem(ScopeKey.IS_ADMIN, ScopeValue.FALSE);
    navigate(PageURL.ADMIN_LOGIN);
  };

  return (
    <Navbar className={style.header} expand="lg">
      <Container>
        <Navbar.Toggle aria-controls="collapse-navbar" />
        <Navbar.Collapse id="collapse-navbar" className="justify-content-start">
          <Nav className={style.content}>
            <PageShortcut active={active} />
          </Nav>
        </Navbar.Collapse>
        <Navbar.Collapse id="collapse-navbar" className="justify-content-end">
          <Nav className={style.content}>
            <IconContext.Provider
              value={{ color: "#959595", size: "20px"}}
            >
              <IoMdLogOut style={{cursor: "pointer" }} onClick={() => logout()} />
            </IconContext.Provider>
          
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Header;
