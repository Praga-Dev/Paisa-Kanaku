import React from "react";

import {
  LaptopOutlined,
  NotificationOutlined,
  UserOutlined,
} from "@ant-design/icons";

// export const items1 = ["Report", "Forms", "Barchart"].map((key) => ({
//   key,
//   label: `${key}`,
// }));
export const items1 = [];

export const LAYOUT_ITEMS = [
  {
    key: `SETUP`,
    icon: React.createElement(LaptopOutlined),
    label: `Setup`,
    children: [
      {
        key: "BRAND",
        label: `Brand`,
      },
      {
        key: "MEMBER",
        label: `Member`,
      },
      {
        key: "PRODUCT_CATEGORY",
        label: `Product Category`,
      },
      {
        key: "PRODUCT",
        label: `Product`,
      },
    ],
  },
];
