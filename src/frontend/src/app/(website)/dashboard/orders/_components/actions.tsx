'use client';

import { Input, Select } from "antd";
import { DatePicker } from 'antd';
import dayjs from 'dayjs';
import customParseFormat from 'dayjs/plugin/customParseFormat';
import moment from "moment";

const { RangePicker } = DatePicker;
dayjs.extend(customParseFormat);
const dateFormat = 'YYYY-MM-DD';

export default function Actions() {
  const handleChange = (value: string) => {
    console.log(`selected ${value}`);
  };

  return (
    <div className="flex items-center justify-center gap-x-4">
      <Input placeholder="Tìm kiếm" allowClear style={{ width: 168 }} />
      <Select
        defaultValue="all"
        style={{ width: 120, flexShrink: 0 }}
        onChange={handleChange}
        options={[
          { value: 'all', label: 'Tất cả' },
          { value: 'success', label: 'Thành công' },
          { value: 'waiting', label: 'Đang chờ' },
          { value: 'cancel', label: 'Đã hủy' },
        ]}
      />
      <RangePicker maxDate={dayjs(moment(new Date()).format(dateFormat), dateFormat)} style={{ 
        width: 240
      }} />
    </div>
  );
}
