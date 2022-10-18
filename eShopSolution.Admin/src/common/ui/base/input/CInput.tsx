import {
  FC,
  InputHTMLAttributes,
  RefObject,
} from "react";
import style from "./input.module.scss";

interface Props extends InputHTMLAttributes<HTMLInputElement> {
  iref?:
    | string
    | ((instance: HTMLInputElement | null) => void)
    | RefObject<HTMLInputElement>;
  valid?: boolean;
}

const CInput: FC<Props> = (props: Props) => {
  const { valid = true } = props;

  return (
    <>
      <input
        ref={props.iref}
        {...Object.assign({}, props, { iref: undefined, valid: undefined })}
        className={
          `${props.disabled && style.disable} ` +
          `${valid ? "isvalid" : "isinvalid"} ` +
          `${style.inputContainer} ${props.className} cinput`
        }
        type={props.type}
      />
    </>
  );
};

export default CInput;
