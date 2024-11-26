"use client";

import Card from "@/components/product/card";
import { MyProductLatest } from "@/interfaces/products";
import productService from "@/services/productService";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

export default function ProductUser({ userId }: { userId: string }) {
  const [products, setProducts] = useState<MyProductLatest[]>([]);

  useEffect(() => {
    const fetchMyLatestProducts = async () => {
      try {
        const result = await productService.getLatestProducts({
          userId
        });
        if (result.succeeded) {
          setProducts(result.data);
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage =
          error instanceof Error
            ? error.message
            : "An unexpected error occurred";
        toast.error(errorMessage);
      }
    };

    fetchMyLatestProducts();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div className="grid gap-8 grid-cols-2 md:grid-cols-3 xl:grid-cols-4 ">
      {
        products.map(item => <Card key={item.id} data={item} />)
      }
    </div>
  );
}
