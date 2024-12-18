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
    previewImages: string[] | null
}

export interface SearchProductParams {
    name: string;
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

export interface ProductCheckoutInfo {
    id: string;
    name: string;
    slug: string;
    thumbnail: string;
    price: number;
    shortDescription: string;
}
