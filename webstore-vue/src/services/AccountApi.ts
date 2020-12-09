import { ILoginDTO } from "./../types/ILoginDTO";
import { IRegisterDTO } from "./../types/IRegisterDTO";
import Axios from "axios";
import { IFetchResponse } from "@/types/iFetchResponse";
import { ILoginResponse } from '@/types/ILoginResponse';

export abstract class AccountApi {
    private static axios = Axios.create({
        baseURL: "https://mamkupi.azurewebsites.net/api/v1.1/",
        headers: {
            common: {
                "Content-Type": "application/json"
            }
        }
    });

    static async getJwt(
        loginDTO: ILoginDTO
    ): Promise<IFetchResponse<ILoginResponse>> {
        const url = "account/login";
        try {
            const response = await this.axios.post(url, loginDTO);
            console.log("get Jwt reponse", response);
            if (response.status === 200) {
                return {
                    statusCode: response.status,
                    data: response.data.token
                };
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (error) {
            console.log("error: ", (error as Error).message);
            return {
                statusCode: 0,
                errorMessage: JSON.stringify((error as Error).message)
            };
        }
    }

    static async register(
        registerDTO: IRegisterDTO
    ): Promise<IFetchResponse<ILoginResponse>> {
        const url = "account/register";
        try {
            const response = await this.axios.post(url, registerDTO);
            console.log("register", response);
            // if registered and logged in
            if (response.status === 200) {
                return {
                    statusCode: response.status,
                    data: response.data.token
                };
            }

            // if something went wrong with registration or login
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (error) {
            console.log("error: ", (error as Error).message);
            return {
                statusCode: 0,
                errorMessage: JSON.stringify((error as Error).message)
            };
        }
    }
}
