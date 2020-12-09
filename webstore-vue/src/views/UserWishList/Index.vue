<template>
    <div>
        <div v-if="errorMessage">{{errorMessage}}</div>
        <div v-else-if="userWishList.length < 1">
            <h3 style="text-align: center">Empty</h3>
        </div>
        <div v-else>
            <div
                style="text-align: center"
            >{{commonTranslations.filter(t => t.code == "WishList")[0].name}}</div>
            <hr />
            <div class="row">
                <div
                    v-for="item in userWishList"
                    :key="item.id"
                    class="col-xs-12 col-sm-6 col-md-4 col-lg-3"
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
                        <div class="row" v-if="isAuthenticated()" style="justify-content:center">
                            <a
                                class="btn btn-primary own-btn own-btn-cart col-10"
                                @click="AddToCart(item.id)"
                            >{{commonTranslations.filter(t => t.code == "AddToCart")[0].name}}</a>
                            <a
                                class="btn btn-secondary own-btn own-btn-wish col-10"
                                @click="RemoveFromWishList(item.id)"
                            >{{commonTranslations.filter(t => t.code == "Remove")[0].name}}</a>
                        </div>
                    </div>
                    <hr />
                </div>
                <div class="col-12" style="text-align: center">
                    <a
                        class="btn-secondary btn"
                        style="padding-right: 40px; padding-left: 40px"
                        @click="ClearWishList()"
                    >{{commonTranslations.filter(t => t.code == "Clear")[0].name}}</a>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "@/router";
import store from "@/store";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class UserWishListIndex extends Vue {
    errorMessage = "";

    get userWishList() {
        return store.state.userWishList;
    }

    ClearWishList(): void {
        store.dispatch("clearUserWishList").then(() => {
            store.dispatch("getUserWishList");
        });
    }

    RemoveFromWishList(itemId: string): void {
        store.dispatch("removeProductFromWishList", itemId).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                store.dispatch("getUserWishList");
            } else {
                this.errorMessage = JSON.parse(response.errorMessage).message;
            }
        });
    }

    AddToCart(itemId: string): void {
        store
            .dispatch("addProductToCartFromWishList", itemId)
            .then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    store.dispatch("getUserWishList");
                } else {
                    this.errorMessage = JSON.parse(
                        response.errorMessage
                    ).message;
                }
            });
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
        store
            .dispatch("getUserWishList")
            .then((response) => {
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.errorMessage = "NOT ALLOWED";
                } else {
                    this.errorMessage = JSON.parse(
                        response.errorMessage
                    ).message;
                }
            })
            .catch((e) => {
                console.log(e);
            });
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
}
</script>
