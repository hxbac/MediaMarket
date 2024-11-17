import axiosInstance from "../utils/axios";

const prefix = 'orders';

const orderService = {
    create: async (params) => {
        try {
            const response = await axiosInstance.post(prefix + '/create', params);
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching ' + prefix);
        }
    },
    getMyPurchases: async (params) => {
        try {
            const response = await axiosInstance.get(prefix + '/my-purchases', params);
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
    getMyOrders: async (params) => {
        try {
            const response = await axiosInstance.get(prefix + '/my-orders', params);
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
    }
}

export default orderService;
