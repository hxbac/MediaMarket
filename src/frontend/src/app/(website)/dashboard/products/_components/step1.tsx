"use client";

import { ProductTypeInterface } from "@/interfaces/products";
import ProductType from "./productType";
import { ProductType as ProductTypeEnum } from "@/enums/ProductType";
import { useProductContext } from "../_context/ProductContext";

const productTypes : ProductTypeInterface[] = [
  {
    type: ProductTypeEnum.Video,
    title: 'Video',
    description: 'Sản phẩm truyền tải nội dung trực quan và sống động, bao gồm các định dạng như phim, clip, hoặc video quảng cáo, giúp bạn tiếp cận khán giả một cách ấn tượng và hiệu quả.'
  },
  {
    type: ProductTypeEnum.Image,
    title: 'Hình ảnh',
    description: 'Bộ sưu tập hình ảnh chất lượng cao, được sắp xếp theo chủ đề hoặc câu chuyện, mang đến trải nghiệm hình ảnh ấn tượng và ghi dấu những khoảnh khắc đáng nhớ.'
  }
];

export default function Step1() {
  const { value } = useProductContext();

  return (
    <div className="">
      <div className="mb-4">
        <h2 className="text-2xl font-bold mt-8 mb-4">Loại sản phẩm</h2>
        <div className="grid grid-cols-2 gap-6">
          {
            productTypes.map(item => <ProductType key={item.type} data={item} isSelected={value.type === item.type} />)
          }
        </div>
      </div>
    </div>
  );
}
