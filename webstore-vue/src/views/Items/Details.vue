<template>
    <div>
        <h1>{{commonTranslations.filter(t => t.code == "Details")[0].name}}</h1>

        <div>
            <h4>Product</h4>
            <hr />
            <dl v-if="item" class="row">
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Name")[0].name}}</dt>
                <dd class="col-sm-10">{{item.name}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Description")[0].name}}</dt>
                <dd class="col-sm-10">{{item.description}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Price")[0].name}}</dt>
                <dd class="col-sm-10">{{item.price}}</dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Discount")[0].name}}</dt>
                <dd class="col-sm-10">
                    <div style="display: flex; flex-wrap: wrap;">
                        {{item.discount}} % ->
                        <div class="form-group">
                            <input
                                type="number"
                                id="kkk"
                                style="width:100px"
                                v-model="discount"
                                min="0"
                                max="99"
                                class="form-control"
                            />
                        </div>
                        <div class="form-group">
                            <input
                                @click="setDiscount()"
                                type="submit"
                                value="Set"
                                class="btn btn-primary"
                            />
                        </div>
                    </div>
                </dd>
                <dt class="col-sm-2">{{itemsViewTranslations.filter(t => t.code == "Brand")[0].name}}</dt>
                <dd class="col-sm-10">{{item.brand.name}}</dd>
            </dl>
            <strong>{{itemsViewTranslations.filter(t => t.code == "Categories")[0].name}}</strong>
            <p v-for="category in item.categories" :key="category.id">{{category.name}}</p>
        </div>
        <div>
            <router-link
                v-if="userRole.includes('Admin') || userRole.includes('Publisher')"
                :to="`../edit/${id}`"
            >{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</router-link> |
            <router-link :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import IItem from "@/domain/Item/IItem";
import CultureDTO from '@/domain/Translations/CultureDTO';

@Component
export default class ItemsDetails extends Vue {
    @Prop()
    private id!: string;

    discount: string | number = "";

    get item(): IItem | null {
        return store.state.item;
    }

    get userRole(): string {
        return store.getters.userRole;
    }

    setDiscount() {
        this.item!.discount = Number(this.discount);
        store.dispatch("editItem", this.item).then(response => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                store.dispatch("getItem", this.id);
            }
        });
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
        store.dispatch("getItem", this.id);
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
