"use client";

import {
  Button,
  Input,
  Modal,
  Table,
  TablePaginationConfig,
  TableProps,
} from "antd";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { formatDatetime, formatPrice, formatShowDotPrice } from "@/utils/helpers";
import { CardElement, useStripe, useElements } from "@stripe/react-stripe-js";
import withdrawalService from "@/services/withdrawalService";
import WithdrawalStatusTag from "@/components/withdrawal/withdrawalStatus";
import userService from "@/services/userService";

interface DataType {
  id: string;
  amount: number;
  withdrawalStatus: number;
  note: string | null;
  processedAt: string | null;
  createdOn: string | null;
}

export default function Page() {
  const stripe = useStripe();
  const elements = useElements();
  const [withdrawalAmount, setWithdrawalAmount] = useState(0);
  const [data, setData] = useState([]);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentBalance, setCurrentBalance] = useState(0);

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = async () => {
    if (!stripe || !elements) {
      toast.error("Stripe has not loaded yet.");
      return;
    }

    const card = elements.getElement(CardElement);
    if (!card) {
      toast.error("Card error");
      return;
    }

    const { token } = await stripe.createToken(card, { 'currency': 'USD' });

    const response = await withdrawalService.createRequest({
      CardToken: token?.id,
      amount: withdrawalAmount
    });

    if (response.succeeded) {
      toast.success('Tạo yêu cầu rút tiền thành công');
      fetchData(1, pagination.pageSize);
      setIsModalOpen(false);
    } else {
      toast.error(response.errors.join(', '));
    }
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const fetchData = async (page = 1, pageSize = 10) => {
    try {
      const response = await withdrawalService.getMyRequests({
        params: {
          page,
          pageSize
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

  const refreshBalance = async () => {
    try {
      const response = await userService.getMyCurrentBalance();
      setCurrentBalance(response.data.balance);
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
      toast.error(errorMessage);
    }
  }

  useEffect(() => {
    fetchData(pagination.current, pagination.pageSize);
    refreshBalance();
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
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <h1 className="text-center uppercase text-xl font-bold mb-4">Lịch sử rút tiền</h1>
        <div className="flex items-center justify-between mb-4">
          <div>
            Số dư ví: {formatPrice(currentBalance)}
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
          <div className="py-4">
            <h2 className="text-sm font-bold mb-4">Số dư hiện tại: {formatPrice(currentBalance)}</h2>
            <div className="mb-4">
              <p className="text-sm font-semibold mb-2">Nhập số tiền</p>
              <Input type="text" value={formatShowDotPrice(withdrawalAmount)} onChange={e => setWithdrawalAmount(Number(e.target.value.replace(/\D/g, "")))} />
            </div>
            <div className="mb-4">
              <p className="text-sm font-semibold mb-4">Thông tin thẻ</p>
              <CardElement options={{ hidePostalCode: true }} />
            </div>
          </div>
        </Modal>
      </div>
    </section>
  );
}
