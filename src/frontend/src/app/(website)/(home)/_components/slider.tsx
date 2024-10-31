"use client";

import { useEffect, useState } from "react";
import SlickSlider from "react-slick";
import Image from 'next/image';
import { toast } from 'react-toastify';
import categoryService from '@/services/categoryService.js';

interface Item {
  id: string;
  name: string;
  image: string;
  description: string;
}

export default function Slider() {
  const [data, setData] = useState<Item[]|null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await categoryService.getAll();
        if (result.succeeded) {
          setData(result.data);
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

  const settings = {
    dots: false,
    infinite: true,
    slidesToShow: 4,
    autoplay: true,
    speed: 6000,
    autoplaySpeed: 0,
    cssEase: "linear",
    swipe: false,
    draggable: false,
  };

  return (
    data ? 
      <SlickSlider {...settings}>
        {data.map((item, index) => {
          return (
            <div key={index} className="text-white py-12 w-80 h-[400px]">
              <a href="#" className="block w-full h-full relative">
                <Image
                  src={item.image}
                  alt={item.name}
                  width={320}
                  height={400}
                  className="w-full h-auto"
                />
                <h3 className="absolute w-full top-0 left-0 px-6 py-12 text-white text-2xl font-bold">{item.name}</h3>
              </a>
            </div>
          );
        })}
      </SlickSlider>
      : <h1>Loading</h1>
  );
}
