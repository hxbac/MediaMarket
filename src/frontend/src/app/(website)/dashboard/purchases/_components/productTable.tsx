'use client';

import { ProductType } from "@/enums/ProductType";
import { Button, Space, Table, TablePaginationConfig, TableProps, Tag } from "antd";
import Image from "next/image";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import Link from "next/link";
import { formatDatetime, formatPrice } from "@/utils/helpers";
import { useSearchProductContext } from "../../products/_context/SearchProductContext";
import orderService from "@/services/orderService";
import OrderStatusTag from "@/components/order/orderStatus";
import { OrderStatus } from "@/enums/OrderStatus";

interface DataType {
  key: string;
  slug: string;
  thumbnail: number;
  name: string;
  price: number;
  categories: string[];
  orderStatus: number;
  createdAt: string;
}

export default function ProductTable() {
  const [data, setData] = useState([]);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
  });
  const { value } = useSearchProductContext();

  const fetchData = async (page = 1, pageSize = 10) => {
    try {
      const response = await orderService.getMyPurchases({
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
      title: "Thời gian mua",
      dataIndex: "createdAt",
      key: "createdAt",
      render: (createdAt) => formatDatetime(createdAt)
    },
    {
      title: "Trạng thái",
      dataIndex: "orderStatus",
      key: "orderStatus",
      render: (status) => <OrderStatusTag status={status} />
    },
    {
      title: "Hành Động",
      key: "action",
      render: (_, record) => {
        if (record.orderStatus === OrderStatus.Completed) {
          return (
            <Space size="middle">
              <Link href={`/products/${record.slug}`} target="_blank">
                <Button type="primary">Tải xuống</Button>
              </Link>
            </Space>
          )
        } else {
          return <></>
        }
      },
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
