import { DiscountType } from "@/enums/DiscountType";

export interface ProductCard {
    id: string;
    name: string;
    slug: string;
    price: number;
    productType: number | null;
    shortDescription: string;
    thumbnail: string;
}

export interface ProductTypeInterface {
    type: number,
    title: string,
    description: string
};

export interface FileInfo {
    id: string|null;
    url: string;
}

export interface ProductInfo {
    type: number,
    name: string,
    shortDescription: string,
    description: string,
    categoryIds: string[],
    price: number,
    thumbnail: string,
    tags: string[],
    originalFiles: FileInfo[],
    rangeVideoPreview: [string, string] | null,
    previewImages: string[] | null,
    discounts: ProductDiscount[]
}

export interface ProductDiscount {
    type: number;
    value: number;
    timeRange: string[];
}

export interface SearchProductParams {
    name: string;
    productType: number | null;
}

export interface SearchProductOrderParams {
    name: string;
    productType: number | null;
}

export interface MyProductLatest {
    id: string;
    name: string;
    slug: string;
    price: number;
    productType: number;
    shortDescription: string;
    thumbnail: string;
}

export interface DiscountItem {
    id: string;
    endDate: string;
    value: number;
    type: DiscountType;
}

export interface EventDiscountItem {
    id: string;
    endDate: string;
    value: number;
    type: DiscountType;
}

export interface ProductCheckoutInfo {
    id: string;
    name: string;
    slug: string;
    thumbnail: string;
    price: number;
    shortDescription: string;
    eventDiscounts: EventDiscountItem[];
    productDiscounts: DiscountItem[];
}
