"use client";

import dynamic from "next/dynamic";

import 'react-quill/dist/quill.snow.css';
import { useMemo } from 'react';

export default function CustomEditor({ value, onChange }: { value: string, onChange: (value: string) => void }) {
  const ReactQuill = useMemo(() => dynamic(() => import('react-quill'), { ssr: false }),[]);

  return (
    <div>
      <ReactQuill theme="snow" value={value} style={{ height: '300px' }} onChange={onChange} />
    </div>
  );
}
