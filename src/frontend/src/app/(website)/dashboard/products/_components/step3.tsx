"use client";

import { ProductType } from "@/enums/ProductType";
import { useProductContext } from "../_context/ProductContext";
import UploadVideo from "./uploads/uploadVideo";

export default function Step3() {
  const { value } = useProductContext();

  return (
    <div>
      {
        (() => {
          switch (value.type) {
            case ProductType.Video:
              return <UploadVideo />;
            case ProductType.Image:
              return <></>;
            default:
              return <></>;
          }
        })()
      }
    </div>
  )
}
