import React, { FC, ImgHTMLAttributes, useState } from "react";
import { Modal, Image } from "react-bootstrap";

interface Props extends ImgHTMLAttributes<HTMLImageElement> {}

const ZoomImage: FC<Props> = (props: Props) => {
  const [isOpen, setOpen] = useState<boolean>(false);
  const toggle = () => setOpen(!isOpen);

  return (
    <>
      <Image src={props.src} onClick={toggle} {...props} />
      <Modal size="lg" centered show={isOpen} onHide={toggle}>
        <Modal.Body className="p-0">
          <Image src={props.src} style={{ width: "100%" }} />
        </Modal.Body>
      </Modal>
    </>
  );
};

export default ZoomImage;
