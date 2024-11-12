import TagRounded from "@/components/tag/tagRounded";
import Prices from "./_components/prices";
import CardHorizontal from "@/components/user/cardHorizontal";
import productService from "@/services/productService";
import { Tag } from "@/interfaces/tag";


interface ProductParams {
  slug: string;
}

export default async function Page({ params }: { params: ProductParams }) {
  const { slug } = params;

  const response = await productService.getDetail(slug);
  const data = response.data;

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <div className="w-full min-h-60">

        </div>
        <div className="flex gap-52 items-start">
          <div className="flex-1">
            <div>
              <h1 className="text-5xl font-bold mb-8">{data.name}</h1>
              <p className="mb-8 text-sm font-semibold">{data.shortDescription}</p>
            </div>
            <CardHorizontal />
            <div>
              <h2 className="text-2xl font-bold mt-8 mb-4">Mô tả</h2>
              <div className="">
                <p dangerouslySetInnerHTML={{ __html: data.description }}></p>
              </div>
            </div>
            <div>
              <h2 className="text-2xl font-bold mt-8 mb-4">Tags</h2>
              <div className="flex gap-4">
                {
                  data.tags.map((tag: Tag) => (
                    <TagRounded key={tag.id}>
                      {tag.name}
                    </TagRounded>
                  ))
                }
              </div>
            </div>
          </div>
          <Prices />
        </div>
      </div>
    </section>
  );
}