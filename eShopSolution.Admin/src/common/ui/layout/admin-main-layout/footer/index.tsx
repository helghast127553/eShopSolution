import { FC } from "react";
import { Col, Container, Row } from "react-bootstrap";
import style from "./footer.module.scss";

interface Props {}

const Footer: FC<Props> = (props: Props) => {
  return (
    <footer className={style.fContainer}>
      <Container fluid>
        <Row>
          <Col
            className={`${style.bottomRow} d-flex justify-content-center align-items-center`}
          >
            © TMA Solutions 2020 All Rights Reserved
            {process.env.REACT_APP_BUILD_NUMBER
              ? ` - BUILD:${process.env.REACT_APP_BUILD_NUMBER}`
              : null}
          </Col>
        </Row>
      </Container>
    </footer>
  );
};

export default Footer;
