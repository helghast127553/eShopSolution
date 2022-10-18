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
            2020 All Rights Reserved
          </Col>
        </Row>
      </Container>
    </footer>
  );
};

export default Footer;
