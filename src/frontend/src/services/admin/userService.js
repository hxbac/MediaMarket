import { getAxiosInstance } from "@/utils/helpers";

const axiosInstance = getAxiosInstance();

const prefix = "admin/users";

const userService = {
  getListUser: async (params) => {
    try {
      const response = await axiosInstance.get(prefix, params);
      return response.data.data;
    } catch (error) {
      throw new Error(error.message || "Error fetching " + prefix);
    }
  },
};

export default userService;
