import axios from "../lib/axios/dist/esm/axios.min.js";
// import dotenv from "../lib/dotenv/lib/main.d.ts";
// const dotenv = require("dotenv");

// console.log("import.meta.env :>> ", process.env);

const apiKey = import.meta.env.AIRTABLE_API_KEY;
const baseKey = import.meta.env.NUGS_BASE_KEY;
const bearer_token = import.meta.env.AIRTABLE_BEARER_TOKEN;
export const base_api_url = `https://api.airtable.com/v0/${baseKey}/`;
const devmode = import.meta.env.NODE_ENV === "development";
console.log("apiKey :>> ", apiKey);

export const formatRecords = (records = []) => {
  let collection = [].concat(records);

  const format = (record) => {
    if (!record) return {};
    let { id, fields } = record;
    return {
      id,
      ...fields,
    };
  };

  let result =
    collection.length > 0 ? collection.map(format) : format(collection);

  return result;
};

export const searchBase = async (tableName, maxRecords = 10) => {
  const result = await axios({
    url: `${base_api_url}${tableName}?maxRecords=${maxRecords}`,
    headers: {
      "Content-Type": "x-www-form-urlencoded",
      Authorization: `Bearer ${apiKey}`,
    },
  });

  return formatRecords(result?.data?.records);
};
