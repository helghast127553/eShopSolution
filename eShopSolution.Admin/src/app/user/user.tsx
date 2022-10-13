import react, { FC } from "react";

import AdminContentLayout from "../../common/ui/layout/admin-content-layout";
import { PageName } from "../../models/enum";

interface Props {}

const User: FC<Props> = (props: Props) => {
  return (
    <AdminContentLayout title="User" activate={PageName.User}>
      User
    </AdminContentLayout>
  );
};

export default User;
