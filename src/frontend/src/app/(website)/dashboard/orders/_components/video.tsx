'use client';

import { ProductType } from "@/enums/ProductType";
import { Button, Space, Table, TablePaginationConfig, TableProps, Tag } from "antd";
import Image from "next/image";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import Link from "next/link";
import { formatPrice } from "@/utils/helpers";
import { useSearchProductContext } from "../../products/_context/SearchProductContext";
import orderService from "@/services/orderService";

interface DataType {
  key: string;
  slug: string;
  thumbnail: number;
  name: string;
  price: number;
  categories: string[];
}

export default function Video() {
  const [data, setData] = useState([]);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 2,
    total: 0,
  });
  const { value } = useSearchProductContext();

  const fetchData = async (page = 1, pageSize = 10) => {
    try {
      const response = await orderService.getMyOrders({
        params: {
          page,
          pageSize,
          productType: ProductType.Video,
          name: value.name
        },
      });

      setData(response.data);
      setPagination({
        current: page,
        pageSize,
        total: response.totalCount,
      });
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
      toast.error(errorMessage);
    }
  };

  useEffect(() => {
    fetchData(pagination.current, pagination.pageSize);
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [value]);

  const handleTableChange = (pagination: TablePaginationConfig) => {
    fetchData(pagination.current, pagination.pageSize);
  };

  const columns: TableProps<DataType>["columns"] = [
    {
      title: "Hình ảnh",
      dataIndex: "thumbnail",
      key: "thumbnail",
      render: (text) => <Image src={text} width={100} height={100} alt={text} unoptimized />,
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
      render: (price) => formatPrice(price)
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
      title: "Hành Động",
      key: "action",
      render: (_, record) => (
        <Space size="middle">
          <Link href={`/products/${record.slug}`} target="_blank">
            <Button>Xem</Button>
          </Link>
          <Link href={`/products/${record.key}`}>
            <Button type="primary" danger>Xóa</Button>
          </Link>
        </Space>
      ),
    },
  ];

  return (
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
  );
}
