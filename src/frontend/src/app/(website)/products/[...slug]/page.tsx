import TagRounded from "@/components/tag/tagRounded";
import Prices from "./_components/prices";
import CardHorizontal from "@/components/user/cardHorizontal";
import productService from "@/services/productService";
import { Tag } from "@/interfaces/tag";
import Link from "next/link";
import { formatPrice } from "@/utils/helpers";

interface ProductParams {
  slug: string;
}

export default async function Page({ params }: { params: ProductParams }) {
  const { slug } = params;

  const response = await productService.getDetail(slug);
  const data = response.data;

  console.log(data);

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <div className="w-full min-h-60"></div>
        <div className="flex gap-52 items-start">
          <div className="flex-1">
            <div>
              <h1 className="text-5xl font-bold mb-8">{data.name}</h1>
              <p className="mb-8 text-sm font-semibold">
                {data.shortDescription}
              </p>
            </div>
            <CardHorizontal />
            <div>
              <h2 className="text-2xl font-bold mt-8 mb-4">Mô tả</h2>
              <div className="">
                <p dangerouslySetInnerHTML={{ __html: data.description }}></p>
              </div>
            </div>
            <div>
              <h2 className="text-2xl font-bold mt-8 mb-4">Tags</h2>
              <div className="flex gap-4">
                {data.tags.map((tag: Tag) => (
                  <TagRounded key={tag.id}>{tag.name}</TagRounded>
                ))}
              </div>
            </div>
          </div>
          <div className="p-6 border border-gray-200 rounded-xl w-80">
            <div className="rounded-xl border-2 border-purple-600 p-4">
              <div>
                <h1>{data.name}</h1>
                <p>{formatPrice(data.price)}</p>
              </div>
              {/* <div className="flex items-center justify-between font-bold mb-4">
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
              </div> */}
            </div>
            <Link
              href={`/checkout/${data.slug}`}
              className="bg-purple-600 mt-4 text-white font-bold flex items-center justify-center h-11 rounded cursor-pointer hover:opacity-80"
            >
              Mua
            </Link>
            <div className="flex items-center justify-center select-none cursor-pointer mt-4">
              <svg
                width={24}
                height={24}
                x="0"
                y="0"
                viewBox="0 0 24 24"
                xmlSpace="preserve"
              >
                <path d="M6.2 19H5v1h3.5v-1H7.3v-5.8h5.9V5h-7z"></path>
                <path d="M16.7 10.2l2.1-4h-4.5v8.1H19z"></path>
              </svg>
              Báo cáo sản phẩm
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
