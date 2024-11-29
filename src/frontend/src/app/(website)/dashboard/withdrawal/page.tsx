"use client";

import { ProductType } from "@/enums/ProductType";
import {
  Button,
  Modal,
  Space,
  Table,
  TablePaginationConfig,
  TableProps,
  Tag,
} from "antd";
import Image from "next/image";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import Link from "next/link";
import { formatPrice } from "@/utils/helpers";
import orderService from "@/services/orderService";
import OrderStatusTag from "@/components/order/orderStatus";

interface DataType {
  key: string;
  slug: string;
  thumbnail: number;
  name: string;
  price: number;
  categories: string[];
  orderStatus: number;
}

export default function Video() {
  const [data, setData] = useState([]);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 2,
    total: 0,
  });
  const [isModalOpen, setIsModalOpen] = useState(false);

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const fetchData = async (page = 1, pageSize = 10) => {
    try {
    } catch (error: unknown) {
      const errorMessage =
        error instanceof Error ? error.message : "An unexpected error occurred";
      toast.error(errorMessage);
    }
  };

  useEffect(() => {
    fetchData(pagination.current, pagination.pageSize);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleTableChange = (pagination: TablePaginationConfig) => {
    fetchData(pagination.current, pagination.pageSize);
  };

  const columns: TableProps<DataType>["columns"] = [
    {
      title: "Hình ảnh",
      dataIndex: "thumbnail",
      key: "thumbnail",
      render: (text) => (
        <Image src={text} width={100} height={100} alt={text} unoptimized />
      ),
    },
    {
      title: "Tên sản phẩm",
      dataIndex: "name",
      key: "name",
      render: (text) => <a>{text}</a>,
    },
    {
      title: "Giá",
      dataIndex: "price",
      key: "price",
      render: (price) => formatPrice(price),
    },
    {
      title: "Danh mục",
      key: "categories",
      dataIndex: "categories",
      render: (_, { categories }) => (
        <>
          {categories.map((category) => {
            let color = category.length > 5 ? "geekblue" : "green";
            if (category === "loser") {
              color = "volcano";
            }
            return (
              <Tag color={color} key={category}>
                {category.toUpperCase()}
              </Tag>
            );
          })}
        </>
      ),
    },
    {
      title: "Trạng thái",
      dataIndex: "orderStatus",
      key: "orderStatus",
      render: (status) => <OrderStatusTag status={status} />,
    },
    {
      title: "Hành Động",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <Link href={`/products/${record.slug}`} target="_blank">
            <Button>Xem</Button>
          </Link>
          <Link href={`/products/${record.key}`}>
            <Button type="primary" danger>
              Xóa
            </Button>
          </Link>
        </Space>
      ),
    },
  ];

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <h1 className="text-center uppercase text-xl font-bold mb-4">Lịch sử rút tiền</h1>
        <div className="flex items-center justify-between">
          <div>
            Search
          </div>
          <div>
          <Button type="primary" onClick={showModal}>
            Tạo yêu cầu rút tiền
          </Button>
          </div>
        </div>
        <Table<DataType>
          columns={columns}
          dataSource={data}
          pagination={{
            current: pagination.current,
            pageSize: pagination.pageSize,
            total: pagination.total,
            showSizeChanger: true,
          }}
          onChange={handleTableChange}
        />
        <Modal title="Tạo yêu cầu rút tiền" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
          <p>Some contents...</p>
          <p>Some contents...</p>
          <p>Some contents...</p>
        </Modal>
      </div>
    </section>
  );
}
