import React from "react";
import "antd/dist/antd.css";
import "./AppLayout.css";
import { LAYOUT_ITEMS, items1 } from "./LAYOUT_ITEMS.js";
import AppBody from "../../AppBody.js";
import { Layout, Menu } from "antd";

const { Header, Content, Sider } = Layout;

const AppLayout = () => (
  <Layout>
    <Header className="header">
      <div className="logo"></div>
      <Menu
        theme="light"
        mode="horizontal"
        defaultSelectedKeys={["2"]}
        items={items1}
      />
    </Header>
    <Layout>
      <Sider width={200} className="site-layout-background">
        <Menu
          mode="inline"
          defaultSelectedKeys={["1"]}
          defaultOpenKeys={["sub1"]}
          style={{
            height: "100%",
            borderRight: 0,
          }}
          items={LAYOUT_ITEMS}
        />
      </Sider>
      <Layout
        style={{
          padding: "0 24px 24px",
        }}
      >
        <Content>
          <AppBody />
        </Content>
      </Layout>
    </Layout>
  </Layout>
);
export default AppLayout;
