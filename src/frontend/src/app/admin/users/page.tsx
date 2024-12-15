"use client";

import userService from "@/services/admin/userService";
import { formatPrice } from "@/utils/helpers";
import { Table, TablePaginationConfig, TableProps, Tag } from "antd";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface DataType {
  id: string;
  name: string;
  userName: string;
  email: string;
  numberOrder: number;
  numberProduct: number;
  lockoutEnabled: boolean;
}

export default function Page() {
  const [data, setData] = useState([]);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 2,
    total: 0,
  });

  const fetchData = async (page = 1, pageSize = 10) => {
    try {
      const response = await userService.getListUser({
        params: {
          page,
          pageSize,
        },
      });

      setData(response.data);
      setPagination({
        current: page,
        pageSize,
        total: response.totalCount,
      });
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
      title: "Tên người dùng",
      dataIndex: "name",
      key: "name",
      render: (name) => name,
    },
    {
      title: "Tài khoản",
      dataIndex: "userName",
      key: "userName",
      render: (userName) => userName,
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
      render: (email) => email,
    },
    {
      title: "Số dư ví",
      dataIndex: "balance",
      key: "balance",
      render: (balance) => formatPrice(balance),
    },
    {
      title: "Số đơn hàng đã đặt",
      dataIndex: "numberOrder",
      key: "numberOrder",
      render: (numberOrder) => numberOrder,
    },
    {
      title: "Số sản phẩm đã tạo",
      dataIndex: "numberProduct",
      key: "numberProduct",
      render: (numberProduct) => numberProduct,
    },
    {
      title: "Trạng thái",
      dataIndex: "abc",
      key: "abc",
      render: () => <Tag color="#87d068">Đang hoạt động</Tag>,
    },
  ];

  return (
    <div className="min-h-[80vh] pr-6">
      <h1 className="capitalize text-xl font-bold mb-4 border-b py-4">
        Danh sách người dùng
      </h1>
      <div className="flex items-center justify-between mb-4">
        <div>Search</div>
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
    </div>
  );
}
