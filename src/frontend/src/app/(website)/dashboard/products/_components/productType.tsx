"use client";

import { ProductTypeInterface } from "@/interfaces/products";
import { useProductContext } from "../_context/ProductContext";

export default function ProductType({ data, isSelected } : 
  { 
    data: ProductTypeInterface, 
    isSelected: boolean
  }) {
  
  const { value, setValue } = useProductContext();

  const handleSelect = () => {
    setValue({
      ...value,
      type: data.type
    });
  }

  return (
    <div onClick={handleSelect} className={`border-2 p-5 cursor-pointer rounded-md ${isSelected ? 'border-purple-600' : 'hover:border-purple-600'}`}>
      <div className="w-8 h-8 rounded-full bg-purple-600 mb-2"></div>
      <h3 className="font-bold text-base mb-1">{data.title}</h3>
      <p className="text-sm">{data.description}</p>
    </div>
  );
}
