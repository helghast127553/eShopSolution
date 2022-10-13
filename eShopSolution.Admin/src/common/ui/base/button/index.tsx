import React, {
    ButtonHTMLAttributes,
    FC,
    MouseEvent,
    useEffect,
    useState,
  } from "react";
  import style from "./btn.module.scss";
  import { ButtonSize } from "../../../../models/enum";
  
  interface Props extends ButtonHTMLAttributes<HTMLButtonElement> {
    outline?: boolean;
    size?: ButtonSize;
    borderless?: boolean;
    dropdown?: boolean;
    iref?: React.LegacyRef<HTMLButtonElement> | undefined;
  }
  
  const CButton: FC<Props> = (props: Props) => {
    const { outline = false, borderless = false, dropdown = false, size } = props;
  
    const [isShow, setShow] = useState<boolean>(false);
  
    const toggle = (e: MouseEvent<HTMLButtonElement>) => {
      e.stopPropagation();
      e.preventDefault();
      setShow(!isShow);
    };
  
    let realSize = ButtonSize.NORMAL;
    if (size) realSize = size;
  
    const SIZE_MAP = {
      [ButtonSize.NORMAL]: style.btn32,
      [ButtonSize.LARGE]: style.btn40,
      [ButtonSize.SMALL]: style.btn24,
    };
  
    useEffect(() => {
      if (dropdown) {
        document.addEventListener("click", () => setShow(false), false);
      }
      return () => {
        document.removeEventListener("click", () => setShow(false), false);
      };
      // eslint-disable-next-line
    }, []);
  
    if (dropdown) {
      return (
        <div className={`${style.dropdown} ${isShow && style.show}`}>
          <button
            ref={props.iref}
            {...Object.assign({}, props, {
              outline: undefined,
              borderless: undefined,
              dropdown: undefined,
            })}
            onClick={toggle}
            className={
              `${style.btn} ` +
              `${SIZE_MAP[realSize]} ` +
              `${props.className} ` +
              `${outline ? style.outlineBtn : style.defaultBtn} ` +
              `${props.disabled && style.opacity} ` +
              `${borderless && style.borderless}`
            }
          >
            {props.children}
          </button>
        </div>
      );
    } else {
      return (
        <button
          ref={props.iref}
          {...Object.assign({}, props, {
            outline: undefined,
            borderless: undefined,
          })}
          className={
            `${style.btn} ` +
            `${SIZE_MAP[realSize]} ` +
            `${props.className} ` +
            `${outline ? style.outlineBtn : style.defaultBtn} ` +
            `${props.disabled && style.opacity} ` +
            `${borderless && style.borderless}`
          }
        >
          {props.children}
        </button>
      );
    }
  };
  
  export default CButton;
  