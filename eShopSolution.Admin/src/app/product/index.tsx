import { FC, useState, MouseEvent, useEffect } from "react";
import { Image } from "react-bootstrap";
import { CButton, CTable, CTPaging, CTRow } from "../../common/ui/base";
import { APIResponse, ProductData } from "../../models";
import { PageName } from "../../models/enum";
import { doDeleteProduct, doGetProducts } from "./api";
import Trash from "../../common/ui/assets/ic/trash-bin.svg";
import ZoomImage from "../../common/ui/base/zoom-image";
import AdminContentLayout from "../../common/ui/layout/admin-content-layout";

interface Props {}

const Product: FC<Props> = (props: Props) => {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>(1);
  const [products, setProducts] = useState<Array<ProductData>>([]);
  const TABLE_HEADER = [
    "NO.",
    "Tên sản phẩm",
    "Loại sản phẩm",
    // "Mô tả",
    "Gía sản phẩm",
    "Hình ảnh sản phẩm",
    "Time Created",
    "Time Updated",
    "Tác vụ"
  ];

  useEffect(() => {
    products.length === 0 && getProducts(1);
    // eslint-disable-next-line
  }, []);

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
      <CTable>
        <thead>
          <CTRow header data={TABLE_HEADER} />
        </thead>
        <tbody>
          {products.map((item, index) => (
            <CTRow
              key={index}
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
    </AdminContentLayout>
  );
};

export default Product;
