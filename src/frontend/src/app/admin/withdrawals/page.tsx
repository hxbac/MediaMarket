"use client";

import WithdrawalStatusTag from "@/components/withdrawal/withdrawalStatus";
import withdrawalService from "@/services/admin/withdrawalService";
import { formatDatetime, formatPrice } from "@/utils/helpers";
import { Button, Input, Modal, Select, Table, TablePaginationConfig, TableProps } from "antd";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { DatePicker } from 'antd';
import { WithdrawalStatus } from "@/enums/WithdrawalStatus";
import TextArea from "antd/es/input/TextArea";

const { RangePicker } = DatePicker;

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
    pageSize: 10,
    total: 0,
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [dataShowModal, setDataShowModal] = useState<DataType>({
    id: '',
    amount: 0,
    withdrawalStatus: WithdrawalStatus.Pending,
    note: null,
    processedAt: null,
    createdOn: null,
    userCreated: '',
    nameOfUser: '',
  });
  const [withdrawalApprovalStatus, setWithdrawalApprovalStatus] = useState(WithdrawalStatus.Completed);
  const [withdrawalApprovalNote, setWithdrawalApprovalNote] = useState('');

  const showModal = () => {
    setWithdrawalApprovalStatus(WithdrawalStatus.Completed);
    setWithdrawalApprovalNote('');
    setIsModalOpen(true);
  };

  const handleOk = () => {
    if (withdrawalApprovalNote.trim() === '') {
      toast.error('Vui lòng điền ghi chú!');
      return;
    }
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

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
    },
    {
      title: "Hành động",
      dataIndex: "id",
      key: "action",
      render: (_, item) => {
        setDataShowModal(item);
        return <div><Button type="primary" onClick={showModal}>Xem</Button></div>;
      },
    }
  ];

  return (
    <div className="min-h-[80vh] pr-6">
      <h1 className="capitalize text-xl font-bold mb-4 border-b py-4">
        Danh sách yêu cầu rút tiền
      </h1>
      <div className="flex items-center justify-between mb-4">
        <div className="flex gap-x-4">
          <RangePicker style={{ flexShrink: 0 }} />
          <Input placeholder="Tên người yêu cầu" />
          <Select
            defaultValue="waiting"
            style={{ width: 'auto' }}
            options={[
              { value: 'waiting', label: 'Chờ xử lý' },
              { value: 'success', label: 'Thành công' },
              { value: 'failed', label: 'Đã hủy' },
            ]}
          />
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
      <Modal title="Yêu cầu rút tiền" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
        <div className="mb-4 mt-4">
          <h4 className="text-base font-semibold p-2 bg-gray-200 mb-2">Thông tin người gửi yêu cầu</h4>
          <div className="px-2 py-1">
            <p className="mb-1.5">Họ tên: <b>{dataShowModal.nameOfUser}</b></p>
            <p className="mb-1.5">Số tiền: <b>{formatPrice(dataShowModal.amount)}</b></p>
            <p className="mb-1.5">Trạng thái: <WithdrawalStatusTag status={dataShowModal.withdrawalStatus} /></p>
          </div>
        </div>
        {
          dataShowModal.withdrawalStatus == WithdrawalStatus.Pending ?
          (
            <div className="">
              <h4 className="text-base font-semibold p-2 bg-gray-200 mb-2">Hành động</h4>
              <div className="px-2 py-1">
                <label htmlFor="" className="mb-1 block">Xác nhận</label>
                <Select
                  defaultValue={WithdrawalStatus.Completed}
                  style={{ width: '100%' }}
                  value={withdrawalApprovalStatus}
                  onChange={(value) => setWithdrawalApprovalStatus(value)}
                  options={[
                    { value: WithdrawalStatus.Completed, label: 'Chấp nhận' },
                    { value: WithdrawalStatus.Failed, label: 'Từ chối' },
                  ]}
                />
                <label htmlFor="" className="mt-2 mb-1 block">Ghi chú</label>
                <TextArea value={withdrawalApprovalNote} onChange={(e) => setWithdrawalApprovalNote(e.target.value)} style={{ height: 80, resize: 'none' }} rows={4} />
              </div>
            </div>
          ) :
          (
            <></>
          )
        }
      </Modal>
    </div>
  );
}
