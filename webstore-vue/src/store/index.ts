import { IBrandsEdit } from "./../domain/Brands/IBrandsEdit";
import { IBrandsCreate } from "./../domain/Brands/IBrandsCreate";
import { IBrands } from "./../domain/Brands/IBrands";
import { ILoginDTO } from "./../types/ILoginDTO";
import Vue from "vue";
import Vuex from "vuex";
import { AccountApi } from "@/services/AccountApi";
import jwtDecode from "jwt-decode";
import { BaseService } from "@/services/BaseService";
import { IFetchResponse } from "@/types/IFetchResponse";
import IItem from "@/domain/Item/IItem";
import IItemCreate from "@/domain/Item/IItemCreate";
import { IRegisterDTO } from '@/types/IRegisterDTO';
import { ILoginResponse } from '@/types/ILoginResponse';
import IItemEdit from '@/domain/Item/IItemEdit';
import IItemDisplayView from '@/domain/Item/IItemDisplayView';
import { ICart } from '@/domain/Cart/ICart';
import { IItemInCart } from '@/domain/ItemInCart/IItemInCart';
import IFilterSearch from '@/domain/FilterSearch/IFilterSearch';
import createPersistedState from "vuex-persistedstate";
import ICategory from '@/domain/Category/ICategory';
import { IWarehouse } from '@/domain/Warehouse/IWarehouse';
import { IWarehouseDisplay } from '@/domain/Warehouse/IWarehouseDisplay';
import { IItemInWarehouse } from '@/domain/ItemInWarehouse/IItemInWarehouse';
import { IItemInWarehouseDisplay } from '@/domain/ItemInWarehouse/IItemInWarehouseDisplay';
import ICategoryEdit from '@/domain/Category/ICategoryEdit';
import ICategoryCreate from '@/domain/Category/ICategoryCreate';
import { IWarehouseCreate } from '@/domain/Warehouse/IWarehouseCreate';
import { IWarehouseEdit } from '@/domain/Warehouse/IWarehouseEdit';
import { IWarehouseDTO } from '@/domain/Warehouse/IWarehouseDTO';
import IItemDTO from '@/domain/Item/IItemDTO';
import CultureDTO from '@/domain/Translations/CultureDTO';
import Culture from '@/domain/Translations/Culture';
import UICultures from '@/domain/Translations/UICultures';
import { CategoriesApi } from '@/services/CategoriesApi';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        jwt: null as string | null,
        brands: [] as IBrands[],
        brand: null as null | IBrands,
        items: [] as IItem[],
        item: null as null | IItem,
        itemDTO: null as null | IItemDTO,
        itemsView: null as null | IItemDisplayView,
        userWishList: [] as IItem[],
        cartView: null as null | ICart,
        cart: null as null | ICart,
        carts: null as null | ICart[],
        category: null as null | ICategory,
        categories: null as null | ICategory[],
        warehouse: null as null | IWarehouseDTO,
        warehouses: null as null | IWarehouse[],
        itemInWarehouse: null as null | IItemInWarehouseDisplay,
        profileEntity: "" as string,
        translations: null as Culture[] | null,
        currentCulture: { name: "English", code: "en" } as UICultures,
        cultures: [] as UICultures[]
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },
        setTranslations(state, translations) {
            state.translations = translations;
        },
        setBrands(state, brands: IBrands[]) {
            state.brands = brands;
        },
        setBrand(state, brand: IBrands) {
            state.brand = brand;
        },
        setItems(state, items: IItem[]) {
            state.items = items;
        },
        setItem(state, item: IItem) {
            state.item = item;
        },
        setItemDTO(state, itemDTO: IItemDTO) {
            state.itemDTO = itemDTO;
        },
        setItemsView(state, itemsView: IItemDisplayView) {
            state.itemsView = itemsView;
        },
        setUserWishList(state, userWishList: IItem[]) {
            state.userWishList = userWishList;
        },
        setCartView(state, cartView: ICart) {
            state.cartView = cartView;
        },
        setCategory(state, category: ICategory) {
            state.category = category;
        },
        setCategories(state, categories: ICategory[]) {
            state.categories = categories;
        },
        setCarts(state, carts: ICart[]) {
            state.carts = carts;
        },
        setCart(state, cart: ICart) {
            state.cart = cart;
        },
        setWarehouses(state, warehouses: IWarehouse[]) {
            state.warehouses = warehouses;
        },
        setWarehouse(state, warehouse: IWarehouseDTO) {
            state.warehouse = warehouse;
        },
        setItemInWarehouse(state, itemInWarehouse: IItemInWarehouseDisplay) {
            state.itemInWarehouse = itemInWarehouse;
        },
        setCurrentCulture(state, culture: UICultures) {
            state.currentCulture = culture;
        },
        setCultures(state, cultures: UICultures[]) {
            state.cultures = cultures;
        },
        setProfileState(state, profileEntity: string) {
            state.profileEntity = profileEntity;
        }
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        },
        userRole(context): string {
            if (context.jwt !== null) {
                const decoded = jwtDecode(context.jwt) as Record<
                    string,
                    string
                >;
                return JSON.stringify(
                    decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
                );
            }
            return "null";
        },
        userId(context): string {
            if (context.jwt !== null) {
                const decoded = jwtDecode(context.jwt) as Record<
                    string,
                    string
                >;
                return JSON.stringify(
                    decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
                );
            }
            return "null";
        },
        userName(context): string {
            if (context.jwt !== null) {
                const decoded = jwtDecode(context.jwt) as Record<
                    string,
                    string
                >;
                return JSON.stringify(
                    decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
                );
            }
            return "null";
        }
    },
    actions: {
        clearJwt(context): void {
            context.commit("setJwt", null);
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<IFetchResponse<ILoginResponse>> {
            const loginResponse = await AccountApi.getJwt(loginDTO).then(
                response => {
                    if (response.statusCode === 200) {
                        console.log(response.data)
                        context.commit("setJwt", response.data)
                    }
                    return response;
                }
            );
            return loginResponse;
        },
        async registerUser(context, registerDTO: IRegisterDTO): Promise<IFetchResponse<ILoginResponse>> {
            const registerResponse = await AccountApi.register(registerDTO).then(
                response => {
                    if (response.statusCode === 200) {
                        console.log(response.data)
                        context.commit("setJwt", response.data)
                    }
                    return response;
                }
            );
            return registerResponse;
        },
        async setCurrentCulture(context, culture: UICultures) {
            context.commit("setCurrentCulture", culture);
        },
        async setCultures(context) {
            const response = await BaseService.getAll<CultureDTO>("cultures");
            if (response.data != null) context.commit("setCultures", response.data);
        },
        async setTranslations(context) {
            const response = await BaseService.getTranslations<CultureDTO>("cultures/resources", context.state.currentCulture.code);
            if (response.data != null) context.commit("setTranslations", response.data);
        },
        // CUSTOMER

        // brands public page
        async getBrandsView(context): Promise<IFetchResponse<IBrands[]>> {
            console.log("getBrandsView");
            const response = await BaseService.getAll<IBrands>("brandsView");
            if (response.data != null) context.commit("setBrands", response.data);
            return response;
        },
        async getBrandView(context, id): Promise<IFetchResponse<IBrands>> {
            console.log("getBrandViewById");
            const response = await BaseService.getSingleEntity<IBrands>(
                `brandsView/${id}`
            );
            if (response.data != null) context.commit("setBrand", response.data);
            return response;
        },

        // items public page
        async getItemsView(context): Promise<IFetchResponse<IItemDisplayView>> {
            console.log("getItemsView");
            const response = await BaseService.getSingleEntity<IItemDisplayView>("itemsView");
            if (response.data != null) context.commit("setItemsView", response.data);
            return response;
        },
        async postItemsView(context, filter: IFilterSearch): Promise<IFetchResponse<IItemDisplayView>> {
            console.log("postItemsView");
            const response = await BaseService.post<IItemDisplayView>("itemsView", filter, null);
            const itemView = response.data
            if (itemView) context.commit("setItemsView", itemView);
            return response;
        },
        async getItemView(context, id): Promise<IFetchResponse<IItem>> {
            console.log("geItemViewById");
            const response = await BaseService.getSingleEntity<IItem>(
                `itemsView/${id}`
            );
            if (response.data) context.commit("setItem", response.data);
            return response;
        },

        // customer cart page
        async getCartsView(context): Promise<IFetchResponse<ICart>> {
            console.log("getCartView");
            const response = await BaseService.getSingleEntity<ICart>("CartsView", context.state.jwt);
            console.log(response.data);
            if (response.data) context.commit("setCartView", response.data);
            return response;
        },
        async addProductToCart(context, itemId): Promise<IFetchResponse<IItem>> {
            const response = await BaseService.post<IItem>("ItemsView/AddToCart", { itemId }, context.state.jwt);
            return response;
        },
        async removeProductFromCart(context, itemInCartId): Promise<IFetchResponse<ICart>> {
            const response = await BaseService.post<ICart>("cartsView/RemoveProduct", { id: itemInCartId }, context.state.jwt);
            return response;
        },
        async clearCart(context, cartId: string): Promise<IFetchResponse<ICart>> {
            const response = await BaseService.post<ICart>("cartsView/clear", { id: cartId }, context.state.jwt);
            return response;
        },
        async increaseAmount(context, itemInCartId: string): Promise<IFetchResponse<IItemInCart>> {
            const response = await BaseService.post<IItemInCart>("cartsView/IncreaseQuantity", { id: itemInCartId }, context.state.jwt);
            return response;
        },
        async decreaseAmount(context, itemInCartId: string): Promise<IFetchResponse<IItemInCart>> {
            const response = await BaseService.post<IItemInCart>("cartsView/DecreaseQuantity", { id: itemInCartId }, context.state.jwt);
            return response;
        },

        // customer wishlist page
        async getUserWishList(context): Promise<IFetchResponse<IItem[]>> {
            console.log("getUserWishList");
            const response = await BaseService.getAll<IItem>("userWishList", context.state.jwt);
            if (response.data) context.commit("setUserWishList", response.data);
            return response;
        },
        async clearUserWishList(context): Promise<IFetchResponse<IItem[]>> {
            console.log("clearUserWishList");
            const response = await BaseService.post<IItem[]>("userWishList/Clear", null, context.state.jwt);
            return response;
        },
        async addProductToCartFromWishList(context, itemId): Promise<IFetchResponse<IItem>> {
            const response = await BaseService.post<IItem>("userWishList/AddProductToCart", { itemId }, context.state.jwt);
            return response;
        },
        async addProductToWishList(context, itemId): Promise<void> {
            await BaseService.post<IItem>("ItemsView/AddToWishList", { itemId }, context.state.jwt);
        },
        async removeProductFromWishList(context, itemId: string): Promise<IFetchResponse<IItem>> {
            console.log("removeProductFromWishList");
            const response = await BaseService.post<IItem>("userWishList/RemoveProduct", { itemId }, context.state.jwt);
            return response;
        },

        // search page
        async getSearchPage(context, search): Promise<void> {
            console.log("getSearchPage");
            const itemView = await BaseService.getAll<IItemDisplayView>(`searchpage/${search}`, context.state.jwt);
            context.commit("///", itemView);
        },
        async postSearchPage(context, filter: IFilterSearch): Promise<void> {
            console.log("postSearchPage");
            const itemView = await BaseService.post<IItemDisplayView>("searchpage", filter, context.state.jwt);
            context.commit("///", itemView);
        },

        // BUSINESS CLIENT

        // Warehouses
        async getWarehouses(context): Promise<IFetchResponse<IWarehouse[]>> {
            const response = await BaseService.getAll<IWarehouse>("warehouses", context.state.jwt);
            if (response.data) context.commit("setWarehouses", response.data);
            return response;
        },
        async getWarehouse(context, id): Promise<IFetchResponse<IWarehouseDTO>> {
            const response = await BaseService.getSingleEntity<IWarehouseDTO>(
                `warehouses/${id}`, context.state.jwt
            );
            if (response.data) context.commit("setWarehouse", response.data);
            return response;
        },
        async getItemInWarehouse(context, id): Promise<IFetchResponse<IItemInWarehouseDisplay>> {
            const response = await BaseService.getSingleEntity<IItemInWarehouseDisplay>(
                `ItemInWarehouses/${id}`, context.state.jwt
            );
            if (response.data) context.commit("setItemInWarehouse", response.data);
            return response;
        },
        async deleteItemInWarehouse(context, id): Promise<IFetchResponse<string>> {
            console.log(id);
            const itemInWarehouse = await BaseService.deleteSingleEntity<IItemInWarehouse>(
                `ItemInWarehouses/${id}`,
                context.state.jwt
            );
            return itemInWarehouse;
        },
        async createWarehouse(
            context,
            warehouseCreateDTO
        ): Promise<IFetchResponse<string>> {
            const warehouse = await BaseService.create<IWarehouseCreate>(
                "Warehouses",
                warehouseCreateDTO,
                context.state.jwt
            );
            return warehouse;
        },
        async editWarehouse(
            context,
            warehouseEditDTO
        ): Promise<IFetchResponse<string>> {
            const warehouse = await BaseService.update<IWarehouseEdit>(
                `warehouses/${warehouseEditDTO.id}`,
                warehouseEditDTO,
                context.state.jwt
            );
            return warehouse;
        },
        async deleteWarehouse(context, id): Promise<IFetchResponse<string>> {
            const warehouse = await BaseService.deleteSingleEntity<IWarehouse>(
                `warehouses/${id}`,
                context.state.jwt
            );
            return warehouse;
        },
        async AddItemsToWarehouse(context, WarehouseItemsDTO): Promise<IFetchResponse<IWarehouseDTO>> {
            const response = await BaseService.post<IWarehouseDTO>(
                `Warehouses/AddItemsToWarehouse`,
                WarehouseItemsDTO,
                context.state.jwt
            );
            return response;
        },
        async ChangeAmount(context, { itemInWarehouseId, amount, operation }): Promise<IFetchResponse<IItemInWarehouse>> {
            const response = await BaseService.getSingleEntity<IItemInWarehouse>(
                `ItemInWarehouses/ChangeAmount?id=${itemInWarehouseId}&amount=${amount}&submitButton=${operation}`,
                context.state.jwt
            );
            return response;
        },

        // Categories
        async getCategories(context): Promise<IFetchResponse<ICategory[]>> {
            console.log("getCategories");
            const response = await CategoriesApi.getAll<ICategory>("categories", context.state.currentCulture.code, context.state.jwt);
            if (response.data) context.commit("setCategories", response.data);
            return response;
        },
        async getChildCategories(context): Promise<IFetchResponse<ICategory[]>> {
            console.log("getChildCategories");
            const response = await CategoriesApi.getAll<ICategory>("categories/Child", context.state.currentCulture.code, context.state.jwt);
            if (response.data) context.commit("setCategories", response.data);
            return response;
        },
        async getCategory(context, id): Promise<IFetchResponse<ICategory>> {
            console.log("getCategoryById");
            const response = await BaseService.getSingleEntity<ICategory>(
                `Categories/${id}`, context.state.jwt
            );
            if (response.data) context.commit("setCategory", response.data);
            return response;
        },
        async createCategory(
            context,
            CategoryCreateDTO
        ): Promise<IFetchResponse<string>> {
            console.log("createCategory");
            const category = await BaseService.create<ICategoryCreate>(
                "categories",
                CategoryCreateDTO,
                context.state.jwt
            );
            return category;
        },
        async editCategory(
            context,
            categoryEditDTO
        ): Promise<IFetchResponse<string>> {
            console.log("editCategory");
            const category = await BaseService.update<ICategoryEdit>(
                `categories/${categoryEditDTO.id}`,
                categoryEditDTO,
                context.state.jwt
            );
            return category;
        },
        async deleteCategory(context, id): Promise<IFetchResponse<string>> {
            console.log("deleteCategory");
            const category = await BaseService.deleteSingleEntity<ICategory>(
                `categories/${id}`,
                context.state.jwt
            );
            return category;
        },

        // Brands
        async getBrands(context): Promise<IFetchResponse<IBrands[]>> {
            console.log("getBrands");
            const response = await BaseService.getAll<IBrands>("brands", context.state.jwt);
            if (response.data) context.commit("setBrands", response.data);
            return response;
        },
        async getBrand(context, id): Promise<IFetchResponse<IBrands>> {
            console.log("getBrandById");
            const response = await BaseService.getSingleEntity<IBrands>(
                `brands/${id}`, context.state.jwt
            );
            if (response.data) context.commit("setBrand", response.data);
            return response;
        },
        async createBrand(
            context,
            BrandCreateDTO
        ): Promise<IFetchResponse<string>> {
            console.log("createBrand");
            const brand = await BaseService.create<IBrandsCreate>(
                "brands",
                BrandCreateDTO,
                context.state.jwt
            );
            return brand;
        },
        async editBrand(
            context,
            BrandEditDTO
        ): Promise<IFetchResponse<string>> {
            console.log("editBrand");
            const brand = await BaseService.update<IBrandsEdit>(
                `brands/${BrandEditDTO.id}`,
                BrandEditDTO,
                context.state.jwt
            );
            return brand;
        },
        async deleteBrand(context, id): Promise<IFetchResponse<string>> {
            console.log("getBrandById");
            const brand = await BaseService.deleteSingleEntity<IBrands>(
                `brands/${id}`,
                context.state.jwt
            );
            return brand;
        },

        // Items
        async getItems(context): Promise<IFetchResponse<IItem[]>> {
            console.log("getItems");
            const response = await BaseService.getAll<IItem>("items", context.state.jwt);
            if (response.data) context.commit("setItems", response.data);
            return response;
        },
        async getItem(context, id): Promise<IFetchResponse<IItem>> {
            console.log("geItemById");
            const response = await BaseService.getSingleEntity<IItem>(
                `items/${id}`, context.state.jwt
            );
            if (response.data) context.commit("setItem", response.data);
            return response;
        },
        async getItemDTO(context, id): Promise<IFetchResponse<IItemDTO>> {
            console.log("geItemDTO");
            const response = await BaseService.getSingleEntity<IItemDTO>(
                `items/getItemDTO/${id}`, context.state.jwt
            );
            if (response.data) context.commit("setItemDTO", response.data);
            return response;
        },
        async createItem(
            context,
            ItemCreateDTO
        ): Promise<IFetchResponse<string>> {
            console.log("createItem");
            const item = await BaseService.create<IItemCreate>(
                "items",
                ItemCreateDTO,
                context.state.jwt
            );
            return item;
        },
        async editItem(context, ItemEditDTO): Promise<IFetchResponse<string>> {
            console.log("editItem");
            const item = await BaseService.update<IItemEdit>(
                `items/${ItemEditDTO.id}`,
                ItemEditDTO,
                context.state.jwt
            );
            return item;
        },
        async deleteItem(context, id): Promise<IFetchResponse<string>> {
            console.log("deleteItem");
            const item = await BaseService.deleteSingleEntity<IItem>(
                `items/${id}`,
                context.state.jwt
            );
            return item;
        },

        setProfileState(context, state: string) {
            context.commit("setProfileState", state);
        }
    },
    plugins: [createPersistedState()],
    modules: {}
});
