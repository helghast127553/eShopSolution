import React, { FC, useState, useEffect, BaseSyntheticEvent } from "react";
import { Form } from "react-bootstrap";
import { SubmitErrorHandler, SubmitHandler, useForm } from "react-hook-form";
import { CButton, CInput, CModal, CTextArea } from "../../common/ui/base";
import { CategoryFormInputs } from "../../models/form";
import { FormAction } from "../../models/enum";
import style from "./category.module.scss";
import { AxiosError } from "axios";
import { CategoryData, ProductData } from "../../models";

interface Props {
  initialData?: ProductData;
  action: FormAction;
  isOpen: boolean;
  toggle: () => void;
  onAddSucess: () => void;
}

const ProductWriter: FC<Props> = (props: Props) => {
  const { isOpen, toggle, onAddSucess, action, initialData } = props;
  const { register, handleSubmit, reset } =
    useForm<CategoryFormInputs>();
  const [isSubmit, setSubmit] = useState<boolean>(false);

  useEffect(() => {
    if (initialData !== undefined) {
      reset({ name: initialData.name, description: initialData.description})
    }
  }, [initialData])

  const onAddValid: SubmitHandler<any> = (data, event) => {
    
  };

  const onAddInvalid: SubmitErrorHandler<CategoryFormInputs> = (
    _,
    event
  ) => {
    event?.target.classList.add("wasvalidated");
  };

  const cancel = (): void => {
    toggle();
  };

  return (
    <CModal
      size="sm"
      isOpened={isOpen}
      toggle={cancel}
      modalHeader={
        <h3>
          {action === FormAction.CREATE
            ? "Thêm loại sản phẩm"
            : "Chỉnh sửa loại sản phẩm"}
        </h3>
      }
    >
      <Form
        className={style.loginForm}
        noValidate
        onSubmit={handleSubmit(onAddValid, onAddInvalid)}
      >
        <Form.Group className={style.inputGroup}>
          <Form.Label className="required">Tên loại sản phẩm</Form.Label>
          <CInput
            name="name"
            type="text"
            placeholder="Nhập tên loại sản phẩm"
            iref={register({})}
          />
        </Form.Group>
        <Form.Group className={style.inputGroup}>
          <Form.Label className="required">Mô tả</Form.Label>
          <CTextArea
            name="description"
            placeholder="Nhập mô tả"
            iref={register({})}
          />
        </Form.Group>
        <Form.Group className={style.inputGroup}>
          <Form.Label className="required">Mô tả</Form.Label>
          <CInput
            type="text"
            name="price"
            placeholder="Nhập giá cả"
            iref={register({})}
          />
        </Form.Group>
        <div className={`d-flex justify-content-end ${style.buttonGroup}`}>
          <CButton type="button" outline onClick={cancel}>
            Hủy
          </CButton>
          <CButton type="submit" className="ms-2">
            Lưu
          </CButton>
        </div>
      </Form>
    </CModal>
  );
};

export default ProductWriter;
