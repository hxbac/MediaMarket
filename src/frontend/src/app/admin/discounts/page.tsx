"use client";

import { DiscountType } from "@/enums/DiscountType";
import eventDiscountService from "@/services/admin/eventDiscountService";
import { formatDatetime, formatPrice } from "@/utils/helpers";
import { Button, DatePicker, Input, Modal, Radio, Table, TablePaginationConfig, TableProps } from "antd";
import moment from "moment";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

interface DataType {
  id: string;
  name: string;
  description: string;
  type: DiscountType;
  value: number;
  startDate: string;
  endDate: string;
}

const { RangePicker } = DatePicker;

export default function Page() {
  const [data, setData] = useState([]);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [discountType, setDiscountType] = useState(DiscountType.Fixed);
  const [name, setName] = useState('');
  const [discountValue, setDiscountValue] = useState(0);
  const [discountMinValue, setDiscountMinValue] = useState(0);
  const [timeDiscount, setTimeDiscount] = useState<string[]>([]);


  const showModal = async () => {
    setIsModalOpen(true);
  };

  const handleOk = async () => {
    if (discountType === DiscountType.Fixed && discountMinValue <= discountValue) {
      toast.error('Khi loại giảm giá là cố định, giá trị đơn hàng tối thiểu không được nhỏ hơn giá trị giảm giá.');
      return;
    }

    try {
      const dataRequest = {
        name,
        type: discountType,
        value: discountValue,
        discountMinValue: discountMinValue,
        startDate: timeDiscount[0],
        endDate: timeDiscount[1]
      };
      await eventDiscountService.create(dataRequest);
      await fetchData();
      setIsModalOpen(false);
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
      toast.error(errorMessage);
    }

  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const fetchData = async (page = 1, pageSize = 10) => {
    try {
      const response = await eventDiscountService.getListDiscount({
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
      title: "Tên",
      dataIndex: "name",
      key: "name",
      render: (name) => name,
    },
    {
      title: "Loại giảm giá",
      dataIndex: "type",
      key: "type",
      render: (type) => type === DiscountType.Fixed ? 'Cố định' : 'Phần trăm',
    },
    {
      title: "Giá trị",
      dataIndex: "value",
      key: "value",
      render: (text, record) => record.type === DiscountType.Fixed ? formatPrice(text) : text + '%',
    },
    {
      title: "Ngày bắt đầu",
      dataIndex: "startDate",
      key: "startDate",
      render: (startDate) => formatDatetime(startDate),
    },
    {
      title: "Ngày kết thúc",
      dataIndex: "endDate",
      key: "endDate",
      render: (endDate) => formatDatetime(endDate),
    },
  ];

  const rowClassName = (record: DataType) => {
    const now = new Date().toISOString();
  return now >= record.startDate && now <= record.endDate ? 'font-bold' : '';
  };

  return (
    <div className="min-h-[80vh] pr-6">
      <h1 className="capitalize text-xl font-bold mb-4 border-b py-4">
        Danh sách giảm giá
      </h1>
      <div className="flex items-center justify-between mb-4">
        <div>Search</div>
        <div>
          <Button type="primary" onClick={showModal}>Tạo mới</Button>
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
        rowClassName={rowClassName}
      />
      <Modal title="Giảm giá" open={isModalOpen} centered onOk={handleOk} onCancel={handleCancel}>
        <h2 className="text-sm font-semibold mt-2 mb-2">Đợt giảm giá</h2>
        <Input className="mb-4" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder="Đợt giảm giá" />
        <h2 className="text-sm font-semibold mt-6 mb-2">Loại giảm giá</h2>
        <Radio.Group className="mb-4" defaultValue="0" value={discountType.toString()} onChange={(e) => setDiscountType(Number(e.target.value))} buttonStyle="solid">
          <Radio.Button value="0">Cố định</Radio.Button>
          <Radio.Button value="1">Phần trăm</Radio.Button>
        </Radio.Group>
        <h2 className="text-sm font-semibold mt-2 mb-2">Giá trị giảm</h2>
        <Input className="mb-4" type="text" value={discountValue.toString()} onChange={(e) => setDiscountValue(Number(e.target.value))} placeholder="Giảm giá" />
        <h2 className="text-sm font-semibold mt-2 mb-2">Giá trị được áp dụng</h2>
        <Input className="mb-4" type="number" value={discountMinValue} onChange={(e) => setDiscountMinValue(Number(e.target.value))} placeholder="Giá trị tối thiểu sẽ áp dụng giảm giá" />
        <h2 className="text-sm font-semibold mt-2 mb-2">Thời gian áp dụng</h2>
        <RangePicker disabledDate={(current) => current && current < moment().startOf('day')} onChange={(e, m) => setTimeDiscount(m)} showTime />
      </Modal>
    </div>
  );
}
