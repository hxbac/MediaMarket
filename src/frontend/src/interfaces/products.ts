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

export interface ProductInfo {
    type: number,
    name: string,
    description: string,
    categories: string[],
    tags: string[],
}
