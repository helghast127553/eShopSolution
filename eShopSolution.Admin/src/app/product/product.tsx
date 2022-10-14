import { FC } from "react";
import AdminContentLayout from "../../common/ui/layout/admin-content-layout";
import { PageName } from "../../models/enum";

interface Props {}

const Product: FC<Props> = (props: Props) => {
    return<AdminContentLayout title="Product" activate={PageName.Product}>
        DSADASDSA
    </AdminContentLayout>
};

export default Product;