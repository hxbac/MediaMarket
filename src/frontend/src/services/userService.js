import axiosInstance from "../utils/axios";

const prefix = 'users';

const userService = {
    getProfile: async () => {
        try {
            const response = await axiosInstance.post(prefix + '/get-profile');
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching ' + prefix);
        }
    },
    getUserInfo: async (id) => {
        try {
            const response = await axiosInstance.get(prefix + '/' + id);
            return response.data;
        } catch (error) {
            throw new Error(error.message || 'Error fetching ' + prefix);
        }
    }
}

export default userService;
