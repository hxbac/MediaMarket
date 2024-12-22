'use client';

import orderService from "@/services/orderService";
import Link from "next/link";
import { useEffect } from "react";
import { toast } from "react-toastify";

export default function Page({
  searchParams,
}: {
  searchParams: { [key: string]: string | string[] | undefined };
}) {
  const sessionId = searchParams.session_id;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await orderService.fulfillStripePayment({
          sessionId: sessionId?.toString()
        });
        console.log(response);
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        // toast.error(errorMessage);
      }
    };

    fetchData();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <section className="bg-white pt-16">
      <div className="flex justify-center py-10 items-center max-w-screen-xl mx-auto lg:gap-16 xl:gap-24">
        <div className="max-w-96" style={{ boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px' }}>
          <div className="bg-white p-6  md:mx-auto">
            <svg
              viewBox="0 0 24 24"
              className="text-green-600 w-16 h-16 mx-auto my-6"
            >
              <path
                fill="currentColor"
                d="M12,0A12,12,0,1,0,24,12,12.014,12.014,0,0,0,12,0Zm6.927,8.2-6.845,9.289a1.011,1.011,0,0,1-1.43.188L5.764,13.769a1,1,0,1,1,1.25-1.562l4.076,3.261,6.227-8.451A1,1,0,1,1,18.927,8.2Z"
              ></path>
            </svg>
            <div className="text-center">
              <h3 className="md:text-2xl text-base text-gray-900 font-semibold text-center capitalize">
                Thanh toán thành công!
              </h3>
              <p className="text-gray-600 my-2">
                Thank you for completing your secure online payment.
              </p>
              <p> Have a great day! </p>
              <div className="py-10 text-center">
                <Link
                  href="/dashboard/purchases"
                  className="px-12 bg-indigo-600 hover:bg-indigo-500 text-white font-semibold py-3 capitalize rounded-lg"
                >
                  Xem lịch sử mua hàng
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
