import { FC, useEffect, useState, MouseEvent } from "react";
import { CButton, CTable, CTPaging, CTRow } from "../../common/ui/base";
import { APIResponse, CategoryData } from "../../models";
import { ButtonSize, FormAction, PageName } from "../../models/enum";
import { doDeleteCategory, doGetSubCategoriesPaging } from "./api";
import { Image } from "react-bootstrap";
import Plus from "../../common/ui/assets/ic/plus.svg";
import AdminContentLayout from "../../common/ui/layout/admin-content-layout";
import Trash from "../../common/ui/assets/ic/trash-bin.svg";
import CategoryWriter from "./CategoryWriter";

interface Props {}

const Category: FC<Props> = (props: Props) => {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>(1);
  const [categories, setCategories] = useState<Array<CategoryData>>([]);
  const [activeItem, setActiveItem] = useState<CategoryData>();
  const [action, setAction] = useState<FormAction>(FormAction.CREATE);
  const [isOpened, setIsOpened] = useState<boolean>(false);
  const TABLE_HEADER = [
    "NO.",
    "Tên loại sản phẩm",
    "Mô tả",
    "Time Created",
    "Time Updated",
    "Tác vụ",
  ];

  useEffect(() => {
    categories.length === 0 && getCategories(1);
    // eslint-disable-next-line
  }, []);

  const toggle = (): void => setIsOpened(!isOpened);

  const onCreateCategory = (): void => {
    setAction(FormAction.CREATE);
    setActiveItem(undefined);
    toggle();
  };

  const onUpdateCategory = (data: CategoryData): void => {
    setAction(FormAction.UPDATE);
    setActiveItem(data);
    toggle();
  };

  const getCategories = (PageIndex: number): void => {
    doGetSubCategoriesPaging(PageIndex)
      .then((response: any) => {
        const data: APIResponse<CategoryData> = response;
        setCategories(data.resultObj.items);
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

    doDeleteCategory(id)
      .then((response) => {
        getCategories(currentPage);
      })
      .catch((error) => console.log(error));
  };

  return (
    <AdminContentLayout title="Category" activate={PageName.Category}>
      <div className="d-flex align-items-center justify-content-end">
        <CButton size={ButtonSize.NORMAL} onClick={onCreateCategory}>
          <Image src={Plus} className="bicon" />
          Thêm loại sản phẩm
        </CButton>
      </div>
      <div className="mt-4">
        <CTable>
          <thead>
            <CTRow header data={TABLE_HEADER} />
          </thead>
          <tbody>
            {categories.map((item, index) => (
              <CTRow
                onClick={() => onUpdateCategory(item)}
                key={index}
                data={[
                  index + 1,
                  item.name,
                  item.description,
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
      {categories.length > 0 && (
        <div className="d-flex justify-content-end">
          <CTPaging
            className="mt-4"
            currentPage={currentPage}
            totalPage={totalPage}
            onGetData={getCategories}
          />
        </div>
      )}
      <CategoryWriter
        action={action}
        initialData={activeItem}
        isOpen={isOpened}
        toggle={toggle}
        onAddSucess={() => getCategories(currentPage)}
      />
    </AdminContentLayout>
  );
};

export default Category;
