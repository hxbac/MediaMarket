export interface ProductHomePage {
    id: string;
    name: string;
    price: number;
    productType: number;
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
    originalFiles: FileInfo[]
}
