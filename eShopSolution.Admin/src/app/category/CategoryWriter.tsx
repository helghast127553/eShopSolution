import React, { FC, useState, useEffect } from "react";
import { Form } from "react-bootstrap";
import { SubmitErrorHandler, SubmitHandler, useForm } from "react-hook-form";
import {
  CButton,
  CInput,
  CModal,
  CTextArea,
  CSelect,
} from "../../common/ui/base";
import { CategoryFormInputs } from "../../models/form";
import { FormAction } from "../../models/enum";
import { CategoryData } from "../../models";
import { doGetParentCategories, doPostCategory, doPutCategory } from "./api";
import style from "./category.module.scss";
import CInputHint from "../../common/ui/base/input/CInputHint";

interface Props {
  initialData?: CategoryData;
  action: FormAction;
  isOpen: boolean;
  toggle: () => void;
  onAddSucess: () => void;
}

const CategoryWriter: FC<Props> = (props: Props) => {
  const { isOpen, toggle, onAddSucess, action, initialData } = props;
  const [parentCategories, setParentCategories] = useState<Array<CategoryData>>(
    []
  );
  const { register, handleSubmit, reset, errors } = useForm<CategoryFormInputs>();

  useEffect(() => {
    if (FormAction.UPDATE === action) {
      if (initialData !== undefined) {
        reset({
          name: initialData.name,
          description: initialData.description,
          parentId: initialData.parentId,
        });
      }
    } else {
      reset({ name: undefined, description: undefined, parentId: undefined });
    }
    // eslint-disable-next-line
  }, [initialData, action]);

  useEffect(() => {
    doGetParentCategories()
      .then((response: any) => {
        setParentCategories(response);
      })
      .catch((error) => console.log(error));
  }, []);

  const onAddValid: SubmitHandler<CategoryFormInputs> = (data, event) => {
    if (FormAction.CREATE === action) {
      doPostCategory(data)
        .then((response) => {
          onAddSucess();
          toggle();
        })
        .catch((error) => console.log(error));
    } else {
      if (initialData) {
        doPutCategory(initialData.id, data)
          .then((response) => {
            onAddSucess();
            toggle();
          })
          .catch((error) => console.log(error));
      }
    }
  };

  const onAddInvalid: SubmitErrorHandler<CategoryFormInputs> = (_, event) => {
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
            iref={register({ required: "Trường này là bắt buộc" })}
            valid={!errors.name}
          />
          {errors.name && <CInputHint>{errors.name.message}</CInputHint>}
        </Form.Group>
        <Form.Group className={style.inputGroup}>
          <Form.Label className="required">Mô tả</Form.Label>
          <CTextArea
            name="description"
            placeholder="Nhập mô tả"
            iref={register({ required: "Trường này là bắt buộc" })}
            valid={!errors.description}
          />
          {errors.description && <CInputHint>{errors.description.message}</CInputHint>}
        </Form.Group>
        <Form.Group>
          <Form.Label>Loại sản phẩm cha</Form.Label>
          <CSelect
            iref={register({ required: "Trường này là bắt buộc" })}
            name="parentId"
            placeholder="Chọn loại sản phẩm cha"
            valid={!errors.parentId}
          >
            {parentCategories.map((item) => (
              <option title={item.name} value={item.id}>
                {item.name}
              </option>
            ))}
          </CSelect>
          {errors.parentId && <CInputHint>{errors.parentId.message}</CInputHint>}
        </Form.Group>
        <div className={`d-flex justify-content-end ${style.buttonGroup}`}>
          <CButton type="button" outline onClick={cancel}>
            Hủy
          </CButton>
          <CButton type="submit" className="ml-3">
            Lưu
          </CButton>
        </div>
      </Form>
    </CModal>
  );
};

export default CategoryWriter;
