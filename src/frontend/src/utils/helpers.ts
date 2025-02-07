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

export const forceDownload = (url: string, filename?: string): void => {
  const a = document.createElement('a');
  a.href = url;
  a.download = filename || '';  // Set the filename if provided
  a.target = '_blank';  // Open in a new tab
  a.click();
}

export const showLoading = (show = true) => {
  if (show) {
    document.getElementById('loading')?.classList.remove('hidden');
  } else {
    document.getElementById('loading')?.classList.add('hidden');
  }
}

export const formatShowDotPrice = (price: number) => {
  return new Intl.NumberFormat("vi-VN").format(price);
}

export const timeRemaining = (targetDate: string): string => {
  const now = new Date();
  const target = new Date(targetDate);

  const diffInMs = target.getTime() - now.getTime();

  if (diffInMs <= 0) {
      return "Đã qua thời gian";
  }

  const seconds = Math.floor(diffInMs / 1000);
  const minutes = Math.floor(seconds / 60);
  const hours = Math.floor(minutes / 60);
  const days = Math.floor(hours / 24);

  if (days > 0) {
      return `Còn ${days} ngày`;
  } else if (hours > 0) {
      return `Còn ${hours} giờ`;
  } else if (minutes > 0) {
      return `Còn ${minutes} phút`;
  } else {
      return `Còn ${seconds} giây`;
  }
}
