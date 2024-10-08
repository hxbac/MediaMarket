"use client";

import React from "react";
import SlickSlider from "react-slick";
import Image from 'next/image';

export default function Slider() {
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

  const data = [
    {
      text: "Graphic Design",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/graphic_design.webp",
      url: "",
    },
    {
      text: "Marketing",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/marketing.webp",
      url: "",
    },
    {
      text: "Fine Art",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/fine_art.webp",
      url: "",
    },
    {
      text: "Photography",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/photography.webp",
      url: "",
    },
    {
      text: "Graphic Design",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/graphic_design.webp",
      url: "",
    },
    {
      text: "Marketing",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/marketing.webp",
      url: "",
    },
    {
      text: "Fine Art",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/fine_art.webp",
      url: "",
    },
    {
      text: "Photography",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/photography.webp",
      url: "",
    },
    {
      text: "Graphic Design",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/graphic_design.webp",
      url: "",
    },
    {
      text: "Marketing",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/marketing.webp",
      url: "",
    },
    {
      text: "Fine Art",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/fine_art.webp",
      url: "",
    },
    {
      text: "Photography",
      image: "https://static.skillshare.com/assets/images/loh/category_carousel/320x400/photography.webp",
      url: "",
    }
  ];

  return (
    <SlickSlider {...settings}>
      {data.map((item, index) => {
        return (
          <div key={index} className="text-white py-12 w-80 h-[400px]">
            <a href="#" className="block w-full h-full relative">
              <Image
                src={item.image}
                alt={item.text}
                width={320}
                height={400}
                className="w-full h-auto"
              />
              <h3 className="absolute w-full top-0 left-0 px-6 py-12 text-white text-2xl font-bold">{item.text}</h3>
            </a>
          </div>
        );
      })}
    </SlickSlider>
  );
}
