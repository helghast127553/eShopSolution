import React, { FC, HTMLAttributes } from "react";
import { Image } from "react-bootstrap";
import style from "./paging.module.scss";
import Prev from "../../../assets/ic/enable.svg";

interface Props extends HTMLAttributes<HTMLElement> {
  active: boolean;
  prev?: boolean;
  next?: boolean;
}

const PagingItem: FC<Props> = (props: Props) => {
  const { prev = false, next = false, active = false } = props;

  return (
    <div
      className={
        `${style.item} ` +
        `${!prev && !next && style.number} ` +
        `${active && style.active} ` +
        `${(prev || next) && style.prevNext}`
      }
      onClick={(prev || next) && !active ? undefined : props.onClick}
    >
      {prev && <Image className={`${style.prev} left90`} src={Prev} />}
      {!prev && !next && props.children}
      {next && <Image className={`${style.next} right90`} src={Prev} />}
    </div>
  );
};

export default PagingItem;
