import moment from "moment";

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
