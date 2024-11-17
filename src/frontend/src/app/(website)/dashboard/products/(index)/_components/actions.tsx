'use client';

import { Button, Input } from "antd";
import Link from "next/link";
import { useSearchProductContext } from "../../_context/SearchProductContext";
import { useEffect, useState } from "react";
import { useDebounce } from "@/hooks/debounce";

export default function Actions() {
  const { value, setValue } = useSearchProductContext();
  const [searchTerm, setSearchTerm] = useState('');
  const debouncedTerm = useDebounce(searchTerm, 500);

  useEffect(() => {
    setValue({ ...value, name: debouncedTerm });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [debouncedTerm]);

  return (
    <div className="flex items-center justify-center gap-x-4">
      <Input placeholder="Tìm kiếm" value={searchTerm} onChange={(e) => setSearchTerm(e.target.value)} />
      <Link href='/dashboard/products/create'><Button type="primary">Tạo mới</Button></Link>
    </div>
  );
}
