"use client";

import CustomEditor from "@/components/form/editor";
import { CategoryHomePage } from "@/interfaces/categories";
import { Button, Flex, Form, Input, message, Modal, Radio, Table, TableProps, Tag, Upload, UploadFile } from "antd";
import { useProductContext } from "../_context/ProductContext";
import Tags from "./tags";
import { UploadChangeParam } from "antd/es/upload";
import { useState } from "react";
import { toast } from "react-toastify";
import { ProductType } from "@/enums/ProductType";
import productService from "@/services/productService";
import { PlusOutlined } from "@ant-design/icons";
import { formatDatetime, formatPrice, formatShowDotPrice } from "@/utils/helpers";
import { DatePicker } from 'antd';
import moment from 'moment';
import { ProductDiscount } from "@/interfaces/products";

const { RangePicker } = DatePicker;

interface EnhanceProductRequest {
  name: string;
  shortDescription: string;
  description: string;
  productType: ProductType
}

interface EnhanceProductResponse {
  description: string,
  tags: string[]
}

export default function Step2({ categories }: { categories: CategoryHomePage[] }) {
  const [form] = Form.useForm();
  const { value, setValue } = useProductContext();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isModalDiscountOpen, setIsModalDiscountOpen] = useState(false);
  const [isLoadingEnhanceProduct, setIsLoadingEnhanceProduct] = useState(false);
  const [enhanceInformationResponse, setEnhanceInformationResponse] = useState<EnhanceProductResponse>({
    description: '',
    tags: []
  });
  const [discountType, setDiscountType] = useState(0);
  const [discountValue, setDiscountValue] = useState(0);
  const [timeDiscount, setTimeDiscount] = useState<string[]>([]);

  const showModal = async () => {
    if (!value.name || !value.shortDescription || !value.description) {
      toast.error('Vui lòng điền tên, đoạn mô tả ngắn để tiếp tục sử dụng chức năng này!');
      return;
    }

    setIsLoadingEnhanceProduct(true);
    setIsModalOpen(true);

    const enhanceRequest: EnhanceProductRequest = {
      name: value.name,
      shortDescription: value.shortDescription,
      description: value.description,
      productType: value.type
    };

    try {
      const result = await productService.enhanceInformation(enhanceRequest);
      if (result.succeeded) {
        setEnhanceInformationResponse(result.data);
        setIsLoadingEnhanceProduct(false);
      } else {
        throw new Error(result.message);
      }
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
      console.log(errorMessage);
      toast.error('Có lỗi xảy ra, vui lòng thử lại sau');
      setIsModalOpen(false);
    }
  };

  const handleOk = async () => {
    if (isLoadingEnhanceProduct) {
      toast.info('Vui lòng chờ');
    }

    setValue({
      ...value,
      description: enhanceInformationResponse.description,
      tags: value.tags
    });
    setIsModalOpen(false);
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
      if (value.categoryIds.length > 10) {
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
    setValue({ ...value, price: Number(priceString.replace(/\D/g, "")) });
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

  const handleCancelModalDiscount = () => {
    setIsModalDiscountOpen(false);
  }

  const handleOkModalDiscount = () => {
    const discount: ProductDiscount = {
      type: discountType,
      value: discountValue,
      timeRange: timeDiscount
    }
    value.discounts.push(discount);
    setValue({
      ...value,
      discounts: value.discounts
    })
    setIsModalDiscountOpen(false);
  }

  const handleDeleteDiscount = (index: number) => {
    setValue({
      ...value,
      discounts: [...value.discounts.splice(index)]
    });
  }

  const discountColumns: TableProps<ProductDiscount>["columns"] = [
    {
      title: "Loại giảm giá",
      dataIndex: "type",
      key: "type",
      render: (text) => <>{text === 0 ? 'Cố định' : 'Theo phần trăm'}</>,
    },
    {
      title: "Giá trị giảm",
      dataIndex: "value",
      key: "value",
      render: (text, record) => record.type === 0 ? formatPrice(text) : text + '%',
    },
    {
      title: "Giá dự kiến",
      key: "est_price",
      render: (_, record) => {
        const price = record.type === 0 ? value.price - record.value : value.price - record.value * value.price / 100;
        return formatPrice(price);
      },
    },
    {
      title: "Thời gian áp dụng",
      dataIndex: "timeRange",
      key: "timeRange",
      render: (timeRange) => {
        return formatDatetime(timeRange[0], 'HH:mm:ss DD-MM-YYYY') + ' - ' + formatDatetime(timeRange[1], 'HH:mm:ss DD-MM-YYYY');
      },
    },
    {
      title: "Hành động",
      key: "action",
      render: (_, record, index) => {
        return <Button color="danger" variant="outlined" onClick={() => handleDeleteDiscount(index + 1)}>Xóa</Button>;
      },
    },
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
          <div className="flex items-center gap-x-4">
            <Input placeholder="Nhập giá tiền" value={formatShowDotPrice(value.price)} onChange={(e) => handleChangePrice(e.target.value) } />
            <Button type="primary" onClick={() => setIsModalDiscountOpen(true)} icon={<PlusOutlined />} iconPosition={'end'}>
              Giảm giá
            </Button>
          </div>
        </Form.Item>
        {
          value.discounts.length > 0 ? (
            <Table<ProductDiscount>
              pagination={false}
              className="pb-4"
              columns={discountColumns}
              dataSource={value.discounts}
            />
          ) : <></>
        }
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
      <div className="mb-16">
        <CustomEditor value={value.description} onChange={onChangeDescription} />
      </div>
      <h2 className="text-sm font-bold mt-8 mb-4">Nhãn sản phẩm</h2>
      <div>
        <Tags tags={value.tags} setTags={setTags} />
      </div>
      <Modal title="Hỗ trợ tạo nội dung bằng AI" open={isModalOpen} centered width={800} onOk={handleOk} onCancel={handleCancel}>
        <h2 className="text-sm font-bold mt-8 mb-4">Đoạn giới thiệu gợi ý</h2>
        <div className="mb-8 rounded-md p-4 bg-gray-50">
          {
            isLoadingEnhanceProduct ?
              (
                <>
                  <div className="h-2.5 bg-gray-200 rounded-full w-48 mb-4"></div>
                  <div className="h-2 bg-gray-200 rounded-full max-w-[360px] mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full max-w-[330px] mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full max-w-[300px] mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full max-w-[360px] mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full mb-2.5"></div>
                  <div className="h-2 bg-gray-200 rounded-full max-w-[360px] mb-2.5"></div>
                </>
              ) :
              <div dangerouslySetInnerHTML={{ __html: enhanceInformationResponse.description }}>
              </div>
          }

        </div>
        <h2 className="text-sm font-bold mt-8 mb-4">Nhãn gợi ý</h2>
        <Flex gap="4px 0" wrap>
          {
            isLoadingEnhanceProduct ?
              <>
                {Array.from(Array(10).keys()).map((_, index) => (<div key={index} className="h-6 bg-gray-200 rounded-md w-12 mr-2.5"></div>))}
              </>
            :
              <>
                {enhanceInformationResponse.tags.map((item, index) => <Tag key={index} color="red">{item}</Tag>)}
              </>
          }

        </Flex>
      </Modal>
      <Modal title="Thêm giảm giá" open={isModalDiscountOpen} centered onOk={handleOkModalDiscount} onCancel={handleCancelModalDiscount}>
        <h2 className="text-sm font-bold mb-4 mt-6">Giá tiền gốc: {formatPrice(value.price)}</h2>
        <Radio.Group className="mb-4" defaultValue="0" value={discountType.toString()} onChange={(e) => setDiscountType(Number(e.target.value))} buttonStyle="solid">
          <Radio.Button value="0">Cố định</Radio.Button>
          <Radio.Button value="1">Phần trăm</Radio.Button>
        </Radio.Group>
        <Input className="mb-4" type="text" value={discountValue.toString()} onChange={(e) => setDiscountValue(Number(e.target.value))} placeholder="Giảm giá" />
        <h2 className="text-sm font-bold mb-4">Giá dự kiến: {discountType === 0 ? formatPrice(value.price - discountValue) : formatPrice(value.price - value.price * discountValue / 100)}</h2>
        <RangePicker disabledDate={(current) => current && current < moment().startOf('day')} onChange={(e, m) => setTimeDiscount(m)} showTime />
      </Modal>
    </div>
  );
}
