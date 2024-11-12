import axios from "axios";
import https from 'https';

if (!process.env.NEXT_PUBLIC_API_URL) {
    throw new Error('Please config api endpoint in .env file.');
}

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
  httpsAgent: new https.Agent({
    rejectUnauthorized: false,
  }),
});

axiosInstance.interceptors.request.use(
  (config) => {
    if (typeof window === 'undefined') {
      return config;
    }

    const token = localStorage.getItem("accessToken");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    console.error("API Error:", error);
    return Promise.reject(error);
  }
);

export default axiosInstance;
