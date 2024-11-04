"use client";

import CustomEditor from "@/components/form/editor";
import { Form, Input } from "antd";

export default function Step2() {
  const categories = [
    {
      name: "Featured",
    },
    {
      name: "Music",
    },
    {
      name: "Drawing & Painting",
    },
    {
      name: "Marketing",
    },
    {
      name: "Animation",
    },
    {
      name: "Social Media",
    },
    {
      name: "UI/UX design",
    },
    {
      name: "Creative Writing",
    },
    {
      name: "Featured",
    },
    {
      name: "Music",
    },
    {
      name: "Drawing & Painting",
    },
    {
      name: "Marketing",
    },
    {
      name: "Animation",
    },
    {
      name: "Social Media",
    },
    {
      name: "UI/UX design",
    },
    {
      name: "Creative Writing",
    },
  ];
  const [form] = Form.useForm();

  return (
    <div className="">
      <h2 className="text-2xl font-bold mt-8 mb-4">Thông tin sản phẩm</h2>
      <Form form={form} layout="vertical">
        <Form.Item
          label="Tên sản phẩm"
          name="Input2"
          rules={[
            { required: true, message: "Tên sản phẩm không được trống!" },
          ]}
        >
          <Input placeholder="Nhập tên sản phẩm" />
        </Form.Item>
        <CustomEditor />
      </Form>
      <h2 className="text-2xl font-bold mt-8 mb-4">Danh mục</h2>
      <div className="flex flex-wrap justify-center gap-x-2 gap-y-1">
        {categories.map((item, index) => {
          return (
            <div key={index} className="py-1 px-4 font-bold whitespace-nowrap transition-all border border-gray-700 rounded-[100px] hover:bg-purple-600 hover:border-purple-600 hover:text-white cursor-pointer category-item">
              {item.name}
              <div className="delete">
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
