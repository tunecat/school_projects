import { IFetchResponse } from "./../types/iFetchResponse";
import Axios from "axios";
import store from '@/store';

export abstract class BaseService {
    private static axios = Axios.create({
        baseURL: "https://mamkupi.azurewebsites.net/api/v1.1/"
    });

    static async getTranslations<TEntity>(
        _baseUrl: string,
        language: string
    ): Promise<IFetchResponse<TEntity[]>> {
        const url = `${_baseUrl}`;
        try {
            const response = await this.axios.get<TEntity[]>(url, {
                headers: {
                    common: {
                        "Content-Type": "application/json"
                    },
                    'accept-language': language
                }
            });
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

    static async getAll<TEntity>(
        _baseUrl: string,
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
                        }
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

    static async getSingleEntity<TEntity>(
        url: string,
        jwt: string | null = null
    ): Promise<IFetchResponse<TEntity>> {
        try {
            let response;
            if (jwt) {
                response = await this.axios.get<TEntity>(url, {
                    headers: {
                        Authorization: `bearer ${jwt}`,
                        common: {
                            "Content-Type": "application/json"
                        }
                    }
                });
            } else {
                response = await this.axios.get<TEntity>(url)
            }
            console.log("getById action");
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
            }
        } catch (reason) {
            return {
                statusCode: reason.response.status
            };
        }
    }

    static async delete<TEntity>(
        url: string,
        data: any,
        jwt: string | null
    ): Promise<IFetchResponse<string>> {
        try {
            const response = await this.axios.delete<TEntity>(url, {
                headers: { Authorization: "Bearer " + jwt }
            });
            console.log("delete action");
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                };
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            };
        }
    }

    static async post<TEntity>(
        _baseUrl: string,
        data: any | null,
        jwt: string | null
    ): Promise<IFetchResponse<TEntity>> {
        console.log(data);
        const url = `${_baseUrl}`;
        try {
            const response = await this.axios.post<TEntity>(url, data, {
                headers: {
                    Authorization: `bearer ${jwt}`,
                    common: {
                        "Content-Type": "application/json"
                    }
                }
            });
            console.log("post action");
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status,
                    data: response.data
                };
            }
            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            };
        }
    }

    static async create<TEntity>(
        _baseUrl: string,
        entity: TEntity,
        jwt: string | null
    ): Promise<IFetchResponse<string>> {
        const url = `${_baseUrl}`;
        try {
            const response = await this.axios.post<TEntity>(url, entity, {
                headers: {
                    Authorization: `bearer ${jwt}`,
                    common: {
                        "Content-Type": "application/json"
                    }
                }
            });
            console.log("create action");
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                };
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            };
        }
    }

    static async update<TEntity>(
        _baseUrl: string,
        entity: TEntity,
        jwt: string | null
    ): Promise<IFetchResponse<string>> {
        const url = `${_baseUrl}`;
        try {
            const response = await this.axios.put<TEntity>(url, entity, {
                headers: {
                    Authorization: `bearer ${jwt}`,
                    common: {
                        "Content-Type": "application/json"
                    }
                }
            });
            console.log("update action");
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                };
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            };
        }
    }

    static async deleteSingleEntity<TEntity>(
        url: string,
        jwt: string | null
    ): Promise<IFetchResponse<string>> {
        try {
            const response = await this.axios.delete<TEntity>(url, {
                headers: { Authorization: "Bearer " + jwt }
            });
            console.log("delete action");
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                };
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            };
        }
    }
}
