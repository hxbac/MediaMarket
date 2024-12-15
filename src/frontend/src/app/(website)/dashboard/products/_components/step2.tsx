"use client";

import CustomEditor from "@/components/form/editor";
import { CategoryHomePage } from "@/interfaces/categories";
import { Button, Flex, Form, Input, message, Modal, Tag, Upload, UploadFile } from "antd";
import { useProductContext } from "../_context/ProductContext";
import Tags from "./tags";
import { UploadChangeParam } from "antd/es/upload";
import { useState } from "react";

export default function Step2({ categories }: { categories: CategoryHomePage[] }) {
  const [form] = Form.useForm();
  const { value, setValue } = useProductContext();
  const [isModalOpen, setIsModalOpen] = useState(false);

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = async () => {

  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

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

  const suggestTags = [
    {
      text: 'Cố đô Huế',
      color: 'magenta'
    },
    {
      text: 'Di sản văn hóa',
      color: 'red'
    },
    {
      text: 'Phong cảnh Việt Nam',
      color: 'volcano'
    },
    {
      text: 'Kiến trúc cổ',
      color: 'orange'
    },
    {
      text: 'Sông Hương',
      color: 'gold'
    },
    {
      text: 'Lăng tẩm triều Nguyễn',
      color: 'lime'
    },
    {
      text: 'Núi Ngự Bình',
      color: 'magenta'
    },
    {
      text: 'Đại Nội Huế',
      color: 'orange'
    },
    {
      text: 'Văn hóa truyền thống',
      color: 'gold'
    },
    {
      text: 'Du lịch Huế',
      color: 'orange'
    }
  ];

  return (
    <div className="">
      <h2 className="text-2xl font-bold mt-8 mb-4">Thông tin sản phẩm</h2>
      <Form form={form} layout="vertical">
        <Form.Item
          label={<label className="text-sm font-bold">Tên sản phẩm</label>}
          name="Input2"
          rules={[
            { required: true, message: "Tên sản phẩm không được trống!" },
          ]}
        >
          <Input placeholder="Nhập tên sản phẩm" value={value.name} onChange={(e) => setValue({ ...value, name: e.target.value })} />
        </Form.Item>
        <Form.Item
          label={<label className="text-sm font-bold">Giá tiền</label>}
          name="price"
          rules={[
            { required: true, message: "Giá tiền không được trống!" },
          ]}
        >
          <Input placeholder="Nhập giá tiền" value={value.price} onChange={(e) => handleChangePrice(e.target.value) } />
        </Form.Item>
        <h2 className="text-sm font-bold mt-6 mb-2">Ảnh sản phẩm</h2>
        <div className="mb-4">
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
        </div>
        <Form.Item
          label={<label className="text-sm font-bold">Mô tả ngắn</label>}
          name="shortDescription"
        >
          <Input placeholder="Nhập đoạn mô tả ngắn" value={value.description} onChange={(e) => setValue({...value, shortDescription: e.target.value}) } />
        </Form.Item>
      </Form>
      <h2 className="text-sm font-bold mt-6 mb-2">Danh mục</h2>
      <div className="flex flex-wrap gap-x-2 gap-y-1 mb-4">
        {categories.map((item) => {
          const isSelected = value.categoryIds.includes(item.id);
          return (
            <div key={item.id} onClick={() => handleClickCategory(item.id)} className={`py-1 px-4 font-bold whitespace-nowrap transition-all border border-gray-700 rounded-[100px] ${isSelected ? 'bg-purple-600 border-purple-600 text-white' : 'hover:bg-purple-600 hover:border-purple-600 hover:text-white'} cursor-pointer category-item`}>
              {item.name}
            </div>
          );
        })}
      </div>
      <div className="flex items-center justify-between">
        <h2 className="text-sm font-bold mt-6 mb-2">Mô tả sản phẩm</h2>
        <button className="flex items-center text-purple-600 disabled:opacity-50" id="enhance-description-btn" onClick={showModal}>
          <div className="hidden" role="status" id="enhance-loading">
              <svg aria-hidden="true" className="w-4 h-4 me-2 text-gray-200 animate-spin dark:text-gray-600 fill-purple-600" viewBox="0 0 100 101" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z" fill="currentColor"></path><path d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z" fill="currentFill"></path></svg>
              <span className="sr-only">Loading...</span>
          </div>
          <svg id="enhance-icon" xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16"><g id="Group_174545" data-name="Group 174545" transform="translate(-376 -151)"><path id="Path_171728" data-name="Path 171728" d="M10.4,48.464a7.4,7.4,0,0,0-4.744,4.744.345.345,0,0,1-.656,0,7.358,7.358,0,0,0-1.8-2.941,7.37,7.37,0,0,0-2.94-1.8.344.344,0,0,1,0-.656,7.361,7.361,0,0,0,2.94-1.8A7.344,7.344,0,0,0,5,43.068a.345.345,0,0,1,.656,0,7.379,7.379,0,0,0,4.741,4.742.345.345,0,0,1,0,.654Z" transform="translate(375.983 113.554)" fill="#3339f1"></path><path id="Path_171729" data-name="Path 171729" d="M92.288,3.259a4.376,4.376,0,0,0-2.8,2.8.2.2,0,0,1-.387,0,4.385,4.385,0,0,0-2.8-2.8.2.2,0,0,1,0-.388A4.34,4.34,0,0,0,88.035,1.8,4.356,4.356,0,0,0,89.094.065a.2.2,0,0,1,.387,0A4.376,4.376,0,0,0,90.548,1.8a4.343,4.343,0,0,0,1.74,1.067.2.2,0,0,1,0,.388Z" transform="translate(299.557 151.084)" fill="#3339f1"></path><path id="Path_171730" data-name="Path 171730" d="M58.372,48.437a7.4,7.4,0,0,0-4.744,4.744.338.338,0,0,1-.3.237c0-.026-.008-10.516,0-10.618a.338.338,0,0,1,.3.236,7.379,7.379,0,0,0,4.741,4.742.345.345,0,0,1,0,.659Z" transform="translate(327.983 113.581)" fill="#3339f1" opacity="0.2"></path><path id="Path_171731" data-name="Path 171731" d="M120.595,3.335a4.376,4.376,0,0,0-2.8,2.8.2.2,0,0,1-.193.141V0a.2.2,0,0,1,.193.141A4.377,4.377,0,0,0,118.86,1.88a4.343,4.343,0,0,0,1.74,1.067.2.2,0,0,1-.005.388Z" transform="translate(271.251 151.008)" fill="#3339f1" opacity="0.2"></path></g></svg>
          <span className="ml-2 text-sm font-bold">Cải thiện bằng AI</span>
        </button>
      </div>
      <div className="mb-4">
        <CustomEditor value={value.description} onChange={onChangeDescription} />
      </div>
      <h2 className="text-sm font-bold mt-8 mb-4">Nhãn sản phẩm</h2>
      <div>
        <Tags tags={value.tags} setTags={setTags} />
      </div>
      <Modal title="Hỗ trợ tạo nội dung bằng AI" open={isModalOpen} centered width={800} onOk={handleOk} onCancel={handleCancel}>
        <h2 className="text-sm font-bold mt-8 mb-4">Đoạn giới thiệu gợi ý</h2>
        <div className="mb-8 rounded-md p-4 bg-gray-100">
          <h4>Cố đô Huế – Hành trình khám phá vẻ đẹp lịch sử và văn hóa qua từng khung hình</h4>
          <p>Cố đô Huế, từng là trung tâm chính trị, văn hóa và tôn giáo của Việt Nam trong hơn 140 năm triều Nguyễn, vẫn giữ nguyên vẻ đẹp cổ kính và sự trang nghiêm của một vùng đất di sản. Album hình ảnh về Cố đô Huế là một hành trình hình ảnh đầy cảm xúc, tái hiện những giá trị tinh thần và nghệ thuật đỉnh cao mà mảnh đất này mang lại.</p>
          <p>Bước vào album, bạn sẽ được chiêm ngưỡng những khung cảnh lộng lẫy và trang nghiêm của các công trình kiến trúc di sản. Đại Nội Huế, với Ngọ Môn uy nghi, Điện Thái Hòa lộng lẫy và những cung điện, đền đài mang đậm phong cách kiến trúc triều Nguyễn, được khắc họa rõ nét qua từng bức ảnh. Những chi tiết tinh xảo trên các mái ngói lưu ly, các cột gỗ chạm trổ hoa văn hay hệ thống sân vườn hài hòa là minh chứng cho sự tài hoa của nghệ nhân xưa.</p>
          <p>Không dừng lại ở đó, album còn dẫn dắt bạn đến những lăng tẩm nổi tiếng, nơi an nghỉ của các vị vua Nguyễn. Lăng Minh Mạng với kiến trúc đối xứng tuyệt đẹp, lăng Khải Định độc đáo với sự kết hợp giữa phong cách Á – Âu, hay lăng Tự Đức thơ mộng giữa thiên nhiên xanh mát đều được tái hiện sinh động, đưa người xem bước vào không gian hoài niệm và sâu lắng.</p>
          <p>Ngoài các di sản kiến trúc, album cũng ghi lại vẻ đẹp tự nhiên đặc trưng của Huế. Sông Hương mềm mại như dải lụa uốn quanh thành phố, lúc hiền hòa phản chiếu ánh nắng, lúc mơ màng trong sương sớm. Núi Ngự Bình, biểu tượng thiêng liêng của Huế, hiện lên sừng sững giữa khung trời xanh, như một bức bình phong che chở cho mảnh đất cố đô.</p>
        </div>
        <h2 className="text-sm font-bold mt-8 mb-4">Nhãn gợi ý</h2>
        <Flex gap="4px 0" wrap>
          {suggestTags.map((item, index) => <Tag key={index} color={item.color}>{item.text}</Tag>)}
        </Flex>
      </Modal>
    </div>
  );
}
