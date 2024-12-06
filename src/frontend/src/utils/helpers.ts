import moment from "moment";
import axiosInstance from "./axios";

export const formatPrice = (price: number) => {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  }).format(price);
};

export const formatDatetime = (datetimeOffset: string | null, format = 'YYYY-MM-DD HH:mm:ss') => {
  if (!datetimeOffset) {
    return "";
  }
  return moment(datetimeOffset).format(format);
}

export const getAxiosInstance = (isAdmin = false) => {
  if (typeof window === 'undefined') {
    return axiosInstance;
  }

  const accessToken = isAdmin ? localStorage.getItem("accessTokenAdmin") : localStorage.getItem("accessToken");
  if (accessToken) {
    axiosInstance.interceptors.request.use(
      (config) => {
        config.headers.Authorization = `Bearer ${accessToken}`;
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );
  }

  return axiosInstance;
}
