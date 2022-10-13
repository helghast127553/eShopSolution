import React, { FC, HTMLAttributes, ReactNode, useEffect } from "react";
import style from "./row.module.scss";

interface Props extends HTMLAttributes<HTMLElement> {
  header?: boolean;
  data: Array<ReactNode>;
  eachColClass?: string;
  eachColHeaderClass?: string;
  colPositon?: number;
  onClick?: () => void;
  elementId?: string;
  handleMouseDown?: () => void;
  text?: string;
}

const CTRow: FC<Props> = (props: Props) => {
  const {
    header,
    data,
    className,
    eachColClass,
    eachColHeaderClass,
    colPositon,
    elementId = "tr",
    text,
  } = props;

  useEffect(() => {
    const element: any = document.getElementById(elementId);

    const listElement: any = document.getElementById(`list-${elementId}`);

    listElement.addEventListener("contextmenu", (event: any) => {
      event.stopPropagation();
    });

    document.addEventListener("contextmenu", (event) => {
      listElement.style.display = "none";
    });

    document.addEventListener("mousedown", (event: any) => {
      event.stopPropagation();
    });

    const onMouseDown = (event: any) => {
      listElement.style.display = "none";
      element.removeEventListener("mousedown", onMouseDown);
    };

    element.addEventListener("contextmenu", (event: any) => {
      event.preventDefault();
      event.stopPropagation();
      document.addEventListener("mousedown", onMouseDown);
      const rect = element.getBoundingClientRect();
      const x = event.clientX - rect.left;
      // const y = event.clientY - rect.top;
      listElement.style.display = "block";
      listElement.style.marginTop = "5px";
      listElement.style.left = x + "px";
    });
    // eslint-disable-next-line
  }, []);

  const colums = header
    ? data.map((colum, index) => (
        <th
          className={`${index === colPositon && eachColHeaderClass}`}
          key={index}
        >
          {colum}
        </th>
      ))
    : data.map((colum, index) => (
        <td
          className={
            colum !== "isHidden"
              ? `${index === colPositon && eachColClass}`
              : "d-none"
          }
          key={index}
        >
          {colum}
        </td>
      ));

  return (
    <tr
      onClick={props.onClick}
      className={`${className} ${style.row} ${props.onClick && style.rowItem} ${
        style.box
      }`}
      id={elementId}
    >
      {colums}
      <ul className={style.list} id={`list-${elementId}`}>
        <li
          onMouseDown={() => props.handleMouseDown && props.handleMouseDown()}
        >
          {text}
        </li>
      </ul>
    </tr>
  );
};

export default CTRow;
