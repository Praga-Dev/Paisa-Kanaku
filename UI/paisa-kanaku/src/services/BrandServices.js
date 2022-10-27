import axios from "axios";
import { baseURL } from "../config/ServiceConfig.js";

const brandBaseUrl = `${baseURL}setup/brand/`;

export const getBrandList = () => {
  return axios.get(brandBaseUrl);
};
