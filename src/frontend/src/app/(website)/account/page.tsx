"use client";

import Card from "@/components/product/card";
import { MyProductLatest } from "@/interfaces/products";
import productService from "@/services/productService";
import { useAppSelector } from "@/store/hooks";
import { Button, DatePicker, DatePickerProps, Form, Image, Input, message, Modal } from "antd";
import TextArea from "antd/es/input/TextArea";
import Upload, { UploadChangeParam, UploadFile } from "antd/es/upload";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface Profile {
  avatar: string;
  name: string;
  description: string;
  phoneNumber: string;
  address: string;
  birthday: string;
}

export default function Page() {
  const [form] = Form.useForm();
  const [products, setProducts] = useState<MyProductLatest[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const user = useAppSelector(state => state.user.user);
  const [profile, setProfile] = useState<Profile>({
    avatar: '',
    name: '',
    description: '',
    phoneNumber: '',
    address: '',
    birthday: ''
  });

  useEffect(() => {
    if (user === null) {
      return;
    }

    setProfile({
      avatar: user.avatar ?? '',
      name: user.name,
      description: user.description ?? '',
      phoneNumber: user.phoneNumber ?? '',
      address: user.address ?? '',
      birthday: user.birthday ?? ''
    })
  }, [user]);

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  useEffect(() => {
    const fetchMyLatestProducts = async () => {
      try {
        const result = await productService.getMyLatestProducts();
        if (result.succeeded) {
          setProducts(result.data);
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        toast.error(errorMessage);
      }
    }

    fetchMyLatestProducts();
  }, []);

  const onChangeFileEvent = (info: UploadChangeParam<UploadFile>) => {
    const { status } = info.file;
    if (status !== 'uploading') {
      setProfile({
        ...profile,
        avatar: info.file.response.data[0].url
      });
    }
    if (status === 'done') {
      message.success(`${info.file.name} file uploaded successfully.`);
    } else if (status === 'error') {
      message.error(`${info.file.name} file upload failed.`);
    }
  }

  const onChangeBirthday: DatePickerProps['onChange'] = (date, dateString) => {
    let birthday = '';
    if (Array.isArray(dateString)) {
      birthday = dateString[0];
    } else {
      birthday = dateString;
    }

    setProfile({
      ...profile,
      birthday
    });
  };

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-10 grid grid-cols-1">
        <div data-aos="fade" className="aos-init aos-animate">
          <div className="py-12">
            <div className="flex items-center justify-between">
              <h2 className="after-effect after:left-52 mt-12 lg:mt-0 ">
                About Me
              </h2>
              <div className="pr-8">
                <Button type="primary" onClick={showModal}>Chỉnh sửa thông tin</Button>
              </div>
            </div>
            <div className="grid grid-cols-12 md:gap-10 pt-4 md:pt-[40px] items-center">
              <div className="col-span-12 md:col-span-4">
                <Image
                  src={user?.avatar ?? '/images/no-avatar.png'}
                  alt={user?.name}
                  className="w-full md:w-[330px] h-auto object-cover overflow-hidden rounded-[35px] mb-3 md:mb-0"
                  loading="lazy"
                />
              </div>
              <div className="col-span-12 md:col-span-8 space-y-2.5">
                <div className=" md:mr-12 xl:mr-16">
                  <h3 className="text-4xl font-medium mb-2.5 ">
                    Tôi là ai?
                  </h3>
                  <p className="text-gray-lite leading-7">
                    {user?.description}
                  </p>
                </div>
                <div>
                  <h3 className="text-4xl font-medium my-5 capitalize">
                    Thông tin cơ bản
                  </h3>
                  <div className=" grid grid-cols-1 sm:grid-cols-2 gap-6">
                    <div className="flex">
                      <span className="text-oriange shadow-icon mr-2.5 flex items-center justify-center rounded-md text-2xl w-12 text-">
                        <svg
                          stroke="currentColor"
                          fill="currentColor"
                          strokeWidth="0"
                          viewBox="0 0 320 512"
                          height="1em"
                          width="1em"
                          xmlns="http://www.w3.org/2000/svg"
                        >
                          <path d="M272 0H48C21.5 0 0 21.5 0 48v416c0 26.5 21.5 48 48 48h224c26.5 0 48-21.5 48-48V48c0-26.5-21.5-48-48-48zM160 480c-17.7 0-32-14.3-32-32s14.3-32 32-32 32 14.3 32 32-14.3 32-32 32zm112-108c0 6.6-5.4 12-12 12H60c-6.6 0-12-5.4-12-12V60c0-6.6 5.4-12 12-12h200c6.6 0 12 5.4 12 12v312z"></path>
                        </svg>
                      </span>
                      <div className="space-y-1">
                        <p className="text-xs text-gray-lite capitalize">
                          Số điện thoại
                        </p>
                        <h6 className="font-medium">
                          <a
                            className="hover:text-[#FA5252] duration-300 transition"
                            href="tel:+1234567890"
                          >
                            {user?.phoneNumber}
                          </a>
                        </h6>
                      </div>
                    </div>
                    <div className="flex">
                      <span className="text-oriange-lite shadow-icon mr-2.5 flex items-center justify-center rounded-md text-2xl w-12 text-">
                        <svg
                          stroke="currentColor"
                          fill="currentColor"
                          strokeWidth="0"
                          viewBox="0 0 384 512"
                          height="1em"
                          width="1em"
                          xmlns="http://www.w3.org/2000/svg"
                        >
                          <path d="M172.268 501.67C26.97 291.031 0 269.413 0 192 0 85.961 85.961 0 192 0s192 85.961 192 192c0 77.413-26.97 99.031-172.268 309.67-9.535 13.774-29.93 13.773-39.464 0zM192 272c44.183 0 80-35.817 80-80s-35.817-80-80-80-80 35.817-80 80 35.817 80 80 80z"></path>
                        </svg>
                      </span>
                      <div className="space-y-1">
                        <p className="text-xs text-gray-lite">
                          Địa chỉ
                        </p>
                        <h6 className="font-medium">
                          {user?.address}
                        </h6>
                      </div>
                    </div>
                    <div className="flex">
                      <span className="text-green shadow-icon mr-2.5 flex items-center justify-center rounded-md text-2xl w-12 text-">
                        <svg
                          stroke="currentColor"
                          fill="currentColor"
                          strokeWidth="0"
                          viewBox="0 0 512 512"
                          height="1em"
                          width="1em"
                          xmlns="http://www.w3.org/2000/svg"
                        >
                          <path d="M176 216h160c8.84 0 16-7.16 16-16v-16c0-8.84-7.16-16-16-16H176c-8.84 0-16 7.16-16 16v16c0 8.84 7.16 16 16 16zm-16 80c0 8.84 7.16 16 16 16h160c8.84 0 16-7.16 16-16v-16c0-8.84-7.16-16-16-16H176c-8.84 0-16 7.16-16 16v16zm96 121.13c-16.42 0-32.84-5.06-46.86-15.19L0 250.86V464c0 26.51 21.49 48 48 48h416c26.51 0 48-21.49 48-48V250.86L302.86 401.94c-14.02 10.12-30.44 15.19-46.86 15.19zm237.61-254.18c-8.85-6.94-17.24-13.47-29.61-22.81V96c0-26.51-21.49-48-48-48h-77.55c-3.04-2.2-5.87-4.26-9.04-6.56C312.6 29.17 279.2-.35 256 0c-23.2-.35-56.59 29.17-73.41 41.44-3.17 2.3-6 4.36-9.04 6.56H96c-26.51 0-48 21.49-48 48v44.14c-12.37 9.33-20.76 15.87-29.61 22.81A47.995 47.995 0 0 0 0 200.72v10.65l96 69.35V96h320v184.72l96-69.35v-10.65c0-14.74-6.78-28.67-18.39-37.77z"></path>
                        </svg>
                      </span>
                      <div className="space-y-1">
                        <p className="text-xs text-gray-lite">
                          Email
                        </p>
                        <h6 className="font-medium">
                          {" "}
                          <a
                            className="hover:text-[#FA5252] duration-300 transition"
                            href="mailto:ibthemes21@gmail.com"
                          >
                            {user?.email}
                          </a>
                        </h6>
                      </div>
                    </div>
                    <div className="flex">
                      <span className="text-color-50 shadow-icon mr-2.5 flex items-center justify-center rounded-md text-2xl w-12 text-">
                        <svg
                          stroke="currentColor"
                          fill="currentColor"
                          strokeWidth="0"
                          viewBox="0 0 448 512"
                          height="1em"
                          width="1em"
                          xmlns="http://www.w3.org/2000/svg"
                        >
                          <path d="M148 288h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm108-12v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 96v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm192 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96-260v352c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V112c0-26.5 21.5-48 48-48h48V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h128V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h48c26.5 0 48 21.5 48 48zm-48 346V160H48v298c0 3.3 2.7 6 6 6h340c3.3 0 6-2.7 6-6z"></path>
                        </svg>
                      </span>
                      <div className="space-y-1">
                        <p className="text-xs text-gray-lite">
                          Ngày Sinh
                        </p>
                        <h6 className="font-medium">
                          {user?.birthday}
                        </h6>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="pb-12 ">
            <h3 className="text-[35px] font-medium pb-5">
              What I do!
            </h3>
            <div className="grid gap-8 grid-cols-2 md:grid-cols-3 xl:grid-cols-4 ">
              {
                products.map(item => <Card key={item.id} data={item} />)
              }
            </div>
          </div>
        </div>
      </div>
      <Modal title="Chỉnh sửa thông tin cá nhân" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>

        <Form form={form} layout="vertical">
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
            label="Tên hiển thị"
            name="name"
            className="mb-2"
            rules={[
              { required: true, message: "Tên không được trống!" },
            ]}
          >
            <Input placeholder="" value={profile.name ?? 'yesy'} onChange={(e) => setProfile({ ...profile, name: e.target.value })} />
          </Form.Item>
          <Form.Item
            label="Số điện thoại"
            className="mb-2"
            name="phoneNumber"
          >
            <Input placeholder="" value={profile.phoneNumber} onChange={(e) => setProfile({ ...profile, phoneNumber: e.target.value })} />
          </Form.Item>
          <Form.Item
            label="Địa chỉ"
            className="mb-2"
            name="address"
          >
            <Input placeholder="" value={profile.address} onChange={(e) => setProfile({ ...profile, address: e.target.value })} />
          </Form.Item>
          <Form.Item
            label="Ngày sinh"
            className="mb-2"
            name="birthday"
          >
            <DatePicker onChange={onChangeBirthday} />
          </Form.Item>
          <Form.Item
            label="Mô tả"
            className="mb-2"
            name="description"
          >
            <TextArea
              value={profile.description}
              onChange={(e) => setProfile({ ...profile, description: e.target.value })}
              autoSize={{ minRows: 3, maxRows: 5 }}
            />
          </Form.Item>
        </Form>
      </Modal>
    </section>
  );
}
