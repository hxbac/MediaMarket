import { getAxiosInstance } from "@/utils/helpers";

const axiosInstance = getAxiosInstance(true);

const prefix = "admin/withdrawals";

const withdrawalService = {
  getListWithdrawal: async (params) => {
    try {
      const response = await axiosInstance.get(prefix, params);
      return response.data.data;
    } catch (error) {
      throw new Error(error.message || "Error fetching " + prefix);
    }
  },
  approvalRequest: async (id, params) => {
    try {
      const response = await axiosInstance.put(prefix + `/${id}/approval`, params);
      return response.data;
    } catch (error) {
      throw new Error(error.message || "Error fetching " + prefix);
    }
  }
};

export default withdrawalService;
