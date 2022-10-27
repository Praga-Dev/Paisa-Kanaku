import React, { useEffect, useState } from "react";
import DraggableModal from "../../../components/shared/modal/DraggableModal";
import Tables from "../../../components/shared/table/Table";
import axios from "axios";
import { Popconfirm } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";

const baseURL = "https://localhost:7122/v1/setup/brand/";

const Brand = (props) => {
  const columns = [
    {
      title: "Brand Name",
      dataIndex: "brandContainer",
      width: 100,
    },
    {
      title: "Actions",
      dataIndex: "actions",
      render: () =>
        brandContainer.length >= 1 ? (
          <Popconfirm title="Sure to delete?">
            <DeleteOutlined />
          </Popconfirm>
        ) : null,
      width: 150,
    },
    {
      title: "Actions",
      dataIndex: "actions",
      render: () =>
        brandContainer.length >= 1 ? (
          <Popconfirm title="Sure to delete?">
            <EditOutlined />
          </Popconfirm>
        ) : null,
      width: 100,
    },
  ];
  const newBrand = "Create New Brand";
  const modelName = "Brand";

  const [brandContainer, setBrandContainer] = useState([]);

  axios.get(baseURL).then((response) => {
    console.log(response.data);
    if (response && response.data) {
      setBrandContainer(response.data);
    }
  });

  const onSaveBrandNameHandler = (BrandName) => {
    console.log("BrandName", BrandName);
    const brandData = {
      ...BrandName,
    };

    console.log("brandContainer before update", brandContainer);
    setBrandContainer([...brandContainer, brandData]);
    console.log("brandContainer after update", brandContainer);
  };

  return (
    <div>
      <DraggableModal
        newBrand={newBrand}
        modelName={modelName}
        onSaveBrandName={onSaveBrandNameHandler}
      />
      <Tables
        columns={columns}
        dataSource={brandContainer}
        bordered
        title={() => "Brand"}
      />
    </div>
  );
};

export default Brand;
