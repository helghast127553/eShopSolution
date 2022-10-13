import React, { FC, useEffect, useState } from "react";
import { Nav, NavDropdown } from "react-bootstrap";
import { Link } from "react-router-dom";
import { Shortcut } from "../../../../../models";
import { PageName, PageURL } from "../../../../../models/enum";
import style from "../header/header.module.scss";

interface Props {
  active: PageName | string;
}

const PageShortcut: FC<Props> = (props: Props) => {
  const { active } = props;

  const [pageShortcuts, setPageShortcuts] = useState<Array<Shortcut>>([
    {
      name: PageName.Product,
      url: PageURL.ADMIN_PRODUCT,
      isActive: false,
    },
    {
      name: PageName.Category,
      url: PageURL.ADMIN_CATEGORY,
      isActive: false,
    },
    {
      name: PageName.User,
      url: PageURL.ADMIN_USER,
      isActive: false,
    }
  ]);

  const setActive = (name: PageName | string): void => {
    const pshortcuts = pageShortcuts.map((ps) => {
      ps.isActive = ps.name === name;
      return ps;
    });
    setPageShortcuts(pshortcuts);
  };

  // eslint-disable-next-line
  useEffect(() => setActive(active), []);

  return (
    <>
      {pageShortcuts.map((shortcut, index) =>
        !shortcut.subName ? (
          <Nav.Item key={index}>
            <Link
              to={shortcut.url}
              className={
                shortcut.isActive
                  ? `${style.shortcut} ${style.active}`
                  : style.shortcut
              }
            >
              {shortcut.name}
            </Link>
          </Nav.Item>
        ) : (
          <Nav.Item key={index}>
            <NavDropdown
              title={shortcut.name}
              id={shortcut.isActive ? "nav-item-active" : "nav-item"}
              className={
                shortcut.isActive
                  ? `${style.shortcut} ${style.active}`
                  : style.shortcut
              }
            >
              {shortcut.subName.map((sub, index) => (
                <NavDropdown.Item key={index} className={style.dropdownItem}>
                  <Link to={sub.url} className={style.subItem}>
                    {sub.name}
                  </Link>
                </NavDropdown.Item>
              ))}
            </NavDropdown>
          </Nav.Item>
        )
      )}
    </>
  );
};

export default PageShortcut;
