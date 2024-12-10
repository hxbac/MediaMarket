"use client";

import Card from "@/components/product/card";
import Category from "@/components/tag/category";
import { CategoryHomePage } from "@/interfaces/categories";
import { useEffect, useState } from "react";

import productService from "@/services/productService.js"
import { ProductCard } from "@/interfaces/products";
import { toast } from "react-toastify";

export default function Products({ categories }: { categories: CategoryHomePage[] }) {
  const [categorySelected, setCategorySelected] = useState<string>(categories[0].id);
  const [products, setProducts] = useState<ProductCard[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await productService.getProductsHomePage({
          params: {
            categoryId: categorySelected
          }
        });
        if (result.succeeded) {
          setProducts(result.data);
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        toast.error(errorMessage);
      }
    };

    fetchData();
  }, [categorySelected]);

  return (
    <section className="bg-white">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:py-24 lg:px-6">
        <div>
          <h1 className="text-5xl text-center font-bold mb-8 capitalize">
          Khám phá các nội dung trực tuyến đầy cảm hứng
          </h1>
          <div className="flex flex-wrap justify-center gap-x-2 gap-y-1">
            {
              categories.map((item) => {
                return (
                  <Category onclick={() => setCategorySelected(item.id)} selected={categorySelected === item.id} key={item.id} >
                    {item.name}
                  </Category>
                );
              })
            }
          </div>
          <div className="mt-12 grid grid-cols-4 gap-4">
            {
              products.map(item => <Card key={item.id} data={item} />)
            }
          </div>
        </div>
      </div>
    </section>
  );
}
