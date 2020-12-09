import { IBrands } from "../domain/Brands/IBrands";
import Axios from "axios";
import router from "@/router";
import store from "@/store";
import { IFetchResponse } from "../types/IFetchResponse";

interface ILoginResponse {
    token: string;
    status: string;
}
export abstract class CategoriesApi {
    private static axios = Axios.create({
        baseURL: "https://mamkupi.azurewebsites.net/api/v1.1/"
    });

    static async getAll<TEntity>(
        _baseUrl: string,
        language: string,
        jwt: string | null = null
    ): Promise<IFetchResponse<TEntity[]>> {
        const url = `${_baseUrl}`;
        try {
            let response;
            if (jwt) {
                response = await this.axios.get<TEntity[]>(url, {
                    headers: {
                        Authorization: `bearer ${jwt}`,
                        common: {
                            "Content-Type": "application/json"
                        },
                        'accept-language': language
                    }
                });
            } else {
                response = await this.axios.get<TEntity[]>(url);
            }
            console.log(response.status);
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    data: response.data,
                    statusCode: response.status
                }
            }
            // something went wrong
            return {
                statusCode: response.status
            };
        } catch (reason) {
            return {
                statusCode: reason.response.status
            };
        }
    }
}
