<template>
    <div>
        <h1>{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</h1>
        <div>{{errorMessage}}</div>
        <h4>Product</h4>
        <hr />
        <div v-for="error in errors" :key="error.id" class="text-danger">
            <div>{{ error.msg }}</div>
        </div>
        <div v-if="itemDTO" class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label
                        class="control-label"
                        for="Name"
                    >{{itemsViewTranslations.filter(t => t.code == "Name")[0].name}}</label>
                    <input
                        v-validate="'required|min:1|max:128'"
                        class="form-control"
                        type="text"
                        data-val="true"
                        name="Name"
                        v-model="itemDTO.item.name"
                        data-val-required="The Name field is required."
                    />
                </div>
                <div class="form-group">
                    <label
                        class="control-label"
                        for="Description"
                    >{{itemsViewTranslations.filter(t => t.code == "Description")[0].name}}</label>
                    <input
                        v-validate="'min:2|max:508'"
                        class="form-control"
                        type="text"
                        name="Description"
                        v-model="itemDTO.item.description"
                    />
                </div>
                <div class="form-group">
                    <label
                        class="control-label"
                        for="Price"
                    >{{itemsViewTranslations.filter(t => t.code == "Price")[0].name}}</label>
                    <input
                        v-validate="'required'"
                        class="form-control"
                        type="number"
                        id="Price"
                        name="Price"
                        v-model="itemDTO.item.price"
                    />
                </div>
                <label class="typo__label">Select Brand</label>
                <multiselect
                    v-model="itemDTO.item.brand"
                    :options="itemDTO.brandsForCreation"
                    :searchable="false"
                    :show-labels="false"
                    label="name"
                    track-by="id"
                    placeholder="Choose product brand"
                ></multiselect>
                <label class="typo__label">Select Categories</label>
                <multiselect
                    v-model="itemDTO.item.categories"
                    :multiple="true"
                    :close-on-select="false"
                    :clear-on-select="false"
                    :preserve-search="true"
                    placeholder="Pick some"
                    label="name"
                    track-by="name"
                    :options="itemDTO.categoriesForCreation"
                >
                    <template slot="selection" slot-scope="{ values, search, isOpen }">
                        <span
                            class="multiselect__single"
                            v-if="itemDTO.item.categories.length &amp;&amp; !isOpen"
                        >{{ itemDTO.item.categories.length }} options selected</span>
                    </template>
                </multiselect>
                <div
                    v-for="category in itemDTO.item.categories"
                    :key="category.id"
                >* {{category.name}}</div>
                <hr />
                <button
                    v-if="errors.all().length === 0"
                    type="submit"
                    @click="onEdit()"
                    class="btn btn-primary"
                >{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</button>
                <button
                    v-else
                    type="submit"
                    class="btn btn-secondary"
                >{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</button>
                <a
                    class="btn btn-danger"
                    @click="onDelete()"
                >{{commonTranslations.filter(t => t.code == "Delete")[0].name}}</a>
            </div>
        </div>

        <div>
            <router-link
                :to="`/Profile`"
            >{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import store from "@/store";
import router from "@/router";
import IItemEdit from "@/domain/Item/IItemEdit";
import IItemDTO from "@/domain/Item/IItemDTO";
import { IBrands } from "../../domain/Brands/IBrands";
import ICategory from "../../domain/Category/ICategory";
import CultureDTO from "@/domain/Translations/CultureDTO";

@Component
export default class ItemsEdit extends Vue {
    @Prop()
    private id!: string;

    selectedBrand: IBrands | null = null;
    selectedCategories: Array<ICategory> = [];

    get itemDTO(): IItemDTO | null {
        const itemDTO = store.state.itemDTO;
        if (itemDTO) {
            return {
                item: itemDTO.item,
                categoriesForCreation: itemDTO.categoriesForCreation,
                brandsForCreation: itemDTO.brandsForCreation,
            };
        }
        return null;
    }

    errorMessage = "";

    onDelete(): void {
        this.$confirm("Are you sure you want to delete this")
            .then(() => {
                store
                    .dispatch("deleteItem", this.id)
                    .then((wasDeleted: boolean) => {
                        if (wasDeleted) {
                            router.push("/Profile");
                        }
                    });
            })
            .catch((e) => console.log(e));
    }

    onEdit(): void {
        if (this.itemDTO!.item.description === "") {
            this.itemDTO!.item.description = null;
        }

        if (this.itemDTO!.item.name.length > 1 && this.itemDTO!.item.brand) {
            const itemEditDTO: IItemEdit = {
                id: this.itemDTO!.item.id,
                description: this.itemDTO!.item.description,
                itemCategories: this.selectedCategories.map((c) => c.id),
                brandId: this.itemDTO!.item.brand.id,
                discount: this.itemDTO!.item.discount,
                name: this.itemDTO!.item.name,
                userId: this.itemDTO!.item.userId,
                price: Number(this.itemDTO!.item.price),
            };
            console.log("KOKO2");
            store.dispatch("editItem", itemEditDTO).then((response) => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    router.push("/Items/Details/" + this.id);
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
        store.dispatch("getItemDTO", this.id);
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
