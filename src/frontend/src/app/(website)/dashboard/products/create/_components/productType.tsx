"use client";

export default function ProductType() {
  return (
    <div className="border-2 hover:border-purple-600 p-5 cursor-pointer rounded-md">
      <div className="w-8 h-8 rounded-full bg-purple-600 mb-2"></div>
      <h3 className="font-bold text-base mb-1">Sách</h3>
      <p className="text-sm">Loại sách sẽ tạo</p>
    </div>
  );
}
