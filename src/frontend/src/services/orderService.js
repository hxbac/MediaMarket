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
    }
}

export default orderService;
