import React, { FC, HTMLAttributes, ReactNode } from "react";
import { Col, Container, Image, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { CButton } from "../../base";
import AdminMainLayout from "../admin-main-layout";
import { PageName } from "../../../../models/enum";
import style from "./adminContentLayout.module.scss";
import Enable from "../../assets/ic/enable.svg";

interface Props extends HTMLAttributes<HTMLElement> {
  btnGroup?: ReactNode;
  dropDefaultContent?: boolean;
  backTo?: string;
  activate: PageName | string;
  title?: string;
  backgroundClass?: string;
}

const AdminContentLayout: FC<Props> = (props: Props) => {
  const navigate = useNavigate();
  const {
    dropDefaultContent = false,
    backTo,
    activate,
    title = "Product",
  } = props;

  return (
    <AdminMainLayout active={activate}>
      <Container className={` ${style.contentLayout} ${props.className}`}>
        <Row className={style.header}>
          <Col className={style.title}>
            {backTo && (
              <CButton
                borderless
                onClick={() => navigate(backTo)}
                className="mr-2"
              >
                <Image src={Enable} className="left90" />
              </CButton>
            )}
            <h1>{title}</h1>
          </Col>
          <Col className={style.btn}>{props.btnGroup}</Col>
        </Row>
        <Row>
          <Col>
            {dropDefaultContent ? (
              <div className={style.padd}>{props.children}</div>
            ) : (
              <div className={`${style.content} ${props.backgroundClass}`}>
                {props.children}
              </div>
            )}
          </Col>
        </Row>
      </Container>
    </AdminMainLayout>
  );
};

export default AdminContentLayout;
