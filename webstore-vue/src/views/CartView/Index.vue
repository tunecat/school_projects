<template>
    <div>
        <div v-if="errorMessage">{{errorMessage}}</div>
        <div v-else-if="isEmpty()">
            <h3 style="text-align: center">Empty</h3>
        </div>
        <div class="row" v-else>
            <div class="col-9">
                <table class="table">
                    <thead>
                        <tr>
                            <th>{{itemsViewTranslations.filter(t => t.code == "ItemUnit")[0].name}}</th>
                            <th>{{itemsViewTranslations.filter(t => t.code == "Price")[0].name}}</th>
                            <th>{{itemsInCartViewTranslations.filter(t => t.code == "ItemAmount")[0].name}}</th>
                            <th>{{itemsInCartViewTranslations.filter(t => t.code == "ItemPrice")[0].name}}</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="itemInCart in cartView.cartItems" :key="itemInCart.id">
                            <td>
                                <router-link
                                    class="row"
                                    :to="`/itemsView/Details/${itemInCart.itemId}`"
                                >
                                    <div class="col-3">
                                        <img
                                            class="card-img-top"
                                            src="https://cdn.icon-icons.com/icons2/1678/PNG/512/wondicon-ui-free-parcel_111208.png"
                                            alt
                                        />
                                    </div>
                                    <div class="col-9">{{itemInCart.item.name}}</div>
                                </router-link>
                            </td>
                            <td>{{itemInCart.itemPriceWithDiscount}}</td>
                            <td>
                                {{itemInCart.amount}}
                                <a @click="DecreaseQuantity(itemInCart.id)">
                                    <i style="font-size: 10px" class="fa fa-minus"></i>
                                </a>
                                <a @click="IncreaseQuantity(itemInCart.id)">
                                    <i style="font-size: 12px" class="fa fa-plus"></i>
                                </a>
                            </td>
                            <td>{{itemInCart.totalPriceWithDiscount}}</td>
                            <td>
                                <a @click="RemoveFromCart(itemInCart.id)">{{commonTranslations.filter(t => t.code == "RemoveFromCart")[0].name}}</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-3">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Total Price</th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{{cartView.totalPrice}} $</td>
                            <td>
                                <a @click="ClearCart(cartView.id)">{{commonTranslations.filter(t => t.code == "ClearCart")[0].name}}</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import router from "@/router";
import store from "@/store";
import { ICart } from "../../domain/Cart/ICart";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class CartViewIndex extends Vue {
    errorMessage = "";

    isEmpty(): boolean {
        return this.cartView == null
            ? true
            : this.cartView.cartItems!.length < 1;
    }

    get cartView(): ICart | null {
        const cart = store.state.cartView;
        return cart;
    }

    ClearCart(cartId: string): void {
        store.dispatch("clearCart", cartId).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                store.dispatch("getCartsView");
            } else {
                this.errorMessage = JSON.parse(response.errorMessage).message;
            }
        });
    }

    RemoveFromCart(itemInCartId: string): void {
        store
            .dispatch("removeProductFromCart", itemInCartId)
            .then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    store.dispatch("getCartsView");
                } else {
                    this.errorMessage = JSON.parse(
                        response.errorMessage
                    ).message;
                }
            });
    }

    DecreaseQuantity(itemInCartId: string): void {
        store.dispatch("decreaseAmount", itemInCartId).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                store.dispatch("getCartsView");
            } else {
                this.errorMessage = JSON.parse(response.errorMessage).message;
            }
        });
    }

    IncreaseQuantity(itemInCartId: string): void {
        store.dispatch("increaseAmount", itemInCartId).then((response) => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                store.dispatch("getCartsView");
            } else {
                this.errorMessage = JSON.parse(response.errorMessage).message;
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
            .dispatch("getCartsView")
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

    get itemsInCartViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "itemInCart") return t;
        })[0].cultureDTO;
        return ItemsViewTranslation;
    }

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
}
</script>
