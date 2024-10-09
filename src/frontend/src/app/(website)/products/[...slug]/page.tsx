import TagRounded from "@/components/tag/tagRounded";
import Prices from "./_components/prices";
import CardHorizontal from "@/components/user/cardHorizontal"; 

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
        <div className="flex gap-52 items-start">
          <div className="flex-1">
            <div>
              <h1 className="text-5xl font-bold mb-8">Invest in Dubai - one of the hottest real estate markets in the world.</h1>
              <p className="mb-8 text-sm font-semibold">With Dubai’s rapidly growing market and its strong position as a global hub, now is an excellent time to enhance your investment portfolio.</p>
            </div>
            <CardHorizontal />
            <div>
              <h2 className="text-2xl font-bold mt-8 mb-4">About this event</h2>
              <div className="">
                <p>Invest in Dubai, one of the hottest real estate markets in the world. If you&apos;re looking to dive into the world of real estate investment, specifically in Dubai, this online event is perfect for you! Dubais market is booming, and theres no better time to get in on the action.</p>
              </div>
            </div>
            <div>
              <h2 className="text-2xl font-bold mt-8 mb-4">Tags</h2>
              <div className="flex gap-4">
                <TagRounded>
                  text 1
                </TagRounded>
                <TagRounded>
                  text 1
                </TagRounded>
                <TagRounded>
                  text 1
                </TagRounded>
                <TagRounded>
                  text 1
                </TagRounded>
              </div>
            </div>
          </div>
          <Prices />
        </div>
      </div>
    </section>
  );
}