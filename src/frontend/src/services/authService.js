import axiosInstance from "../utils/axios";

const prefix = 'auth';

const authService = {
    login: async (data) => {
        try {
            const url = prefix + '/login';
            const response = await axiosInstance.post(url, data);
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching authentication');
        }
    }
}

export default authService;
