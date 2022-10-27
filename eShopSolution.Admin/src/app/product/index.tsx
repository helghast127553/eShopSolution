import { FC, useState, MouseEvent, useEffect } from "react";
import { Image } from "react-bootstrap";
import { CButton, CTable, CTPaging, CTRow } from "../../common/ui/base";
import { APIResponse, ProductData } from "../../models";
import { ButtonSize, FormAction, PageName } from "../../models/enum";
import { doDeleteProduct, doGetProducts } from "./api";
import Plus from "../../common/ui/assets/ic/plus.svg";
import Trash from "../../common/ui/assets/ic/trash-bin.svg";
import ZoomImage from "../../common/ui/base/zoom-image";
import AdminContentLayout from "../../common/ui/layout/admin-content-layout";
import ProductWriter from "./ProductWriter";

interface Props {}

const Product: FC<Props> = (props: Props) => {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>(1);
  const [products, setProducts] = useState<Array<ProductData>>([]);
  const [activeItem, setActiveItem] = useState<ProductData>();
  const [action, setAction] = useState<FormAction>(FormAction.CREATE);
  const [isOpened, setIsOpened] = useState<boolean>(false);
  const TABLE_HEADER = [
    "NO.",
    "Tên sản phẩm",
    "Loại sản phẩm",
    // "Mô tả",
    "Gía sản phẩm",
    "Hình ảnh",
    "Time Created",
    "Time Updated",
    "Tác vụ",
  ];

  useEffect(() => {
    products.length === 0 && getProducts(1);
    // eslint-disable-next-line
  }, []);

  const toggle = (): void => setIsOpened(!isOpened);

  const onCreateProduct = (): void => {
    setAction(FormAction.CREATE);
    setActiveItem(undefined);
    toggle();
  };

  const onUpdateProduct = (data: ProductData): void => {
    setAction(FormAction.UPDATE);
    setActiveItem(data);
    toggle();
  };

  const getProducts = (PageIndex: number): void => {
    doGetProducts(PageIndex)
      .then((response) => {
        const data: APIResponse<ProductData> = response.data;
        setProducts(data.resultObj.items);
        setCurrentPage(data.resultObj.pageIndex);
        setTotalPage(
          Math.ceil(data.resultObj.totalRecords / data.resultObj.pageSize)
        );
      })
      .catch((error) => console.log(error));
  };

  const onDelete = (e: MouseEvent<HTMLButtonElement>, id: number) => {
    e.preventDefault();
    e.stopPropagation();

    doDeleteProduct(id)
      .then((response) => {
        getProducts(currentPage);
      })
      .catch((error) => console.log(error));
  };

  return (
    <AdminContentLayout title="Product" activate={PageName.Product}>
      <div className="d-flex align-items-center justify-content-end">
        <CButton size={ButtonSize.NORMAL} onClick={onCreateProduct}>
          <Image src={Plus} className="bicon" />
          Thêm sản phẩm
        </CButton>
      </div>
      <div className="mt-4">
        <CTable>
          <thead>
            <CTRow header data={TABLE_HEADER} />
          </thead>
          <tbody>
            {products.map((item, index) => (
              <CTRow
                key={index}
                onClick={() => onUpdateProduct(item)}
                data={[
                  index + 1,
                  item.name,
                  item.categoryName,
                  // item.description,
                  item.price,
                  item.imageUrl && (
                    <ZoomImage src={item.imageUrl} className="circle-avatar" />
                  ),
                  item.time_Created,
                  item.time_Updated,
                  <>
                    <CButton
                      borderless
                      onClick={(event) => onDelete(event, item.id)}
                    >
                      <Image src={Trash} />
                    </CButton>
                  </>,
                ]}
              />
            ))}
          </tbody>
        </CTable>
      </div>
      {products.length > 0 && (
        <div className="d-flex justify-content-end">
          <CTPaging
            className="mt-4"
            currentPage={currentPage}
            totalPage={totalPage}
            onGetData={getProducts}
          />
        </div>
      )}
       <ProductWriter
        action={action}
        initialData={activeItem}
        isOpen={isOpened}
        toggle={toggle}
        onAddSucess={() => getProducts(currentPage)}
      />
    </AdminContentLayout>
  );
};

export default Product;
