<template>
    <div>
        <h1>{{commonTranslations.filter(t => t.code == "Details")[0].name}}</h1>

        <div>
            <h4>{{itemsViewTranslations.filter(t => t.code == "ItemUnit")[0].name}}</h4>
            <hr />
            <dl v-if="item" class="row">
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Name")[0].name}}</dt>
                <dd class="col-sm-10">{{item.name}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Description")[0].name}}</dt>
                <dd class="col-sm-10">{{item.description}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Price")[0].name}}</dt>
                <dd class="col-sm-10">{{item.Price}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "ItemPriceWithDiscount")[0].name}}</dt>
                <dd class="col-sm-10">{{item.itemPriceWithDiscount}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Discount")[0].name}}</dt>
                <dd class="col-sm-10">{{item.Discount}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Brand")[0].name}}</dt>
                <dd class="col-sm-10">{{item.brand.name}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Categories")[0].name}}</dt>
                <dd class="col-sm-10">
                    <div v-for="category in item.categories" :key="category.id">{{category.name}}</div>
                </dd>
            </dl>
            <td v-if="isAuthenticated()">
                <a
                    class="btn btn-primary own-btn own-btn-cart"
                    @click="AddToCart(item.id)"
                >{{commonTranslations.filter(t => t.code == "AddToCart")[0].name}}</a>
                <a
                    class="btn btn-secondary own-btn own-btn-wish"
                    @click="AddToWishList(item.id)"
                >{{commonTranslations.filter(t => t.code == "AddToWishList")[0].name}}</a>
            </td>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import IItem from "@/domain/Item/IItem";
import CultureDTO from '@/domain/Translations/CultureDTO';

@Component
export default class ItemsViewDetails extends Vue {
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

    @Prop()
    private id!: string;

    AddToWishList(itemId: string): void {
        console.log(itemId);
        store.dispatch("addProductToWishList", itemId);
    }

    AddToCart(itemId: string): void {
        console.log(itemId);
        store.dispatch("addProductToCart", itemId);
    }

    get item(): IItem | null {
        return store.state.item;
    }

    get userRole(): string {
        return store.getters.userRole;
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
        console.log(this.id);
        store.dispatch("getItemView", this.id);
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
