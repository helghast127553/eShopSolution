import React, { FC, TableHTMLAttributes } from "react";
import style from "./table.module.scss";

interface Props extends TableHTMLAttributes<HTMLTableElement> {
  responsive?: boolean;
  maxHeight?: number;
}

const CTable: FC<Props> = (props: Props) => {
  const { responsive = false } = props;
  const customStyle = props.maxHeight ? {maxHeight: props.maxHeight} : {};

  return (
    <div className={`${style.ctable} ${responsive && style.responsive}`} style={Object.assign({}, props.style, customStyle )}>
      <table className={props.className}>{props.children}</table>
    </div>
  );
};

export default CTable;
