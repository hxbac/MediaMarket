"use client";

import Image from "next/image";

export default function Banner() {
  return (
    <section className="bg-white">
      <div className="grid max-w-screen-xl px-4 pt-20 pb-8 mx-auto lg:gap-8 xl:gap-0 lg:py-16 lg:grid-cols-12 lg:pt-28">
        <div className="mr-auto place-self-center lg:col-span-7">
          <h1 className="max-w-2xl mb-4 text-4xl font-extrabold leading-none tracking-tight md:text-5xl xl:text-6xl">
            Xây dựng nền tảng <br />
            sản phẩm &amp; thương hiệu.
          </h1>
          <p className="max-w-2xl mb-6 font-light text-gray-500 lg:mb-8 md:text-lg lg:text-xl">
            Khám phá cách biến ý tưởng thành nội dung đột phá, nâng tầm giá trị sản phẩm và kết nối thương hiệu của bạn với khách hàng toàn cầu!
          </p>
          <div className="space-y-4 sm:flex sm:space-y-0 sm:space-x-4">
            <a
              href="https://github.com/themesberg/landwind"
              className="inline-flex items-center justify-center w-full px-5 py-3 text-sm font-medium text-center text-gray-900 border border-gray-200 rounded-lg sm:w-auto hover:bg-gray-100 focus:ring-4 focus:ring-gray-100"
            >
              Khám phá
            </a>
          </div>
        </div>
        <div className="hidden lg:mt-0 lg:col-span-5 lg:flex">
          <Image src="https://static.sellfy.com/assets/images/public/1720620680.hero.png" alt="hero image" className="w-full" width={100} height={100} />
        </div>
      </div>
    </section>
  );
}
