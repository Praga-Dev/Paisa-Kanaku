import React from 'react';
import Brand from '../setup/brand/Brand';
import Member from '../setup/member/Member';
import { Routes, Route } from 'react-router-dom';

const Dashboard = () => {
  return (
    <div>
      <Routes>
        <Route path='/' element={<Brand />}></Route>
        <Route path='/member' element={<Member />}></Route>
        {/* <Route></Route> */}
      </Routes>
      {/* <Brand />
      <Member /> */}
    </div>
  );
};

export default Dashboard;
