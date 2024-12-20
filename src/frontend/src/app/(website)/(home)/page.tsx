'use client';

import { toast } from 'react-toastify';
import categoryService from '@/services/categoryService.js';
import Banner from "./_components/banner";
import Slider from "./_components/slider";
import Stats from "./_components/stats";
import Products from "./_components/products";
import Sellers from "./_components/sellers";
import Slogan from "./_components/slogan";
import FAQ from "./_components/faq";
import { CategoryHomePage } from "@/interfaces/categories";
import { useEffect, useState } from "react";

export default function Home() {
  const [categories, setCategories] = useState<CategoryHomePage[]|null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await categoryService.getAll();
        if (result.succeeded) {
          setCategories(result.data);
        } else {
          throw new Error(result.message);
        }
      } catch (error: unknown) {
        const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
        toast.error(errorMessage);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      <Banner />
      { categories ? <Slider data={categories} /> : <></> }
      <Stats />
      { categories ? <Products categories={categories} /> : <></> }
      {/* <Sellers /> */}
      <Slogan />
      {/* <FAQ /> */}
    </div>
  )
};
