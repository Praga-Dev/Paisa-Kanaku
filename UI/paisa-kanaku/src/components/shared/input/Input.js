import React from "react";
import { Input } from "antd";

const Input = () => {
  return (
    <>
      <Form layout="vertical">
        <Form.Item label="Field A" tooltip="This is a required field">
          <Input />
        </Form.Item>
      </Form>
    </>
  );
};

export default Input;
