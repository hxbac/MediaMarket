"use client";

import ProductType from "./productType";

export default function Step1() {
  return (
    <div className="hidden">
      <div className="mb-4">
        <h2 className="text-2xl font-bold mt-8 mb-4">Loại sản phẩm</h2>
        <div className="grid grid-cols-3 gap-6">
          <ProductType />
        </div>
      </div>
      <div className="mb-4">
        <h2 className="text-2xl font-bold mt-8 mb-4">Danh mục sản phẩm</h2>
        <div className="grid grid-cols-3 gap-6">
          <div className="border-2 hover:border-purple-600 p-5 cursor-pointer rounded-md">
            <div className="w-8 h-8 rounded-full bg-purple-600 mb-2"></div>
            <h3 className="font-bold text-base mb-1">Sách</h3>
            <p className="text-sm">Loại sách sẽ tạo</p>
          </div>
          <div className="border-2 hover:border-purple-600 p-5 cursor-pointer rounded-md">
            <div className="w-8 h-8 rounded-full bg-purple-600 mb-2"></div>
            <h3 className="font-bold text-base mb-1">Sách</h3>
            <p className="text-sm">Loại sách sẽ tạo</p>
          </div>
          <div className="border-2 hover:border-purple-600 p-5 cursor-pointer rounded-md">
            <div className="w-8 h-8 rounded-full bg-purple-600 mb-2"></div>
            <h3 className="font-bold text-base mb-1">Sách</h3>
            <p className="text-sm">Loại sách sẽ tạo</p>
          </div>
        </div>
      </div>
    </div>
  );
}
