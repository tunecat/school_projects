<template>
    <div>
        <div v-if="!roles">NOT ALLOWED</div>
        <div v-else>
            <h1>{{commonTranslations.filter(t => t.code == "Create")[0].name}}</h1>
            <div>{{errorMessage}}</div>
            <h4>Product</h4>
            <hr />
            <div v-for="error in errors" :key="error.id" class="text-danger">
                <div>{{ error.msg }}</div>
            </div>
            <div v-if="itemCreateDTO" class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="control-label" for="Name">{{itemsViewTranslations.filter(t => t.code == "Name")[0].name}}</label>
                        <input
                            v-validate="'required|min:1|max:128'"
                            class="form-control"
                            type="text"
                            data-val="true"
                            name="Name"
                            v-model="itemCreateDTO.name"
                            data-val-required="The Name field is required."
                        />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Description">{{itemsViewTranslations.filter(t => t.code == "Description")[0].name}}</label>
                        <input
                            v-validate="'min:2|max:508'"
                            class="form-control"
                            type="text"
                            name="Description"
                            v-model="itemCreateDTO.description"
                        />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="Price">{{itemsViewTranslations.filter(t => t.code == "Price")[0].name}}</label>
                        <input
                            v-validate="'required'"
                            class="form-control"
                            type="number"
                            id="Price"
                            name="Price"
                            v-model="itemCreateDTO.price"
                        />
                    </div>
                    <label class="typo__label">Select Brand</label>
                    <multiselect
                        v-model="selectedBrand"
                        :options="brands"
                        :searchable="false"
                        :show-labels="false"
                        label="name"
                        track-by="id"
                        placeholder="Choose product brand"
                    ></multiselect>
                    <label class="typo__label">Select Categories</label>
                    <multiselect
                        v-model="selectedCategories"
                        :multiple="true"
                        :close-on-select="false"
                        :clear-on-select="false"
                        :preserve-search="true"
                        placeholder="Pick some"
                        label="name"
                        track-by="name"
                        :options="categories"
                    >
                        <template slot="selection" slot-scope="{ values, search, isOpen }">
                            <span
                                class="multiselect__single"
                                v-if="selectedCategories.length &amp;&amp; !isOpen"
                            >{{ selectedCategories.length }} options selected</span>
                        </template>
                    </multiselect>
                    <div v-for="category in selectedCategories" :key="category.id">{{category.name}}</div>
                    <hr />
                    <button
                        v-if="errors.all().length === 0"
                        type="submit"
                        @click="onCreate()"
                        class="btn btn-primary"
                    >{{commonTranslations.filter(t => t.code == "Create")[0].name}}</button>
                    <button v-else type="submit" class="btn btn-secondary">{{commonTranslations.filter(t => t.code == "Create")[0].name}}</button>
                </div>
            </div>

            <div>
                <router-link :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import store from "@/store";
import router from "@/router";
import IItemCreate from "@/domain/Item/IItemCreate";
import IItemDTO from "@/domain/Item/IItemDTO";
import { IBrands } from "@/domain/Brands/IBrands";
import ICategory from "@/domain/Category/ICategory";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class ItemsCreate extends Vue {
    selectedBrand: IBrands | null = null;
    selectedCategories: Array<ICategory> = [];

    itemCreateDTO: IItemCreate | null = {
        description: null,
        itemCategories: null,
        brandId: "",
        discount: 0,
        name: "",
        userId: null,
        price: 0,
    };

    get roles(): boolean {
        return store.getters.userRole.includes("Publisher");
    }

    get brands(): IBrands[] {
        return store.state.brands;
    }

    get categories(): ICategory[] | null {
        return store.state.categories;
    }

    errorMessage = "";

    onCreate(): void {
        if (this.itemCreateDTO!.description === "") {
            this.itemCreateDTO!.description = null;
        }
        if (this.itemCreateDTO!.name.length > 1 && this.selectedBrand) {
            this.itemCreateDTO!.brandId = this.selectedBrand.id;
            this.itemCreateDTO!.price = Number(this.itemCreateDTO!.price);
            this.itemCreateDTO!.itemCategories = this.selectedCategories.map(
                (c) => c.id
            );
            store
                .dispatch("createItem", this.itemCreateDTO)
                .then((response) => {
                    if (
                        response.statusCode >= 200 &&
                        response.statusCode < 300
                    ) {
                        router.push("/Profile");
                    } else {
                        this.errorMessage = JSON.parse(
                            response.errorMessage
                        ).message;
                    }
                });
        }
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
        store.dispatch("getChildCategories");
        store.dispatch("getBrands");
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
