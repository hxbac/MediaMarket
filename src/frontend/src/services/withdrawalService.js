import axiosInstance from "../utils/axios";

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
};

export default userService;
