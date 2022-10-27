import React from "react";
import "antd/dist/antd.css";
// import "./index.css";
import { Table } from "antd";

const Tables = (props) => {
  return (
    <div>
      {/* <Button type="primary" style={{ marginBottom: 16 }}>
        Add a row
      </Button> */}
      <Table
        columns={props.columns}
        dataSource={props.dataSource}
        bordered
        title={props.title}
        pagination={{
          pageSize: 50,
        }}
        scroll={{
          y: 240,
        }}
      />
    </div>
  );
};

export default Tables;
