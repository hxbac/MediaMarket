"use client";

import CustomEditor from "@/components/form/editor";
import { CategoryHomePage } from "@/interfaces/categories";
import { Form, Input } from "antd";
import { useProductContext } from "../_context/ProductContext";
import Tags from "./tags";

export default function Step2({ categories }: { categories: CategoryHomePage[] }) {
  const [form] = Form.useForm();
  const { value, setValue } = useProductContext();

  const onChangeDescription = (newValue: string) => {
    setValue({
      ...value,
      description: newValue
    });
  }

  const handleClickCategory = (id: string) => {
    const index = value.categories.indexOf(id);
    if (index > -1) {
      value.categories.splice(index, 1);
    } else {
      value.categories.push(id);
      if (value.categories.length > 3) {
        value.categories.shift();
      }
    }
    setValue({ ...value });
  }

  const setTags = (newTags: string[]) => {
    setValue({
      ...value,
      tags: newTags
    });
  }

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
          <Input placeholder="Nhập tên sản phẩm" value={value.name} onChange={(e) => setValue({ ...value, name: e.target.value })} />
        </Form.Item>
        <CustomEditor value={value.description} onChange={onChangeDescription} />
      </Form>
      <h2 className="text-2xl font-bold mt-8 mb-4">Danh mục</h2>
      <div className="flex flex-wrap justify-center gap-x-2 gap-y-1">
        {categories.map((item) => {
          const isSelected = value.categories.includes(item.id);
          return (
            <div key={item.id} onClick={() => handleClickCategory(item.id)} className={`py-1 px-4 font-bold whitespace-nowrap transition-all border border-gray-700 rounded-[100px] ${isSelected ? 'bg-purple-600 border-purple-600 text-white' : 'hover:bg-purple-600 hover:border-purple-600 hover:text-white'} cursor-pointer category-item`}>
              {item.name}
            </div>
          );
        })}
      </div>
      <h2 className="text-2xl font-bold mt-8 mb-4">Nhãn sản phẩm</h2>
      <div>
        <Tags tags={value.tags} setTags={setTags} />
      </div>
    </div>
  );
}
