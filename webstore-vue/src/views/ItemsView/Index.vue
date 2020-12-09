<template>
    <div class="row">
        <div class="col-xs-12 col-md-3">
            <form asp-action="Index">
                <div>
                    {{errorMessage}}
                    <strong>{{sharedTranslations.filter(t => t.code == "Brands")[0].name}}</strong>
                    <div class="form-group">
                        <multiselect
                            v-model="brands"
                            :multiple="true"
                            :close-on-select="false"
                            :clear-on-select="false"
                            :preserve-search="true"
                            placeholder="Pick some"
                            label="name"
                            track-by="name"
                            :options="itemsView.brands"
                        >
                            <template slot="selection" slot-scope="{ values, isOpen }">
                                <span
                                    class="multiselect__single"
                                    v-if="values.length &amp;&amp; !isOpen"
                                >{{ values.length }} options selected</span>
                            </template>
                        </multiselect>
                        <div v-for="brand in brands" :key="brand.id">{{brand.name}}</div>
                    </div>
                </div>
                <div>
                    <strong>{{sharedTranslations.filter(t => t.code == "Categories")[0].name}}</strong>
                    <div class="form-group">
                        <multiselect
                            v-model="categories"
                            :multiple="true"
                            :close-on-select="false"
                            :clear-on-select="false"
                            :preserve-search="true"
                            placeholder="Pick some"
                            label="name"
                            track-by="name"
                            :options="itemsView.categories"
                        >
                            <template slot="selection" slot-scope="{ values, search, isOpen }">
                                <span
                                    class="multiselect__single"
                                    v-if="values.length &amp;&amp; !isOpen"
                                >{{ values.length }} options selected</span>
                            </template>
                        </multiselect>
                        <div v-for="category in categories" :key="category.id">{{category.name}}</div>
                    </div>
                </div>
                <div class="form-group">
                    <a
                        type="submit"
                        @click="filter()"
                        class="btn btn-primary"
                    >{{commonTranslations.filter(t => t.code == "Filter")[0].name}}</a>
                </div>
            </form>
        </div>
        <div class="col-xs-12 col-md-9">
            <div class="row">
                <div
                    v-for="item in itemsView.items"
                    :key="item.id"
                    class="col-xs-12 col-sm-6 col-lg-4"
                    style="margin-bottom: 10px"
                >
                    <div class="card" style="min-width: 210px">
                        <router-link
                            class="nav-link text-dark"
                            :to="`/itemsView/Details/${item.id}`"
                        >
                            <img
                                class="card-img-top"
                                style="height: 240px;"
                                src="https://cdn.icon-icons.com/icons2/1678/PNG/512/wondicon-ui-free-parcel_111208.png"
                                alt
                            />
                            <div class="card-body" style="color:black;">
                                <p class="card-text">{{item.brand.name}} {{item.name}}</p>
                                <div
                                    v-if="item.discount > 0"
                                    class="card-text"
                                    style="display: flex; flex-wrap: wrap;"
                                >
                                    <div style="text-decoration: line-through">{{item.price}}</div>
                                    <div>$ -></div>
                                    <strong style="color:red;">{{item.itemPriceWithDiscount}}$</strong>
                                </div>
                                <div v-else>{{item.price}}$</div>
                            </div>
                        </router-link>
                        <div v-if="isAuthenticated()" class="row" style="justify-content:center">
                            <a
                                class="btn btn-primary own-btn own-btn-cart col-10"
                                @click="AddToCart(item.id)"
                            >{{commonTranslations.filter(t => t.code == "AddToCart")[0].name}}</a>
                            <a
                                class="btn btn-secondary own-btn own-btn-wish col-10"
                                @click="AddToWishList(item.id)"
                            >{{commonTranslations.filter(t => t.code == "AddToWishList")[0].name}}</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import router from "@/router";
import store from "@/store";
import IItem from "@/domain/Item/IItem";
import IItemDisplayView from "@/domain/Item/IItemDisplayView";
import IFilterSearch from "../../domain/FilterSearch/IFilterSearch";
import { IBrands } from "../../domain/Brands/IBrands";
import ICategory from "../../domain/Category/ICategory";
import Culture from "../../domain/Translations/Culture";

import { type } from "jquery";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class ItemsViewIndex extends Vue {
    @Prop()
    private post: string | undefined; // defined when dispatch in mounting is not needed

    private brands: Array<IBrands> = [];
    private categories: Array<ICategory> = [];

    private searchDTO: IFilterSearch = {
        Search: "",
        brandsFilter: [],
        categoriesFilter: [],
    };

    get itemsViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "item") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
    }

    get sharedTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const sharedTranslation = translations!.filter((t) => {
            if (t.entityName === "shared") return t;
        })[0].cultureDTO;
        return sharedTranslation;
    }

    get commonTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const commonTranslation = translations!.filter((t) => {
            if (t.entityName === "common") return t;
        })[0].cultureDTO;
        return commonTranslation;
    }

    errorMessage = "";

    AddToWishList(itemId: string): void {
        store.dispatch("addProductToWishList", itemId);
    }

    AddToCart(itemId: string): void {
        store.dispatch("addProductToCart", itemId);
    }

    filter() {
        this.searchDTO.categoriesFilter =
            this.categories.length === 0
                ? null
                : this.categories.map((i) => i.id);
        this.searchDTO.brandsFilter =
            this.brands.length === 0 ? null : this.brands.map((i) => i.id);
        store
            .dispatch("postItemsView", this.searchDTO)
            .then((response) => {
                if (response.statusCode < 200 && response.statusCode >= 300) {
                    this.errorMessage = JSON.parse(
                        response.errorMessage
                    ).message;
                }
            })
            .catch((e) => {
                console.log(e);
            });
    }

    get itemsView(): IItemDisplayView | null {
        return store.state.itemsView;
    }

    isAuthenticated(): boolean {
        return store.getters.isAuthenticated;
    }

    // ============ Lifecycle methods ==========
    beforeCreate(): void {
        console.log("beforeCreate");
    }

    created(): void {
        console.log("created");
    }

    beforeMount(): void {
        console.log("beforeMount");
    }

    mounted(): void {
        if (!this.post) {
            store.dispatch("getItemsView");
        }
        console.log("mounted");
    }

    beforeUpdate(): void {
        console.log("beforeUpdate");
    }

    updated(): void {
        console.log("updated");
    }

    beforeDestroy(): void {
        console.log("beforeDestroy");
    }

    destroyed(): void {
        console.log("destroyed");
    }
}
</script>
