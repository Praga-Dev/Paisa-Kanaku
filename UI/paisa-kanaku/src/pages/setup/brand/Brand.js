import React, { useEffect, useState } from "react";
import DraggableModal from "../../../components/shared/modal/DraggableModal";
import PkTable from "../../../components/shared/table/PKTable";
import axios from "axios";
import { Popconfirm } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";

const baseURL = "https://localhost:7122/v1/setup/brand/";

const Brand = () => {
  const columns = [
    {
      title: "Brand Name",
      dataIndex: "name",
      key: 'id',
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

  useEffect(() => {
    (async () => {
      const response = await axios(baseURL);
      console.log(response)
      if (response && response.data && response.data.data) {
        setBrandContainer(response.data.data);
      }
    })();
  }, []);

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
      <PkTable
        columns={columns}
        dataSource={brandContainer}
        bordered
        title={() => "Brand"}
      />
    </div>
  );
};

export default Brand;
