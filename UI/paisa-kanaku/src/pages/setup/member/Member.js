import React from "react";
import Tables from "../../../components/shared/table/Table";

const columns = [
  {
    title: "Member Name",
    dataIndex: "membername",
    width: 600,
  },
  {
    title: "Actions",
    dataIndex: "actions",
  },
];
const data = [];
for (let i = 0; i < 100; i++) {
  data.push({
    key: i,
    membername: `Edward King ${i}`,
    actions: `London, Park Lane no. ${i}`,
  });
}
const Member = () => {
  return (
    <div>
      <Tables columns={columns} dataSource={data} title={() => "Member"} />
    </div>
  );
};

export default Member;
