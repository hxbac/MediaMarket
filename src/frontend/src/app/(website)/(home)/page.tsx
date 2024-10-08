import Banner from "./_components/banner";
import Slider from "./_components/slider";
import Stats from "./_components/stats";
import Products from "./_components/products";
import Sellers from "./_components/sellers";
import Slogan from "./_components/slogan";
import FAQ from "./_components/faq";

export default function Home() {
  return (
    <div>
      <Banner />
      <Slider />
      <Stats />
      <Products />
      <Sellers />
      <Slogan />
      <FAQ />
    </div>
  )
};
