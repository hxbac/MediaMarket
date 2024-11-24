import { ProductCard } from "@/interfaces/products";
import Image from "next/image";

import { ProductType } from "@/enums/ProductType";
import Link from "next/link";

export default function Card({ data }: { data: ProductCard }) {
  const productTypeTexts = {
    [ProductType.Video.toString()]: 'Video',
    [ProductType.Image.toString()]: 'Hình Ảnh',
    [ProductType.Ebook.toString()]: 'Sách Điện Tử',
  }

  return (
    <div className="relative max-w-sm bg-white border border-gray-200 rounded-lg shadow">
      <Link href={`/products/${data.slug}`} className="absolute inset-0 z-50"></Link>
      {
        data.productType !== null && data.productType !== undefined ?
        (
          <div className="card-top-overlay text-white font-bold pt-3 pl-3 text-lg">{productTypeTexts[data.productType.toString()]}</div>
        ) : ''
      }
      <div>
        <Image
          src={data.thumbnail}
          alt=""
          width={150}
          height={268}
          unoptimized
          className="w-full h-[168px] rounded-t-lg object-cover object-center"
        />
      </div>
      <div className="p-2">
        <div className="flex items-center justify-between mt-2 mb-1">
          <span className="text-xs font-semibold">37,210 students</span>
          <span className="text-xs font-semibold">51m</span>
        </div>
        <h5 className="mb-2 text-base font-bold tracking-tight text-gray-900">
          {data.name}
        </h5>
        <p className="mb-3 font-sm text-gray-700">
          {data.shortDescription}
        </p>
      </div>
    </div>
  );
}
