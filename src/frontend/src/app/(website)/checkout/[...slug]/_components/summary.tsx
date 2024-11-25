'use client';

import { ProductCheckoutInfo } from "@/interfaces/products";
import orderService from "@/services/orderService";
import { Button } from "antd";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";

export default function OrderSummary({ product }: { product: ProductCheckoutInfo }) {
  const router = useRouter();

  const processOrder = async () => {
    try {
      const result = await orderService.create({
        productSlug: product.slug,
      });
      if (result.succeeded) {
        router.push(result.data.redirectUrl);
      } else {
        throw new Error(result.message);
      }
    } catch (error: unknown) {
      const errorMessage =
      error instanceof Error ? error.message : "An unexpected error occurred";
      toast.error(errorMessage);
    }
  };

  return (
    <div className=" col-span-12 xl:col-span-4 bg-gray-50 w-full max-xl:px-6 max-w-3xl xl:max-w-lg mx-auto lg:pl-8 py-24">
      <h2 className="font-manrope font-bold text-xl leading-10 text-black pb-8 border-b border-gray-300">
        Thông tin thanh toán
      </h2>
      <div className="mt-8">
        <div>
          <Button type="primary" onClick={() => processOrder()} className="w-full text-center rounded-lg h-12 font-semibold text-lg text-white transition-all duration-500">
            Thanh toán
          </Button>
        </div>
      </div>
    </div>
  );
}
