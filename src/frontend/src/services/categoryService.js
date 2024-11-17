import axiosInstance from "../utils/axios";

const prefix = 'categories';

const categoryService = {
    getAll: async () => {
        try {
            const response = await axiosInstance.get(prefix + '/get-all');
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching product');
        }
    }
}

export default categoryService;
