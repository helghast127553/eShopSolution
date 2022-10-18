import React, { FC, RefObject, TextareaHTMLAttributes } from "react";
import style from "./input.module.scss";

interface Props extends TextareaHTMLAttributes<HTMLTextAreaElement> {
  iref?:
    | string
    | ((instance: HTMLTextAreaElement | null) => void)
    | RefObject<HTMLTextAreaElement>;
  valid?: boolean;
}

const CTextArea: FC<Props> = (props: Props) => {
  const { valid = true } = props;
  return (
    <>
      <textarea
        ref={props.iref}
        {...Object.assign({}, props, { iref: undefined, valid: undefined })}
        className={
          `${props.disabled && style.disable} ` +
          `${valid ? "isvalid" : "isinvalid"} ` +
          `${style.inputContainer} ${props.className} cinput`
        }
      />
    </>
  );
};

export default CTextArea;
