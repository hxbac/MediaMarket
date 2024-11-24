'use client';

import Link from "next/link";
import { useEffect, useState } from "react";
import searchService from "@/services/searchService";
import { ProductType } from "@/enums/ProductType";
import { toast } from "react-toastify";
import Card from "@/components/product/card";
import { ProductCard } from "@/interfaces/products";

export default function Page({
  searchParams,
}: {
  searchParams: { [key: string]: string | string[] | undefined };
}) {
  const query = searchParams.query;
  const [searchVideo, setSearchVideo] = useState<ProductCard[]>([]);
  const [searchImage, setSearchImage] = useState<ProductCard[]>([]);

  useEffect(() => {
    const searchProductVideo = async () => {
      try {
        const result = await searchService.searchProduct({
          search: query,
          type: ProductType.Video
        });
        if (result.succeeded) {
          setSearchVideo(result.data);
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        toast.error(errorMessage);
      }
    };

    const searchProductImage = async () => {
      try {
        const result = await searchService.searchProduct({
          search: query,
          type: ProductType.Image
        });
        if (result.succeeded) {
          setSearchImage(result.data);
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        toast.error(errorMessage);
      }
    };

    searchProductVideo();
    searchProductImage();
  }, []);

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 mx-auto py-8">
        <h1 className="text-6xl font-bold">{query}</h1>
        <div>
          <div className="flex items-end justify-between">
            <h2 className="text-6xl font-bold">Video</h2>
            <Link href={`/search/video?query=${query}`} className="font-bold text-purple-600">Xem tất cả video</Link>
          </div>
          <p>Learn skills, tools, and techniques from industry experts and creative pros.</p>
          <div className="grid grid-cols-4 gap-x-4 gap-y-6">
            {
              searchVideo.map(item => <Card key={item.id} data={item} />)
            }
          </div>
        </div>
      </div>
    </section>
  );
}
