import axios from 'axios';
import { baseURL } from '../config/ServiceConfig.js';

const brandBaseUrl = `${baseURL}setup/brand/`;
// const brandPostBaseUrl = `${baseURL}setup/brand/create/`;

export const getBrandList = () => {
  return axios.get(brandBaseUrl);
};

// export const postBrandList = () => {
//   return axios.post(brandPostBaseUrl, brandData);
// };
