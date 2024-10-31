import axiosInstance from "../utils/axios";

const prefix = 'products';

const productService = {
    getProductsHomePage: async () => {
        try {
            const response = await axiosInstance.get(prefix + '/get-list-home-page');
            return response.data;
        } catch (error) {
            throw new Error(error.response?.data?.message || 'Error fetching product');
        }
    }
}

export default productService;
