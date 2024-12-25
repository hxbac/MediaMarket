import { getAxiosInstance } from "@/utils/helpers";

const axiosInstance = getAxiosInstance(true);

const prefix = "admin/discounts";

const eventDiscountService = {
  getListDiscount: async (params) => {
    try {
      const response = await axiosInstance.get(prefix, params);
      return response.data.data;
    } catch (error) {
      throw new Error(error.message || "Error fetching " + prefix);
    }
  },
  create: async (params) => {
    try {
      const response = await axiosInstance.post(prefix, params);
      return response.data;
    } catch (error) {
      throw new Error(error.message || "Error fetching " + prefix);
    }
  },
};

export default eventDiscountService;
