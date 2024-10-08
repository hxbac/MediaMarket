"use client";

import Card from "@/components/user/card";

export default function Sellers() {
  return (
    <section className="bg-white">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:py-24 lg:px-6">
        <div>
          <h1 className="text-5xl text-center font-bold mb-8">
            Learn from Creative Experts
          </h1>
          <p className="text-center">
            Skillshare classes are taught by industry leaders excited to share their tools, techniques, and professional journeys with you.
          </p>
          <div className="grid grid-cols-4 gap-4 mt-12">
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
