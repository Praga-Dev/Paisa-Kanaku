import React from 'react';
// import { useNavigate } from "react-router-dom";

import { LaptopOutlined } from '@ant-design/icons';

// export const items1 = ["Report", "Forms", "Barchart"].map((key) => ({
//   key,
//   label: `${key}`,
// }));
export const items1 = [];

export const LAYOUT_CONSTANTS = [
  {
    key: `SETUP`,
    icon: React.createElement(LaptopOutlined),
    label: `Setup`,

    children: [
      {
        key: '/',
        label: `Brand`,
      },
      {
        key: '/member',
        label: `Member`,
      },
      {
        key: '/productCategory',
        label: `Product Category`,
      },
      {
        key: '/product',
        label: `Product`,
      },
    ],
  },
];
