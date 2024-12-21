'use client';

import { Button, Input, Select } from "antd";
import Link from "next/link";
import { useSearchProductContext } from "../../_context/SearchProductContext";
import { useEffect, useState } from "react";
import { useDebounce } from "@/hooks/debounce";
import { ProductType } from "@/enums/ProductType";

export default function Actions() {
  const { value, setValue } = useSearchProductContext();
  const [searchTerm, setSearchTerm] = useState('');
  const debouncedTerm = useDebounce(searchTerm, 500);

  useEffect(() => {
    setValue({ ...value, name: debouncedTerm });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [debouncedTerm]);

  return (
    <div className="flex items-center justify-between my-4 gap-x-4">
      <div className="flex gap-x-4">
        <Input placeholder="Tìm kiếm" value={searchTerm} onChange={(e) => setSearchTerm(e.target.value)} />
        <Select
          defaultValue=""
          style={{ width: 200 }}
          onChange={(e) =>
            setValue({
              ...value,
              productType: e ? Number(e) : null
            })
          }
          options={[
            { value: null, label: 'Tất cả' },
            { value: ProductType.Video, label: 'Video' },
            { value: ProductType.Image, label: 'Hình ảnh' },
          ]}
        />
      </div>
      <Link href='/dashboard/products/create'><Button type="primary">Tạo mới</Button></Link>
    </div>
  );
}
