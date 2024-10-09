"use client";

import Image from 'next/image';
import report from '@/resources/icons/report.svg';

export default function Prices() {
  return (
    <div className="p-6 border border-gray-200 rounded-xl w-80">
      <div className="rounded-xl border-2 border-purple-600 p-4">
        <div className="flex items-center mb-4">
          <input
            id="default-radio-1"
            type="radio"
            name="default-radio"
            className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500"
          />
          <label
            htmlFor="default-radio-1"
            className="ms-2 text-sm font-medium text-gray-900 flex-1 cursor-pointer select-none"
          >
            Default radio
          </label>
        </div>
        <div className="flex items-center mb-4">
          <input
            id="default-radio-2"
            type="radio"
            name="default-radio"
            className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300"
          />
          <label
            htmlFor="default-radio-2"
            className="ms-2 text-sm font-medium text-gray-900 flex-1 cursor-pointer select-none"
          >
            Checked state
          </label>
        </div>
        <div className="flex items-center justify-between font-bold mb-4">
          <span>Giảm giá</span>
          <span>25%</span>
        </div>
        <div>
          <div className="flex items-center justify-between font-bold">
            <span>Giảm giá</span>
            <span>25%</span>
          </div>
          <p className="text-end text-sm mt-1 mb-4">
            Sales end on Jan 23, 2025
          </p>
        </div>
        <div className="flex items-center justify-between font-bold mb-4">
          <span>Giảm giá</span>
          <span>25%</span>
        </div>
      </div>
      <div className="bg-purple-600 mt-4 text-white font-bold flex items-center justify-center h-11 rounded cursor-pointer hover:opacity-80">Mua</div>
      <div className='flex items-center justify-center select-none cursor-pointer mt-4'>
        <svg width={24} height={24} x="0" y="0" viewBox="0 0 24 24" xmlSpace="preserve"><path d="M6.2 19H5v1h3.5v-1H7.3v-5.8h5.9V5h-7z"></path><path d="M16.7 10.2l2.1-4h-4.5v8.1H19z"></path></svg>
        Báo cáo sản phẩm
      </div>
    </div>
  );
}
