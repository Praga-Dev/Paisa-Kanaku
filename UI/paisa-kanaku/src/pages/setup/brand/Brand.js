import React, { useEffect, useState } from 'react';
import DraggableModal from '../../../components/shared/modal/DraggableModal';
import PkTable from '../../../components/shared/table/PKTable';
import axios from 'axios';
import { Popconfirm } from 'antd';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import { getBrandList } from '../../../services/BrandServices';
import { baseURL } from '../../../config/ServiceConfig.js';

// const baseURL = "https://localhost:7122/v1/setup/brand/";
const brandPostBaseUrl = `${baseURL}setup/brand/create/`;

const Brand = () => {
  const columns = [
    {
      title: 'Brand Name',
      dataIndex: 'name',
      key: 'id',
      width: 100,
    },
    {
      title: 'Actions',
      dataIndex: 'actions',
      render: () =>
        brandContainer.length >= 1 ? (
          <Popconfirm title='Sure to delete?'>
            <DeleteOutlined />
          </Popconfirm>
        ) : null,
      width: 150,
    },
    {
      title: 'Actions',
      dataIndex: 'actions',
      render: () =>
        brandContainer.length >= 1 ? (
          <Popconfirm title='Sure to delete?'>
            <EditOutlined />
          </Popconfirm>
        ) : null,
      width: 100,
    },
  ];
  const newBrand = 'Create New Brand';
  const modelName = 'Brand';

  const [brandContainer, setBrandContainer] = useState([]);

  useEffect(() => {
    getBrandList().then((response) => {
      if (response && response.data && response.data.data) {
        setBrandContainer(response.data.data);
      }
    });
  }, []);

  //   useEffect(() => {
  //     postBrandList().then((response) => {
  //      if () {
  //       response
  //      }
  //       setBrandContainer({})
  //    })
  // })

  const onSaveBrandNameHandler = (BrandName) => {
    const brandData = {
      ...BrandName,
    };
    console.log('After brandData', brandData);
    // setBrandContainer([...brandContainer, brandData]);
    // console.log("brandContainer after update", brandContainer);

    axios
      .post(brandPostBaseUrl, brandData)
      .then((res) => {
        console.log(res.data);
        // setBrandContainer("");
      })
      .catch((error) => {
        console.log(error);
      });
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
        title={() => 'Brand'}
      />
    </div>
  );
};

export default Brand;
