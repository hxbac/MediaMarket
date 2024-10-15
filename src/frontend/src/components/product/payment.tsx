import { Button, Tag } from "antd";

export default function CardPayment() {
  return (
    <section className="mb-6 relative p-8 border rounded-xl border-gray-300 bg-[#f9fafc]">
      <div className="w-full max-w-7xl mx-auto">
        <div className="">
          <div className="flex max-lg:flex-col items-center gap-8 lg:gap-24">
            <div className="grid grid-cols-4 w-full">
              <div className="col-span-4 sm:col-span-1">
                <img
                  src="https://pagedone.io/asset/uploads/1705474774.png"
                  alt=""
                  className="max-sm:mx-auto object-cover"
                />
              </div>
              <div className="col-span-4 sm:col-span-3 max-sm:mt-4 sm:pl-8 flex flex-col justify-center max-sm:items-center">
                <p className="font-medium leading-8 text-black whitespace-nowrap">
                  Mã đơn hàng: #10234987
                </p>
                <h6 className="font-manrope font-semibold leading-9 text-black whitespace-nowrap">
                  Decoration Flower port
                </h6>
                <p className="font-normal leading-8 text-gray-500 whitespace-nowrap">
                  By: Dust Studios
                </p>
                <div className="flex items-center max-sm:flex-col gap-x-10 gap-y-3">
                  <p className="font-semibold  leading-8 text-black whitespace-nowrap">
                    Price $80.00
                  </p>
                </div>
                <p className="font-medium leading-8 text-black whitespace-nowrap">
                  Thời gian: 18th march 2021
                </p>
              </div>
            </div>
            <div className="flex items-center justify-around w-full sm:pl-28 lg:pl-0">
              <div className="flex flex-col justify-center items-center max-sm:items-center">
                <p className="font-bold text-gray-500 leading-8 mb-2 text-left whitespace-nowrap">
                  Trạng thái
                </p>
                <div>
                  <Tag color="#87d068">Thành công</Tag>
                </div>
                <div className="mt-4 gap-x-4 flex">
                  <Button color="danger" variant="solid" shape="round">Hủy bỏ</Button>
                  <Button type="primary" shape="round">Mua lại</Button>
                </div>
              </div>
              <div className="flex flex-col justify-center items-start max-sm:items-center">
                <p className="font-normal text-gray-500 leading-8 mb-2 text-left whitespace-nowrap">
                  Delivery Expected by
                </p>
                <p className="font-semibold leading-8 text-black text-left whitespace-nowrap">
                  23rd March 2021
                </p>
              </div>
            </div>
          </div>
        </div>
        <hr />
        <div>
          Download
        </div>
      </div>
    </section>
  );
}
