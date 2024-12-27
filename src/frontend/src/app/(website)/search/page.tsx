'use client';

import Link from "next/link";
import { useEffect, useState } from "react";
import searchService from "@/services/searchService";
import { ProductType } from "@/enums/ProductType";
import { toast } from "react-toastify";
import Card from "@/components/product/card";
import { ProductCard } from "@/interfaces/products";
import { Empty } from "antd";

export default function Page({
  searchParams,
}: {
  searchParams: { [key: string]: string | string[] | undefined };
}) {
  const query = searchParams.query;
  const [searchResult, setsearchResult] = useState<ProductCard[]>([]);

  useEffect(() => {
    const searchProduct = async () => {
      try {
        const result = await searchService.searchProduct({
          search: query
        });
        if (result.succeeded) {
          setsearchResult(result.data);
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        toast.error(errorMessage);
      }
    };

    searchProduct();
  }, [query]);

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 mx-auto py-8">
        <h1 className="text-6xl font-bold mb-4">Tìm kiếm: {query}</h1>
        <div>
          <div className="flex">
            <h2 className="text-6xl font-bold">Sản phẩm</h2>
          </div>
          <p>Danh sách sản phẩm trùng với kết quả tìm kiếm.</p>
          <div className="mt-4">
            {
              searchResult.length > 0 ? (
                <div className="grid grid-cols-4 gap-x-4 gap-y-6">
                  {
                    searchResult.map(item => <Card key={item.id} data={item} />)
                  }
                </div>
              ) : (
                <div className="py-32 border rounded-md">
                  <Empty />
                </div>
              )
            }
          </div>
        </div>
      </div>
    </section>
  );
}
