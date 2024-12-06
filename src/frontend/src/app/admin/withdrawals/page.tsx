"use client";

import WithdrawalStatusTag from "@/components/withdrawal/withdrawalStatus";
import withdrawalService from "@/services/admin/withdrawalService";
import { formatDatetime, formatPrice } from "@/utils/helpers";
import { Table, TablePaginationConfig, TableProps } from "antd";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface DataType {
  id: string;
  amount: number;
  withdrawalStatus: number;
  note: string | null;
  processedAt: string | null;
  createdOn: string | null;
  userCreated: string;
  nameOfUser: string;
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
      const response = await withdrawalService.getListWithdrawal({
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
      title: "Mã yêu cầu",
      dataIndex: "id",
      key: "id",
      render: (text) => <b>{text}</b>,
    },
    {
      title: "Người tạo yêu cầu",
      dataIndex: "nameOfUser",
      key: "nameOfUser",
      render: (nameOfUser) => nameOfUser,
    },
    {
      title: "Số tiền",
      dataIndex: "amount",
      key: "amount",
      render: (price) => formatPrice(price),
    },
    {
      title: "Trạng thái",
      dataIndex: "withdrawalStatus",
      key: "withdrawalStatus",
      render: (withdrawalStatus) => <WithdrawalStatusTag status={withdrawalStatus} />,
    },
    {
      title: "Ghi chú",
      dataIndex: "note",
      key: "note",
      render: (note) => note ?? '',
    },
    {
      title: "Xử lý lúc",
      dataIndex: "processedAt",
      key: "processedAt",
      render: (processedAt) => formatDatetime(processedAt),
    },
    {
      title: "Ngày tạo",
      dataIndex: "createdOn",
      key: "createdOn",
      render: (createdOn) => formatDatetime(createdOn),
    }
  ];

  return (
    <div className="min-h-[80vh] pr-6">
      <h1 className="capitalize text-xl font-bold mb-4 border-b py-4">
        Danh sách yêu cầu rút tiền
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
