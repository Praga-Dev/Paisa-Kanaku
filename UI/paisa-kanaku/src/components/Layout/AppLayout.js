import React from 'react';
import 'antd/dist/antd.css';
import './AppLayout.css';
import { LAYOUT_CONSTANTS, items1 } from './LAYOUT_CONSTANTS.js';
import AppBody from '../../AppBody.js';
import { Layout, Menu } from 'antd';
import { useNavigate } from 'react-router-dom';

const { Header, Content, Sider } = Layout;

const AppLayout = () => {
  const navigate = useNavigate();
  return (
    <Layout>
      <Header className='header'>
        <div className='logo'>
          <h2>PaisaKanaku</h2>
        </div>
        <Menu
          theme='light'
          mode='horizontal'
          defaultSelectedKeys={['2']}
          items={items1}
        />
      </Header>
      <Layout>
        <Sider width={200} className='site-layout-background'>
          <Menu
            onClick={({ key }) => {
              navigate(key);
            }}
            mode='inline'
            defaultSelectedKeys={['1']}
            defaultOpenKeys={['sub1']}
            style={{
              height: '100%',
              borderRight: 0,
            }}
            items={LAYOUT_CONSTANTS}
          />
        </Sider>
        <Layout
          style={{
            padding: '0 24px 24px',
          }}
        >
          <Content>
            <AppBody />
          </Content>
        </Layout>
      </Layout>
    </Layout>
  );
};
export default AppLayout;
