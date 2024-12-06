import { getAxiosInstance } from "@/utils/helpers";

const axiosInstance = getAxiosInstance();

const prefix = 'search';

const searchService = {
    searchProduct: async (params) => {
        try {
            const response = await axiosInstance.get(prefix + '/product', { params });
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching ' + prefix);
        }
    }
}

export default searchService;
