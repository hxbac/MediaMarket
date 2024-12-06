import { getAxiosInstance } from "@/utils/helpers";

const axiosInstance = getAxiosInstance();

const prefix = "withdrawal";

const userService = {
  createRequest: async (params) => {
    try {
      const response = await axiosInstance.post(prefix + "/create", params);
      return response.data;
    } catch (error) {
      throw new Error(error.message || "Error fetching " + prefix);
    }
  },
  getMyRequests: async (params) => {
    try {
      const response = await axiosInstance.get(prefix + "/my-requests", params);
      return response.data.data;
    } catch (error) {
      throw new Error(error.message || "Error fetching " + prefix);
    }
  },
};

export default userService;
