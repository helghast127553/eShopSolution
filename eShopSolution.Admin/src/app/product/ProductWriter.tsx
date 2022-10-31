import { FC, useState, useEffect } from "react";
import { Form } from "react-bootstrap";
import { SubmitErrorHandler, SubmitHandler, useForm } from "react-hook-form";
import {
  CButton,
  CInput,
  CModal,
  CSelect,
  CTextArea,
} from "../../common/ui/base";
import { ProductFormInputs } from "../../models/form";
import { FormAction } from "../../models/enum";
import style from "./product.module.scss";
import { CategoryData, ProductData } from "../../models";
import { doGetSubCategories } from "../category/api";
import { doPostProduct, doPutProduct } from "./api";
import CInputHint from "../../common/ui/base/input/CInputHint";

interface Props {
  initialData?: ProductData;
  action: FormAction;
  isOpen: boolean;
  toggle: () => void;
  onAddSucess: () => void;
}

const ProductWriter: FC<Props> = (props: Props) => {
  const { isOpen, toggle, onAddSucess, action, initialData } = props;
  const { register, handleSubmit, reset, errors } = useForm<ProductFormInputs>();
  const [subCategories, setSubCategories] = useState<Array<CategoryData>>([]);

  useEffect(() => {
    if (FormAction.UPDATE === action) {
      if (initialData !== undefined) {
        reset({
          name: initialData.name,
          description: initialData.description,
          price: initialData.price,
          categoryId: initialData.categoryId,
        });
      }
    } else {
      reset({
        name: undefined,
        description: undefined,
        price: undefined,
        categoryId: undefined,
      });
    }
  }, [initialData, action]);

  useEffect(() => {
    doGetSubCategories()
      .then((response: any) => {
        setSubCategories(response);
      })
      .catch((error) => console.log(error));
  }, []);

  const onAddValid: SubmitHandler<any | FormData> = (data, event) => {
    const dataForm = new FormData();
    dataForm.append("thumbnailImage", data.thumbnailImage[0]);
    dataForm.append("name", data.name);
    dataForm.append("description", data.description);
    dataForm.append("price", data.price);
    dataForm.append("categoryId", data.categoryId);
    if (FormAction.CREATE === action) {
      doPostProduct(dataForm)
      .then((response) => {
        onAddSucess();
        toggle();
      })
      .catch((error) => console.log(error)); 
    } 
    else {
      if (initialData) {
        doPutProduct(initialData.id, dataForm)
        .then((response) => {
          onAddSucess();
          toggle();
        })
        .catch((error) => console.log(error));  
      }
    }
  };

  const onAddInvalid: SubmitErrorHandler<any | FormData> = (_, event) => {
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
          <Form.Label className="required">Hình ảnh</Form.Label>
          <div className="custom-file">
            <input
              type="file"
              name="thumbnailImage"
              className="custom-file-input"
              accept="image/*"
              id="customFile"
              ref={register({})}
            />
            <label className="custom-file-label text-left" htmlFor="customFile">
              Chọn file hình ảnh
            </label>
          </div>
        </Form.Group>
        <Form.Group className={style.inputGroup}>
          <Form.Label className="required">Tên sản phẩm</Form.Label>
          <CInput
            name="name"
            type="text"
            placeholder="Nhập tên sản phẩm"
            iref={register({ required: "Trường này là bắt buộc" })}
            valid={!errors.name}
          />
           {errors.name && <CInputHint>{errors.name.message}</CInputHint>}
        </Form.Group>
        <Form.Group className={style.inputGroup}>
          <Form.Label className="required">Giá sản phẩm</Form.Label>
          <CInput
            name="price"
            type="text"
            placeholder="Nhập giá sản phẩm"
            iref={register({ required: "Trường này là bắt buộc" })}
            valid={!errors.price}
          />
           {errors.price && <CInputHint>{errors.price.message}</CInputHint>}
          <Form.Group>
            <Form.Label>Loại sản phẩm</Form.Label>
            <CSelect
              iref={register({ required: "Trường này là bắt buộc" })}
              name="categoryId"
              placeholder="Chọn loại sản phẩm"
              valid={!errors.categoryId}
            >
              {subCategories.map((item) => (
                <option title={item.name} value={item.id}>
                  {item.name}
                </option>
              ))}
            </CSelect>
            {errors.categoryId && <CInputHint>{errors.categoryId.message}</CInputHint>}
          </Form.Group>
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

export default ProductWriter;
