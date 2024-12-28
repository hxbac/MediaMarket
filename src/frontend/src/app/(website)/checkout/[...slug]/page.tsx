import productService from "@/services/productService";
import OrderSummary from "./_components/summary";
import Image from "next/image";
import { formatPrice } from "@/utils/helpers";
import Link from "next/link";
import { DiscountItem, EventDiscountItem, ProductCheckoutInfo } from "@/interfaces/products";
import { DiscountType } from "@/enums/DiscountType";

interface ProductParams {
  slug: string;
}

export default async function Page({ params }: { params: ProductParams }) {
  const { slug } = params;

  const response = await productService.getCheckoutInfo({ slug: slug.toString() });
  const data: ProductCheckoutInfo = response.data;

  const originalPrice = data.price;
  const sellerPrice = originalPrice * 70 / 100;
  const adminPrice = originalPrice - sellerPrice;

  let priceShow: number = originalPrice;
  if (data.productDiscounts !== null && data.productDiscounts.length > 0) {
    data.productDiscounts.forEach((item: DiscountItem) => {
      if (item.type === DiscountType.Fixed) {
        priceShow -= item.value;
      } else {
        priceShow -= (item.value * sellerPrice / 100);
      }
    });
  }

  if (data.eventDiscounts !== null && data.eventDiscounts.length > 0) {
    data.eventDiscounts.forEach((item: EventDiscountItem) => {
      if (item.type === DiscountType.Fixed) {
        priceShow -= item.value;
      } else {
        priceShow -= (item.value * adminPrice / 100);
      }
    });
  }

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl mx-auto lg:gap-16 xl:gap-24">
        <section className=" relative z-10 after:contents-[''] after:absolute after:z-0 after:h-full xl:after:w-1/3 after:top-0 after:right-0 after:bg-gray-50">
          <div className="w-full max-w-7xl px-4 md:px-5 lg-6 mx-auto relative z-10">
            <div className="grid grid-cols-12">
              <div className="col-span-12 xl:col-span-8 lg:pr-8 pt-14 pb-8 lg:py-24 w-full max-xl:max-w-3xl max-xl:mx-auto">
                <div className="flex items-center justify-between pb-8 border-b border-gray-300">
                  <h2 className="font-manrope font-bold text-xl leading-10 text-black">
                    Sản phẩm
                  </h2>
                </div>
                <div className="flex flex-col min-[500px]:flex-row min-[500px]:items-center gap-5 py-6  border-b border-gray-200 group">
                  <div className="w-full md:max-w-[126px]">
                    <Image
                      src={data.thumbnail}
                      alt={data.name}
                      width={126}
                      height={126}
                      className="w-32 h-32 object-cover rounded-lg"
                      unoptimized
                    />
                  </div>
                  <div className="grid grid-cols-1 md:grid-cols-4 w-full">
                    <div className="md:col-span-2">
                      <div className="flex flex-col max-[500px]:items-center gap-3">
                        <h6 className="font-semibold text-base leading-7 text-black">
                          <Link href={'/products/' + data.slug}>{data.name}</Link>
                        </h6>
                        <h6 className="font-normal text-base leading-7 text-gray-500">
                          {data.shortDescription}
                        </h6>
                      </div>
                    </div>
                    <div className="flex flex-col justify-center items-center max-[500px]:justify-center md:justify-end max-md:mt-3 h-full">
                      {
                        priceShow !== originalPrice ? (
                          <p className="font-bold line-through text-lg leading-8 text-gray-600 text-center transition-all duration-300 group-hover:text-indigo-600">
                            {formatPrice(originalPrice)}
                          </p>
                        ) : (
                          <></>
                        )
                      }
                      <p className="font-bold text-lg leading-8 text-gray-600 text-center transition-all duration-300 group-hover:text-indigo-600">
                        {formatPrice(priceShow)}
                      </p>
                    </div>
                  </div>
                </div>
              </div>
              <OrderSummary product={data}  />
            </div>
          </div>
        </section>
      </div>
    </section>
  );
}
