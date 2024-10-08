import Prices from "./_components/prices";

interface ProductParams {
  slug: string;
}

export default function Page({ params }: { params: ProductParams }) {
  const { slug } = params;
  console.log(slug);
  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <div className="w-full min-h-60">
          
        </div>
        <div className="flex gap-52">
          <div className="flex-1">
            <h1 className="text-5xl font-bold mb-8">Invest in Dubai - one of the hottest real estate markets in the world.</h1>
            <p className="mb-8 text-sm font-semibold">With Dubai’s rapidly growing market and its strong position as a global hub, now is an excellent time to enhance your investment portfolio.</p>
          </div>
          <Prices />
        </div>
      </div>
    </section>
  );
}