"use client";

import { Button } from "antd";
import Step1 from "../_components/step1";
import Step2 from "../_components/step2";
import Step3 from "../_components/step3";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { ProductType } from "@/enums/ProductType";
import { ProductContextProvider } from "../_context/ProductContext";
import { ProductInfo } from "@/interfaces/products";
import { CategoryHomePage } from "@/interfaces/categories";
import categoryService from "@/services/categoryService";
import { toast } from "react-toastify";
import productService from "@/services/productService";
import { showLoading } from "@/utils/helpers";

export default function Page() {
  const router = useRouter();
  const [currentStep, setCurrentStep] = useState<number>(1);
  const [categories, setCategories] = useState<CategoryHomePage[]>([]);
  const [productInfo, setProductInfo] = useState<ProductInfo>({
    type: ProductType.Video,
    name: '',
    thumbnail: '',
    shortDescription: '',
    price: 10000000,
    description: '',
    categoryIds: [],
    tags: [],
    originalFiles: [],
    previewImages: null,
    rangeVideoPreview: null,
    discounts: [],
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await categoryService.getAll();
        if (result.succeeded) {
          setCategories(result.data);
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

  const handlePrevStep = () => {
    const prevStep = currentStep - 1;
    if (prevStep === 0) {
      router.push('/dashboard/products');
      return;
    }
    setCurrentStep(prevStep);
  }

  const handleSubmitCreateProduct = async () => {
    try {
      showLoading();
      const result = await productService.create(productInfo);
      if (result.succeeded) {
        router.push('/dashboard/products');
        toast.success('Tạo sản phẩm thành công!');
      } else {
        throw new Error(result.message);
      }
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'An unexpected error occurred';
      toast.error(errorMessage);
    } finally {
      showLoading(false);
    }
  }

  const handleNextStep = () => {
    const nextStep = currentStep + 1;
    if (nextStep === 4) {
      handleSubmitCreateProduct();
    } else {
      setCurrentStep(nextStep);
    }
  }

  return (
    <section className="bg-white pt-16">
      <div className="items-center max-w-screen-xl px-4 py-8 mx-auto lg:gap-16 xl:gap-24 lg:py-24 lg:px-6">
        <ProductContextProvider initial={{
          value: productInfo,
          setValue: setProductInfo
         }}>
          {
            (() => {
              switch (currentStep) {
                case 2:
                  return <Step2 categories={categories} />;
                case 3:
                  return <Step3 />;
                default:
                  return <Step1 />;
              }
            })()
          }
        </ProductContextProvider>
        <div className="flex items-center justify-between mt-6">
          <Button onClick={handlePrevStep}>Quay Lại</Button>
          <Button onClick={handleNextStep} type="primary">{currentStep === 3 ? 'Hoàn Tất' : 'Tiếp Tục'}</Button>
        </div>
      </div>
    </section>
  );
}
