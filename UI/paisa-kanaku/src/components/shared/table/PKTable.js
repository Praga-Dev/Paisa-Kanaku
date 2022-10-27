import React from 'react';
import 'antd/dist/antd.css';
// import "./index.css";
import { Table } from 'antd';

const PKTable = (props) => {
  return (
    <div>
      <Table
        columns={props.columns}
        dataSource={props.dataSource}
        rowKey='id'
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

export default PKTable;
