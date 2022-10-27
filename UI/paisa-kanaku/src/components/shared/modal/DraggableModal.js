import React, { useRef, useState } from 'react';
import 'antd/dist/antd.css';
import { Button, Modal, Input, Form } from 'antd';
import Draggable from 'react-draggable';
import './DraggableModal.css';

const DraggableModal = (props) => {
  const [open, setOpen] = useState(false);
  const [disabled, setDisabled] = useState(false);
  const [blur, setBlur] = useState(false);
  const [focus, setFocus] = useState(false);
  const [bounds, setBounds] = useState({
    left: 0,
    top: 0,
    bottom: 0,
    right: 0,
  });
  const [brandname, setBrandName] = useState('');

  const brandChangeHandler = (event) => {
    setBrandName(event.target.value);
  };

  const draggleRef = useRef(null);
  const showModal = () => {
    setOpen(true);
  };
  const handleOk = (e) => {
    e.preventDefault();
    //TODO: Make generic draggable modal
    const brandData = {
      brandname: brandname,
    };
    props.onSaveBrandName(brandData);
    setBrandName('');
    setOpen(false);
  };
  const CancelHandler = (e) => {
    setBrandName('');

    setOpen(false);
  };
  const onBlur = () => {
    if (brandname.length < 2) {
      return setBlur(true);
    }
  };
  const onFocus = () => {
    if (brandname.length < 2) {
      return setFocus(true);
    }
  };

  // const CancelHandler = () => {
  //   setBrandName("");
  // };
  const onStart = (_event, uiData) => {
    const { clientWidth, clientHeight } = window.document.documentElement;
    const targetRect = draggleRef.current?.getBoundingClientRect();
    if (!targetRect) {
      return;
    }
    setBounds({
      left: -targetRect.left + uiData.x,
      right: clientWidth - (targetRect.right - uiData.x),
      top: -targetRect.top + uiData.y,
      bottom: clientHeight - (targetRect.bottom - uiData.y),
    });
  };
  return (
    <>
      <Button type='primary' onClick={showModal}>
        {props.newBrand}
      </Button>
      <Modal
        // className="ant-modal"
        title={
          <div
            style={{
              width: '100%',
              cursor: 'move',
            }}
            onMouseOver={() => {
              if (disabled) {
                setDisabled(false);
              }
            }}
            onMouseOut={() => {
              setDisabled(true);
            }}
          >
            {props.modelName}
          </div>
        }
        open={open}
        onOk={handleOk}
        onCancel={CancelHandler}
        modalRender={(modal) => (
          <Draggable
            // className="ant-modal"
            disabled={disabled}
            bounds={bounds}
            onStart={(event, uiData) => onStart(event, uiData)}
          >
            <div ref={draggleRef}>{modal}</div>
          </Draggable>
        )}
      >
        <Form layout='vertical'>
          <Form.Item required label='Name'>
            <Input
              allowClear
              value={brandname}
              onChange={brandChangeHandler}
              onBlur={onBlur}
              onFocus={onFocus}
            />
            {blur ? (
              <span>Enter atleast 2 characters </span>
            ) : focus ? (
              <span>Brand will have atleast 2 characters </span>
            ) : (
              ''
            )}
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default DraggableModal;
