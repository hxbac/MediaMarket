"use client";

import CustomEditor from "@/components/form/editor";
import { InfoCircleOutlined } from "@ant-design/icons";
import { Form, Input } from "antd";

export default function Step2() {
  const [form] = Form.useForm();

  return (
    <div>
      <h2 className="text-2xl font-bold mt-8 mb-4">Thông tin sản phẩm</h2>
      <Form form={form} layout="vertical">
        <Form.Item
          label="Tên sản phẩm"
          name="Input2"
          rules={[
            { required: true, message: "Tên sản phẩm không được trống!" }
          ]}
        >
          <Input placeholder="Nhập tên sản phẩm" />
        </Form.Item>
        <Form.Item
          label="Field B"
          tooltip={{
            title: "Tooltip with customize icon",
            icon: <InfoCircleOutlined />,
          }}
        >
          <Input placeholder="input placeholder" />
        </Form.Item>
        <CustomEditor />
      </Form>
    </div>
  );
}
