import { FC, useEffect, useState } from "react";
import { PageName } from "../../models/enum";
import { UserProfile, APIResponse } from "../../models";
import { doGetUsers } from "./api";
import { CTable, CTPaging, CTRow } from "../../common/ui/base";
import AdminContentLayout from "../../common/ui/layout/admin-content-layout";
import NotData from "../../../common/ui/assets/ic/no-data.svg";

interface Props {}

const User: FC<Props> = (props: Props) => {
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPage, setTotalPage] = useState<number>(1);
  const [userProfiles, setUserProfiles] = useState<Array<UserProfile>>([]);
  const TABLE_HEADER = [
    "NO.",
    "Tài khoản đăng nhập",
    "Họ tên",
    "Email",
    "Số điện thoại",
  ];

  const getUsers = (PageIndex: number): void => {
    doGetUsers(PageIndex)
      .then((response) => {
        console.log(response);
        const data: APIResponse<UserProfile> = response.data;
        setUserProfiles(data.resultObj.items);
        setCurrentPage(data.resultObj.pageIndex);
        setTotalPage(
          Math.ceil(data.resultObj.totalRecords / data.resultObj.pageSize)
        );
      })
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    userProfiles.length === 0 && getUsers(1);
    // eslint-disable-next-line
  }, []);

  return (
    <AdminContentLayout title="User" activate={PageName.User}>
      <CTable>
        <thead>
          <CTRow header data={TABLE_HEADER} />
        </thead>
        <tbody>
          {userProfiles.map((item, index) => (
            <CTRow
              key={index}
              data={[
                index + 1,
                item.userName,
                item.lastName + " " + item.firstName,
                item.email,
                item.phoneNumber,
              ]}
            />
          ))}
        </tbody>
      </CTable>
      {userProfiles.length > 0 && (
        <div className="d-flex justify-content-end">
          <CTPaging
            className="mt-4"
            currentPage={currentPage}
            totalPage={totalPage}
            onGetData={getUsers}
          />
        </div>
      )}
    </AdminContentLayout>
  );
};

export default User;
