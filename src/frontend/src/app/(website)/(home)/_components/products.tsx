"use client";

import Card from "@/components/product/card";

export default function Products() {
  const categories = [
    {
      name: "Featured",
    },
    {
      name: "Music",
    },
    {
      name: "Drawing & Painting",
    },
    {
      name: "Marketing",
    },
    {
      name: "Animation",
    },
    {
      name: "Social Media",
    },
    {
      name: "UI/UX design",
    },
    {
      name: "Creative Writing",
    },
    {
      name: "Featured",
    },
    {
      name: "Music",
    },
    {
      name: "Drawing & Painting",
    },
    {
      name: "Marketing",
    },
    {
      name: "Animation",
    },
    {
      name: "Social Media",
    },
    {
      name: "UI/UX design",
    },
    {
      name: "Creative Writing",
    },
  ];
  return (
    <section className="bg-white">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:py-24 lg:px-6">
        <div>
          <h1 className="text-5xl text-center font-bold mb-8">
            Explore Inspiring Online Courses
          </h1>
          <div className="flex flex-wrap justify-center gap-x-2 gap-y-1">
            {categories.map((item, index) => {
              return (
                <div
                  key={index}
                  className="py-1 px-4 font-bold whitespace-nowrap transition-all border border-gray-700 rounded-[100px] hover:bg-purple-600 hover:border-purple-600 hover:text-white cursor-pointer"
                >
                  {item.name}
                </div>
              );
            })}
          </div>
          <div className="mt-12 grid grid-cols-4 gap-4">
            <Card />
            <Card />
            <Card />
            <Card />
            <Card />
            <Card />
            <Card />
            <Card />
          </div>
        </div>
      </div>
    </section>
  );
}
