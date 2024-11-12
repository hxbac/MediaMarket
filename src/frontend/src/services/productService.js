import axiosInstance from "../utils/axios";

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
    }
}

export default productService;
