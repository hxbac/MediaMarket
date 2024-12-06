import { getAxiosInstance } from "@/utils/helpers";

const axiosInstance = getAxiosInstance();

const prefix = 'products';

const productService = {
    getProductsHomePage: async (options) => {
        try {
            const response = await axiosInstance.get(prefix + '/get-list-by-category', options);
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching product');
        }
    },
    create: async (params) => {
        try {
            const response = await axiosInstance.post(prefix, params);
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching product');
        }
    },
    getDetail: async (slug) => {
        try {
            const response = await axiosInstance.get(prefix + '/' + slug);
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching product');
        }
    },
    getMyProducts: async (params) => {
        try {
            const response = await axiosInstance.get(prefix + '/my-products', params);
            const result = response.data.data;
            result.data = result.data.map(item => {
                return {
                    key: item.id,
                    slug: item.slug,
                    thumbnail: item.thumbnail,
                    name: item.name,
                    price: item.price,
                    categories: item.categories.map(category => category.name)
                };
            });

            return result;
        } catch (error) {
            throw new Error(error.message || 'Error fetching product');
        }
    },
    getMyLatestProducts: async (params) => {
        try {
            const response = await axiosInstance.get(prefix + '/my-latest-products', params);
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching product');
        }
    },
    getCheckoutInfo: async (params) => {
        try {
            const response = await axiosInstance.get(prefix + '/checkout', { params });
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching ' + prefix);
        }
    },
    getLatestProducts: async (params) => {
        try {
            const response = await axiosInstance.get(prefix + '/latest-products-of-user', { params });
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching product');
        }
    },
}

export default productService;
