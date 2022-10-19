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
  const { register, handleSubmit, reset } = useForm<CategoryFormInputs>();

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
      .then((response) => {
        setParentCategories(response.data);
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
        <Form.Group>
          <Form.Label>Loại sản phẩm cha</Form.Label>
          <CSelect
            iref={register({})}
            name="parentId"
            placeholder="Chọn loại sản phẩm cha"
          >
            {parentCategories.map((item) => (
              <option title={item.name} value={item.id}>
                {item.name}
              </option>
            ))}
          </CSelect>
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
