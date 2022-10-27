import axios from "axios";
import { baseURL } from "../config/ServiceConfig.js";

export const getBrandList = () => {
  let url = `${baseURL}setup/brand/`;
  axios.get(url).then((response) => {
    console.log(response);
    return response && response.data && response.data.data
      ? response.data.data
      : null;
  });
};
