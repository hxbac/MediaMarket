"use client";

import CustomEditor from "@/components/form/editor";
import { CategoryHomePage } from "@/interfaces/categories";
import { Button, Form, Input, message, Upload, UploadFile } from "antd";
import { useProductContext } from "../_context/ProductContext";
import Tags from "./tags";
import { UploadChangeParam } from "antd/es/upload";

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
    const index = value.categoryIds.indexOf(id);
    if (index > -1) {
      value.categoryIds.splice(index, 1);
    } else {
      value.categoryIds.push(id);
      if (value.categoryIds.length > 3) {
        value.categoryIds.shift();
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

  const handleChangePrice = (priceString: string) => {   
    setValue({ ...value, price: Number(priceString) });
  }

  const onChangeFileEvent = (info: UploadChangeParam<UploadFile>) => {
    const { status } = info.file;
    if (status !== 'uploading') {
      setValue({ ...value, thumbnail: info.file.response.data[0].url });
    }
    if (status === 'done') {
      message.success(`${info.file.name} file uploaded successfully.`);
    } else if (status === 'error') {
      message.error(`${info.file.name} file upload failed.`);
    }
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
        <Form.Item
          label="Giá tiền"
          name="price"
          rules={[
            { required: true, message: "Giá tiền không được trống!" },
          ]}
        >
          <Input placeholder="Nhập giá tiền" value={value.price} onChange={(e) => handleChangePrice(e.target.value) } />
        </Form.Item>
        <Upload
          action={process.env.NEXT_PUBLIC_API_UPLOAD_SINGLE_FILE}
          listType="picture"
          maxCount={1}
          onChange={onChangeFileEvent}
        >
          <Button type="primary">
            Upload
          </Button>
        </Upload>
        <Form.Item
          label="Mô tả ngắn"
          name="shortDescription"
        >
          <Input placeholder="Nhập đoạn mô tả ngắn" value={value.description} onChange={(e) => setValue({...value, shortDescription: e.target.value}) } />
        </Form.Item>
        <CustomEditor value={value.description} onChange={onChangeDescription} />
      </Form>
      <h2 className="text-2xl font-bold mt-8 mb-4">Danh mục</h2>
      <div className="flex flex-wrap justify-center gap-x-2 gap-y-1">
        {categories.map((item) => {
          const isSelected = value.categoryIds.includes(item.id);
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
