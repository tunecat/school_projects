<template>
    <div>
        <div v-if="statusErrorMessage">{{statusErrorMessage}}</div>
        <div v-else>
        <h1>{{commonTranslations.filter(t => t.code == "Edit")[0].name}}</h1>
        <div style="color: red">{{errorMessage}}</div>
        <hr />
        <div v-for="error in errors" :key="error.id" class="text-danger">
            <div>{{ error.msg }}</div>
        </div>
        <div v-if="categoryEditDTO" class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label" for="Name">{{categoriesViewTranslations.filter(t => t.code == "Name")[0].name}}</label>
                    <input
                        v-validate="'required|min:1|max:128'"
                        class="form-control"
                        type="text"
                        name="name"
                        data-val="true"
                        v-model="categoryEditDTO.name"
                        data-val-required="The Name field is required."
                        minlength="1"
                        maxlength="64"
                    />
                </div>
                <div>
                    <label class="typo__label">Single select</label>
                    <multiselect
                        v-model="selectedCategory"
                        :options="parentCategories"
                        :searchable="false"
                        :show-labels="false"
                        label="name"
                        track-by="id"
                        placeholder="Pick a parent category"
                    ></multiselect>
                </div>
                <button
                    v-if="errors.all().length === 0"
                    type="submit"
                    @click="onEdit()"
                    class="btn btn-primary"
                >{{sharedTranslations.filter(t => t.code == "Save")[0].name}}</button>
                <button v-else type="submit" class="btn btn-primary">{{sharedTranslations.filter(t => t.code == "Save")[0].name}}</button>
            </div>
        </div>
        <div>
            <router-link :to="`/Profile`">{{commonTranslations.filter(t => t.code == "BackToList")[0].name}}</router-link>
        </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { IBrandsEdit } from "../../domain/Brands/IBrandsEdit";
import store from "@/store";
import router from "@/router";
import ICategoryEdit from "../../domain/Category/ICategoryEdit";
import ICategory from "../../domain/Category/ICategory";
import CultureDTO from '@/domain/Translations/CultureDTO';

@Component
export default class CategoriesEdit extends Vue {
    @Prop()
    private id!: string;

    statusErrorMessage = "";

    private selectedCategory: ICategory | null = null;
    errorMessage = "";

    get parentCategories(): ICategory[] | null {
        return store.state.categories;
    }

    get categoryEditDTO(): ICategoryEdit | null {
        const category = store.state.category;
        if (category) {
            return {
                id: category.id,
                name: category.name,
                parentCategoryId: category.parentCategoryId
            };
        }
        return null;
    }

    onEdit(): void {
        if (this.categoryEditDTO!.name!.length > 1) {
            if (this.selectedCategory) {
                this.categoryEditDTO!.parentCategoryId = this.selectedCategory.id;
            }
            store
                .dispatch("editCategory", this.categoryEditDTO)
                .then(response => {
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
        store.dispatch("getCategory", this.id)
            .then(response => {
                console.log(response.statusCode);
                if (response.statusCode >= 400 && response.statusCode < 500) {
                    this.statusErrorMessage = "NOT ALLOWED";
                } else {
                    this.statusErrorMessage = JSON.parse(
                        response.statusErrorMessage
                    ).message;
                }
            })
            .catch(e => {
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

    get categoriesViewTranslations(): CultureDTO[] {
        const translations = store.state.translations;
        const ItemsViewTranslation = translations!.filter((t) => {
            if (t.entityName === "category") return t;
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
