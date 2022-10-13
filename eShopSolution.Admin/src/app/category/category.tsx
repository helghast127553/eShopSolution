import react, { FC } from "react";
import AdminContentLayout from "../../common/ui/layout/admin-content-layout";
import { PageName } from "../../models/enum";

interface Props {}

const Category: FC<Props> = (props: Props) => {
  return (
    <AdminContentLayout title="Category" activate={PageName.Category}>
      DSADASDSA
    </AdminContentLayout>
  );
};

export default Category;
